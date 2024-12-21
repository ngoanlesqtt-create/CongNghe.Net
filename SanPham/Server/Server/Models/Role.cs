using System.ComponentModel.DataAnnotations;

namespace Server.Models
{
    public class Role
    {
        [Required]
        public string Name { get; set; }
    }
}
