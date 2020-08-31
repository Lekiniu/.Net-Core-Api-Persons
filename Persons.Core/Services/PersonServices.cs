using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Persons.Data.Interfaces;
using Persons.Data.Models;
using Persons.Data.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;
using Persons.Core.Profiles;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Persons.Data.Helpers;

namespace Persons.Core.Services
{
    public class PersonServices: IPersonServices
    {

        private readonly PersonsDbContext _context;
        private readonly IHostingEnvironment _appEnvironment;

        public PersonServices(PersonsDbContext context, IHostingEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }


     
        #region Crud

        public async Task<IEnumerable<PersonsModel>> GetAllPersonsAsync(PagedParameters pagedParameters)
        {
            var personsModel = _context.Persons;
                
            var querySearch = pagedParameters.SearchQuery.Trim().ToLower();
            var sortOrder = pagedParameters.SortOrder.Trim().ToLower();

            //CurrentSort = sortOrder;
            //NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            //DateSort = sortOrder == "surName" ? "surName_desc" : "surName";

            var personsCollection = _context.Persons as IQueryable<Data.Entities.Persons>;
            //IQueryable <Data.Entities.Persons> personsCollection = from s in _context.Persons
            //                                select s;
            if (!string.IsNullOrWhiteSpace(querySearch))
            {
                personsCollection = _context.Persons.
                    Where(m => m.Name.Contains(querySearch)
                    || m.NameEng.Contains(querySearch)
                    || m.Surname.Contains(querySearch)
                    || m.SurnameEng.Contains(querySearch)
                    || m.PrivateNumber.Contains(querySearch)
                    || m.Email.Contains(querySearch)
                    || m.PhoneNumber.Contains(querySearch)
                    || m.Address.Street.Contains(querySearch)
                    || m.Address.City.Contains(querySearch)
                    || m.Address.Country.Contains(querySearch));
            }
         switch (sortOrder)
            {
                case "name_desc":
                    personsCollection = personsCollection.OrderByDescending(s => s.Name);
                    break;
                case "surname":
                    personsCollection = personsCollection.OrderBy(s => s.Surname);
                    break;
                case "surname_desc":
                    personsCollection = personsCollection.OrderByDescending(s => s.Surname);
                    break;
                case "name":
                    personsCollection = personsCollection.OrderBy(s => s.Name);
                    break;
                default:
                    personsCollection = personsCollection.OrderBy(s => s.PersonId);
                    break;
            }

            personsCollection =  personsCollection
                .Skip(pagedParameters.PageSize * (pagedParameters.PageNumber - 1))
                .Take(pagedParameters.PageSize);

            var personsList = await personsCollection.ToListAsync();

            var result = Mapping.Mapper.Map<IEnumerable<PersonsModel>>(personsList);
            //var beforePagingList = _context.Persons.AsQueryable();

            //var afterPagingList = PagedList<Data.Entities.Persons>.Create(beforePagingList,
            //    pagedParameters.PageNumber,
            //    pagedParameters.PageSize);

            return result;
        }


        public async Task <PersonsModel> GetPersonByIdAsync(int personId)     
        {           
            var personModel = await _context.Persons 
                    .Include(m => m.Files)
                    .Include(m => m.Address)
                    .Include(m => m.RelatedPersons)
                    .ThenInclude(e=>e.RelatedPerson)
                    .Include(e=>e.RelatedPersons)
                    .ThenInclude(m=>m.PersonType)
                    .FirstOrDefaultAsync(m => m.PersonId == personId);
 
            var result = Mapping.Mapper.Map<PersonsModel>(personModel);

            return result;
        }



        public async Task<PersonsModel> CreatePersonAsync(PersonsModel person)
        {
            var result = Mapping.Mapper.Map<Persons.Data.Entities.Persons>(person);
            await _context.Persons.AddAsync(result);
            await _context.SaveChangesAsync();
            var personModel = await _context.Persons
                              .Include(m => m.Address)
                              .Include(m => m.Files)
                              .FirstOrDefaultAsync(m => m.PersonId == result.PersonId);

            return Mapping.Mapper.Map<PersonsModel>(personModel);

        }

        public async Task<PersonsModel> EditPersonAsync(int personId, PersonsModel person)
        {
            var oldmodel = await _context.Persons
                .FirstOrDefaultAsync(e => e.PersonId == personId);

            var result = Mapping.Mapper.Map(person, oldmodel);
            await _context.SaveChangesAsync();

            return Mapping.Mapper.Map<PersonsModel>(result);
        }


        public async Task DeletePersonAsync(int personId)
        {
            var deleteFilesInfo = _context.Files.Where(e => e.PersonId == personId).ToList();


            var deleteAddressInfo = _context.Addresses.FirstOrDefault(e => e.PersonId == personId);

            var relatedPersonsInfo = await _context.RelatedPersons
                .Where(e => e.PersonId == personId)
                .ToListAsync();

            var personInfo = await _context.Persons
                          .FirstOrDefaultAsync(e => e.PersonId == personId);


            if (deleteFilesInfo != null)
            {
                //to delete from folder
                foreach (var file in deleteFilesInfo)
                {
                    DeleteFilesWithPerson(file);
                    _context.SaveChanges();
                }
            }
            if (relatedPersonsInfo != null)
            {
                _context.RelatedPersons.RemoveRange(relatedPersonsInfo);
            } 
            if (deleteAddressInfo != null)
            {
                _context.Addresses.Remove(deleteAddressInfo);
            }

            _context.Persons.Remove(personInfo);
            await _context.SaveChangesAsync();

        }

        private void DeleteFilesWithPerson(Files file)
        {
            var path = Path.Combine(_appEnvironment.WebRootPath + file.Url);
            if (File.Exists(path))
            {
                _context.Files.Remove(file);
                File.Delete(path);
            }
        }
        #endregion

        public bool CheckIfPersonExit(int personId)
        {
            return _context.Persons.FirstOrDefault(e => e.PersonId == personId) != null ? true : false;
        }

        public bool CheckIfNewPersonExit(PersonsModel person)
        {
            return _context.Persons.FirstOrDefault(e => e.PrivateNumber == person.PrivateNumber) == null ? true : false;
        }


        public bool CheckIfOldPersonExit(int personId, PersonsModel person)
        {
            var result = _context.Persons.FirstOrDefault(e => e.PersonId == personId);
            return _context.Persons.FirstOrDefault(e => e.PrivateNumber == person.PrivateNumber) == null || result.PrivateNumber != person.PrivateNumber ? true : false;
        }
    }
}
