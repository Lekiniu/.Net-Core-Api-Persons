using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Persons.Data.Entities
{
    public class Persons
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PersonId { get; set; }

        [MaxLength(50)]
        [Required]
        public string Name { get; set; }

        [MaxLength(50)]
        [Required]
        public string NameEng { get; set; }

        [MaxLength(50)]
        [Required]
        public string Surname { get; set; }

        [MaxLength(50)]
        [Required]
        public string SurnameEng { get; set; }

        [MaxLength(11)]
        [Required]
        public string PrivateNumber { get; set; }

        [MaxLength(50)]
        [Required]
        public string Email { get; set; }

        [MaxLength(12)]
        [Required]
        public string MobileNumber { get; set; }

        [MaxLength(12)]
        public string PhoneNumber { get; set; }

        public DateTime BirthDate { get; set; }

        public int? AddressId { get; set; }

        public virtual Addresses Address { get; set; }

        public virtual ICollection<Files> Files { get; set; }

        public virtual ICollection<RelatedPersons> RelatedPersons { get; set; }

        public virtual ICollection<RelatedPersons> PersonsGroup { get; set; }
    }
}
