using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Persons.Data.Models;

namespace Persons.Data.Interfaces
{
    public interface IRelatedPersonServices
    {
        Task<PersonsModel> AddRelatedPersonAsync(int personId, PersonTypesModel type, int relativePersonId);

        Task<PersonsModel> GetPersonByIdAsync(int personId);

        bool checkIfRelatedPersonExist(int personId, int relativePersonId);

         Task<IEnumerable<RelatedPersonsModel>> GetRelatedPersonsByIdAsync(int personId);

         Task DeleteRelatedPersonAsync(int personId,int relativePersonId);
    }
}
