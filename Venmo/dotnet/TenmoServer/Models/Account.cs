using System.ComponentModel.DataAnnotations;

namespace TenmoServer.Models
{
    public class Account
    {
        // Properties
        [Required]
        public int AccountId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public decimal Balance { get; set; }
    }
}
