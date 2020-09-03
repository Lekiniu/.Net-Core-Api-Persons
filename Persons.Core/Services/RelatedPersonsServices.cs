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

namespace Persons.Core.Services
{
    public class RelatedPersonsServices : IRelatedPersonServices
    { 
        private readonly PersonsDbContext _context;

        public RelatedPersonsServices(PersonsDbContext context)
        {
            _context = context;
        }

        public bool checkIfRelatedPersonExist(int personId, int relativePersonId)
        {
            return _context.RelatedPersons.FirstOrDefault(e => e.PersonId == personId && e.RelatedPersonId == relativePersonId) == null ? true : false ;
         }


        public async Task<PersonsModel> AddRelatedPersonAsync(int personId, PersonTypesModel personTypeModel, int relativePersonId)
        {
            var person = await _context.Persons.
                FirstOrDefaultAsync(e => e.PersonId == personId);

            await SaveRelatedPerson(personId, personTypeModel, relativePersonId);


            return Mapping.Mapper.Map<PersonsModel>(person);
        }

        public async Task DeleteRelatedPersonAsync(int personId, int relativePersonId)
        {
            var removableRelatedPerson = await _context.RelatedPersons.
                FirstOrDefaultAsync(e => e.PersonId == personId && e.RelatedPersonId == relativePersonId);

            var removablePersonType = await _context.PersonTypes.
                FirstOrDefaultAsync(e => e.PersonTypeId == removableRelatedPerson.PersonTypeId);

            if (removableRelatedPerson != null)
            {
                _context.RelatedPersons.Remove(removableRelatedPerson);
                await _context.SaveChangesAsync();
            }

            if (removablePersonType != null)
            {
                _context.PersonTypes.Remove(removablePersonType);
                await _context.SaveChangesAsync();
            }
        }



        public async Task <IEnumerable<RelatedPersonsModel>> GetRelatedPersonsByIdAsync(int personId)
        {
            var relatedPersonList = await _context.RelatedPersons
                          .Where(m => m.PersonId == personId).Select(e=>e.RelatedPerson).ToListAsync();
            

            //var model= Mapping.Mapper.Map<List<RelatedPersonsModel>>(relatedPersonList);

            var result = relatedPersonList.Select(m => new RelatedPersonsModel()
            {
                RelatedPersonId = m.PersonId,
                Name = m.Name,
                Surname = m.Surname,
                PrivateNumber = m.PrivateNumber,
                Type = getPersonType(personId, m.PersonId)
            });

            var model = Mapping.Mapper.Map<List<RelatedPersonsModel>>(result);
            return model;
        }

        private string getPersonType(int personId, int relatedPersonId)
        {
            var model =  _context.RelatedPersons.
             FirstOrDefault(e => e.PersonId == personId && e.RelatedPersonId == relatedPersonId);
            var result =  _context.PersonTypes.FirstOrDefault(e => e.PersonTypeId == model.PersonTypeId);
            return result.TypeName;
        }

        private async Task SaveRelatedPerson(int personId, PersonTypesModel personTypeModel, int relativePersonId)
        {

             var result = Mapping.Mapper.Map<PersonTypes>(personTypeModel);
             await _context.PersonTypes.AddAsync(result);

            await _context.RelatedPersons.AddAsync(
              new RelatedPersons
              {
                  RelatedPersonId = relativePersonId,
                  PersonId = personId,
                  PersonTypeId = result.PersonTypeId
              });
          
            await _context.SaveChangesAsync();
        }

    }

}
