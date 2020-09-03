using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Persons.Data.Helpers;
using Persons.Data.Models;

namespace Persons.Data.Interfaces
{
    public interface IPersonServices
    {
        Task<PersonsModel> CreatePersonAsync(PersonsModel person);
          
        Task<PersonsModel> EditPersonAsync(int personId, PersonsModel person);

        Task<PersonsModel> EditPersonAddressAsync(int personId,AddressesModel address, int addressId);

        Task DeletePersonAsync(int personId);

        Task<IEnumerable<PersonsModel>> GetAllPersonsAsync(PagedParameters pagedParameters);

        bool CheckIfNewPersonExit(PersonsModel person);

        bool CheckIfOldPersonExit(int personId, PersonsModel person);

        bool CheckIfPersonExit(int personId);
    }
}
