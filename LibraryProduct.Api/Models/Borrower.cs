using System.ComponentModel.DataAnnotations;

namespace LibraryProduct.Api.Models
{
    public class Borrower
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? ContactNumber { get; set; }
        public string? Email { get; set; }
        public string? MembershipId { get; set; } // unique
        public string? Address { get; set; }
        public DateTime MembershipStart { get; set; }
        public DateTime MembershipExpiry { get; set; }
    }
}
