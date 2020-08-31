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

        [MaxLength(15)]
        [Required]
        public string TypeName { get; set; }

        public virtual ICollection<RelatedPersonsModel> RelatedPersons { get; set; }
    }
}
