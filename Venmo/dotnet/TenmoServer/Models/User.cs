using System.ComponentModel.DataAnnotations;

namespace TenmoServer.Models
{
    public class User
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string PasswordHash { get; set; }
        [Required]
        public string Salt { get; set; }
        public string Email { get; set; }
    }

    /// <summary>
    /// Model to return upon successful login
    /// </summary>
    public class ReturnUser
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public string Username { get; set; }
        //public string Role { get; set; }
        [Required]
        public string Token { get; set; }
    }

    /// <summary>
    /// Model to accept login parameters
    /// </summary>
    public class LoginUser
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }

    public class UserTEBucks
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public int AccountId { get; set; }
    }
}
