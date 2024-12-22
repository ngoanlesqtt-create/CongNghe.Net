using System.ComponentModel.DataAnnotations;

namespace Server.DO
{
    public class UserDTO
    {
        [Required]
        [EmailAddress]
        public string email { get; set; }

        [Required]
        public string password { get; set; }
    }
}
