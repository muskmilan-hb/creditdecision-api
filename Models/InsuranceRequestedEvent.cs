namespace CreditDecision.Models
{
    public class InsuranceRequestedEvent
    {
        public int LoanId { get; set; }
        public int CustomerID { get; set; }
        public decimal Amount { get; set; }
        public string? Policytype { get; set; }
    }
}
