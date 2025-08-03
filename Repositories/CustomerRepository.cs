using CreditDecision.Data;
using CreditDecision.Models;
using CreditDecision.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LoanManager.Api.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly CreditDecisionDbContext _context;

        public CustomerRepository(CreditDecisionDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await _context.Customers.Include(c => c.Loans).ToListAsync();
        }

        public async Task<Customer?> GetByIdAsync(int id)
        {
            return await _context.Customers.Include(c => c.Loans)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Customer> AddAsync(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return customer;
        }

        public async Task<Customer?> UpdateAsync(Customer customer)
        {
            var existing = await _context.Customers.FindAsync(customer.Id);
            if (existing == null) return null;

            existing.FullName = customer.FullName;
            existing.Email = customer.Email;
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _context.Customers.FindAsync(id);
            if (existing == null) return false;

            _context.Customers.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
