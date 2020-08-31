using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Persons.Data.Entities
{
    public class Addresses
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AddressId { get; set; }

        [MaxLength(20)]
        public string Country { get; set; }

        [MaxLength(20)]
        public string City { get; set; }

        [MaxLength(50)]
        public string Street { get; set; }


        public int PersonId { get; set; }

        [ForeignKey("PersonId")]
        public virtual Persons Person { get; set; }
    }
}
