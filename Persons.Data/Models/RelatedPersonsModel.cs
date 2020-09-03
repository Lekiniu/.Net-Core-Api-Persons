using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Persons.Data.Models
{
   public class RelatedPersonsModel
    {
        [ScaffoldColumn(false)]
        public int RelatedPersonId { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string PrivateNumber { get; set; }

        public string Type { get; set; }
    }
}

