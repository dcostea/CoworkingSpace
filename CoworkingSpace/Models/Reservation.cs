using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CoworkingSpace.Models
{
    public class Reservation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ScaffoldColumn(false)]
        public int ReservationId { get; set; }

        [Required]
        public int MembershipId { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [StringLength(1000)]
        public string Details { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }


        public virtual Membership Membership { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
