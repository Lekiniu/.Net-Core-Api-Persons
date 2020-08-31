using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Persons.Data.Entities
{
    public class PersonTypes
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PersonTypeId { get; set; }

        [MaxLength(15)]
        [Required]
        public string TypeName { get; set; }

        public virtual ICollection<RelatedPersons> RelatedPersons { get; set; }
    }
}
