using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Persons.Data.Models
{
    public class FilesModel
    {
        [ScaffoldColumn(false)]
        public int FileId { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }

        [JsonIgnore]
        public int PersonId { get; set; }

        public  PersonsModel Person { get; set; }
    }
}
