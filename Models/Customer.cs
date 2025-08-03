using System.Text.Json.Serialization;

namespace CreditDecision.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        [JsonIgnore]
        public ICollection<Loan>? Loans { get; set; }
    }
}
