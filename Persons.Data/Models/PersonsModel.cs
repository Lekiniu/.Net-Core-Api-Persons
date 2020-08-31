using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Persons.Data.Models
{
   public class PersonsModel
    {
        [ScaffoldColumn(false)]
        public int PersonId { get; set; }
 
        public string Name { get; set; }

        public string NameEng { get; set; }

        public string Surname { get; set; }

        public string SurnameEng { get; set; }

        public string PrivateNumber { get; set; }

        public string Email { get; set; }

        public string MobileNumber { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime BirthDate { get; set; }

        [JsonIgnore]
        public int? AddressId { get; set; }

        public  AddressesModel Address { get; set; }

        public  IEnumerable<FilesModel> Files { get; set; }

        public IEnumerable<PersonsModel> RelatedPerson { get; set; }

        //public ICollection<PersonsModel> Person { get; set; }
    }
}
