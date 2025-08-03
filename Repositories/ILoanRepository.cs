using CreditDecision.Models;

namespace LoanManager.Api.Repositories
{
    public interface ILoanRepository
    {
        Task<IEnumerable<Loan>> GetAllAsync();
        Task<Loan?> GetByIdAsync(int id);
        Task<Loan> AddAsync(Loan loan);
        Task<Loan?> UpdateAsync(Loan loan);
        Task<bool> DeleteAsync(int id);
    }
}
