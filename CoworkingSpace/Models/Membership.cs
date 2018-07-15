using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoworkingSpace.Models
{
    public class Membership
    {
        public int MembershipId { get; set; }
        public string Title { get; set; }
        public Frequency Frequency { get; set; }
        public Spot Spot { get; set; }
        public int? SpotSeats { get; set; }
        public int? NoOfDays { get; set; }
        public DayPart DayPart { get; set; }
        public WeekPart WeekPart { get; set; }
        public int Price { get; set; }
    }
}
