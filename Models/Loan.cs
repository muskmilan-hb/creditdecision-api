namespace CreditDecision.Models
{
    public class Loan
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime IssuedDate { get; set; }
        public string Status { get; set; } = "Pending";

        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }

        public bool HasInsurance { get; set; } = false;
    }
}
