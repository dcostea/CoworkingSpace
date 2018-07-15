using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoworkingSpace.Models
{
    public class Reservation
    {
        public int ReservationId { get; set; }
        public int MembershipId { get; set; }
        public int CustomerId { get; set; }
        public string Details { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
