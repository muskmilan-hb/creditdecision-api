using CreditDecision.Models;
using Microsoft.EntityFrameworkCore;

namespace CreditDecision.Data
{
    public class CreditDecisionDbContext(DbContextOptions<CreditDecisionDbContext> options) : DbContext(options)
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Loan> Loans { get; set; }
    }
}
