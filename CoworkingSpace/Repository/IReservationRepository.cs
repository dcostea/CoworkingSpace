using CoworkingSpace.Models;
using System.Collections.Generic;

namespace CoworkingSpace.Repository
{
    public interface IReservationRepository : IRepository<Reservation>
    {
        IEnumerable<Customer> GetAllCustomers();
        IEnumerable<Membership> GetAllMemberships();
        bool ReservationExists(int id);
    }
}
