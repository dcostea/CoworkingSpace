using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoworkingSpace.Data;
using CoworkingSpace.Models;
using Microsoft.EntityFrameworkCore;

namespace CoworkingSpace.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _context;

        public CustomerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(Customer item)
        {
            throw new NotImplementedException();
        }

        public async Task AddAsync(Customer customer)
        {
            _context.Add(customer);
            await _context.SaveChangesAsync();
        }

        public Customer Find(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Customer> FindAsync(int id)
        {
            return await _context.Customers.FirstOrDefaultAsync(m => m.CustomerId == id);
        }

        public IEnumerable<Customer> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await _context.Customers.ToListAsync();
        }

        public void Remove(Customer customer)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveAsync(Customer customer)
        {
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
        }

        public void Update(Customer customer)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(Customer customer)
        {
            _context.Update(customer);
            await _context.SaveChangesAsync();
        }

        public bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.CustomerId == id);
        }
    }
}
