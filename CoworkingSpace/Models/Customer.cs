using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CoworkingSpace.Models
{
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerId { get; set; }

        [StringLength(200)]
        public string Name { get; set; }

        [StringLength(13)]
        public string LegalId { get; set; }

        [StringLength(13)]
        public string Phone { get; set; }
    }
}
