using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CoworkingSpace.Models
{
    public class Membership
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MembershipId { get; set; }

        [StringLength(100)]
        public string Title { get; set; }

        public Frequency Frequency { get; set; }

        public Spot Spot { get; set; }

        [Range(0, 999)]
        public int? SpotSeats { get; set; }

        [Range(1, 31)]
        public int? NoOfDays { get; set; }

        public DayPart DayPart { get; set; }

        public WeekPart WeekPart { get; set; }

        [Range(0, int.MaxValue)]
        public int Price { get; set; }

        [StringLength(1000)]
        public string Benefits { get; set; }
    }
}
