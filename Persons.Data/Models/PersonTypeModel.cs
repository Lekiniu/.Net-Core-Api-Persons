using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Persons.Data.Models
{
    public class PersonTypeModel
    {
        [ScaffoldColumn(false)]
        public int PersonTypeId { get; set; }

        public string TypeName { get; set; }

        public virtual IEnumerable<RelatedPersonsModel> RelatedPersons { get; set; }
    }
}
