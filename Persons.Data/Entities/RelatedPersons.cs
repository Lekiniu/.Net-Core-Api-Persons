using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Persons.Data.Entities
{
   public class RelatedPersons
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RelateId { get; set; }

        [MaxLength(15)]
        [Required]
        public string Type { get; set; }

        public int? PersonId { get; set; }

        public int? RelatedPersonId { get; set; }

        [ForeignKey("PersonId")]
        public virtual Persons Person { get; set; }

        [ForeignKey("RelatedPersonId")]
        public virtual Persons RelatedPerson { get; set; }

    }
}
