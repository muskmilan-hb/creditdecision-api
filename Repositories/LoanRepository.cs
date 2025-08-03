using CreditDecision.Data;
using CreditDecision.Models;
using Microsoft.EntityFrameworkCore;

namespace LoanManager.Api.Repositories
{
    public class LoanRepository : ILoanRepository
    {
        private readonly CreditDecisionDbContext _context;

        public LoanRepository(CreditDecisionDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Loan>> GetAllAsync()
        {
            return await _context.Loans.Include(l => l.Customer).ToListAsync();
        }

        public async Task<Loan?> GetByIdAsync(int id)
        {
            return await _context.Loans.Include(l => l.Customer)
                .FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task<Loan> AddAsync(Loan loan)
        {
            _context.Loans.Add(loan);
            await _context.SaveChangesAsync();
            return loan;
        }

        public async Task<Loan?> UpdateAsync(Loan loan)
        {
            var existing = await _context.Loans.FindAsync(loan.Id);
            if (existing == null) return null;

            existing.Amount = loan.Amount;
            existing.IssuedDate = loan.IssuedDate;
            existing.Status = loan.Status;
            existing.CustomerId = loan.CustomerId;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _context.Loans.FindAsync(id);
            if (existing == null) return false;

            _context.Loans.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
