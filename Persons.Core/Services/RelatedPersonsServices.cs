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
        private readonly IPersonServices _personServices;
        public RelatedPersonsServices(PersonsDbContext context, IPersonServices personServices)
        {
            _context = context;
            _personServices = personServices;
        }

        public bool checkIfRelatedPersonExist(int personId, int relativePersonId)
        {
            return _context.RelatedPersons.FirstOrDefault(e => e.PersonId == personId && e.RelatedPersonId == relativePersonId) == null ? true : false ;
         }
    

        public async Task<PersonsModel> AddRelatedPersonAsync(int personId, string type, int relativePersonId)
        {
            var person = await _context.Persons.
                FirstOrDefaultAsync(e => e.PersonId == personId);

                await SaveRelatedPerson( personId, type, relativePersonId);
         

            return Mapping.Mapper.Map<PersonsModel>(person);
        }

        public async Task DeleteRelatedPersonAsync(int personId, int relativePersonId)
        {
            var relatedPerson = await _context.RelatedPersons.
                FirstOrDefaultAsync(e => e.PersonId == personId && e.RelatedPersonId == relativePersonId);

            if(relatedPerson != null)
            {
                _context.RelatedPersons.Remove(relatedPerson);
                await _context.SaveChangesAsync();
            }
        }

        public async Task <IEnumerable<RelatedPersonsModel>> GetRelatedPersonsByIdAsync(int personId)
        {
            var relatedPersonList = await _context.RelatedPersons
                          .Where(m => m.PersonId == personId).Select(e=>e.RelatedPerson).ToListAsync();


            //var model= Mapping.Mapper.Map<List<PersonsModel>>(personModel);

            var result = relatedPersonList.Select(m => new RelatedPersonsModel()
            {
                RelatedPersonId = m.PersonId,
                Name= m.Name,
                Surname= m.Surname,
                PrivateNumber = m.PrivateNumber,
                Type = getPersonType(personId, m.PersonId)
            });

            //var model = Mapping.Mapper.Map<List<PersonsModel>>(RelatedPersonList);
            return result;
        }

        private  string getPersonType(int personId, int relatedPersonId)
        {
            var model= _context.RelatedPersons.
             FirstOrDefault(e => e.PersonId == personId && e.RelatedPersonId == relatedPersonId);

            return model.Type;
        }

        private async Task SaveRelatedPerson(int personId, string type, int relativePersonId)
        {
            await _context.RelatedPersons.AddAsync(
              new RelatedPersons
              {
                  RelatedPersonId = relativePersonId,
                  PersonId = personId,
                  Type = type
              });
            await _context.SaveChangesAsync();
        }

    }

}
