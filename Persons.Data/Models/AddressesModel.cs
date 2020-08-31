using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Persons.Data.Models
{
    public class AddressesModel
    {
        [ScaffoldColumn(false)]
        public int AddressId { get; set; }


        public string Country { get; set; }

        public string City { get; set; }

       
        public string Street { get; set; }

        [JsonIgnore]
        public int PersonId { get; set; }

        public  PersonsModel Person { get; set; }
    }
}
