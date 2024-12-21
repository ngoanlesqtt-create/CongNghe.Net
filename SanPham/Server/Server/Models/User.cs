using System.ComponentModel.DataAnnotations;

namespace Server.Models
{
    public class User
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string PhoneNumber { get; set; }
    }
}
