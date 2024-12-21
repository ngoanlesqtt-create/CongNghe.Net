using System.ComponentModel.DataAnnotations;

namespace Server.Models
{
    public class UserDTO
    {
        [Required]
        [EmailAddress]
        public string email { get; set; }  

        [Required]
        public string password {  get; set; }
    }
}
