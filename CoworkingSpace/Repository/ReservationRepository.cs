using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoworkingSpace.Data;
using CoworkingSpace.Models;
using Microsoft.EntityFrameworkCore;

namespace CoworkingSpace.Repository
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly ApplicationDbContext _context;

        public ReservationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public ReservationRepository()
        {
        }

        public void Add(Reservation item)
        {
            throw new NotImplementedException();
        }

        public async Task AddAsync(Reservation reservation)
        {
            _context.Add(reservation);
            await _context.SaveChangesAsync();
        }

        public Reservation Find(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Reservation> FindAsync(int id)
        {
            return await _context.Reservations
                .Include(r => r.Customer)
                .Include(r => r.Membership)
                .FirstOrDefaultAsync(m => m.ReservationId == id);
        }

        public IEnumerable<Reservation> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Reservation>> GetAllAsync()
        {
            var applicationDbContext = _context.Reservations.Include(r => r.Customer).Include(r => r.Membership);
            return await applicationDbContext.ToListAsync();
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            return _context.Customers;
        }

        public IEnumerable<Membership> GetAllMemberships()
        {
            return _context.Memberships;
        }

        public void Remove(Reservation item)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveAsync(Reservation reservation)
        {
            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();
        }

        public void Update(Reservation item)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(Reservation reservation)
        {
            _context.Update(reservation);
            await _context.SaveChangesAsync();
        }

        public bool ReservationExists(int id)
        {
            return _context.Reservations.Any(e => e.ReservationId == id);
        }
    }
}
