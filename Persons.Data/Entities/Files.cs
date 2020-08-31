using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Persons.Data.Entities
{
    public class Files
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FileId { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }

        public int PersonId { get; set; }

        [ForeignKey("PersonId")]
        public virtual Persons Person { get; set; }
    }
}
