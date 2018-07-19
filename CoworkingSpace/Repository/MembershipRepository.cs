using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoworkingSpace.Data;
using CoworkingSpace.Models;
using Microsoft.EntityFrameworkCore;

namespace CoworkingSpace.Repository
{
    public class MembershipRepository : IMembershipRepository
    {
        private readonly ApplicationDbContext _context;

        public MembershipRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(Membership item)
        {
            throw new NotImplementedException();
        }

        public async Task AddAsync(Membership membership)
        {
            _context.Add(membership);
            await _context.SaveChangesAsync();
        }

        public Membership Find(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Membership> FindAsync(int id)
        {
            return await _context.Memberships.FirstOrDefaultAsync(m => m.MembershipId == id);
        }

        public IEnumerable<Membership> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Membership>> GetAllAsync()
        {
            return await _context.Memberships.ToListAsync();
        }

        public void Remove(Membership item)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveAsync(Membership membership)
        {
            _context.Memberships.Remove(membership);
            await _context.SaveChangesAsync();
        }

        public void Update(Membership item)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(Membership membership)
        {
            _context.Update(membership);
            await _context.SaveChangesAsync();
        }

        public bool MembershipExists(int id)
        {
            return _context.Memberships.Any(e => e.MembershipId == id);
        }
    }
}
