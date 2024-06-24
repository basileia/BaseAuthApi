using System.ComponentModel.DataAnnotations;

namespace BaseAuthApp_BAL.Models
{
    public class UserCreateModel
    {
        [Required]
        [MaxLength(100)]
        public string Username { get; set; }
        [Required]
        [MaxLength(100)]
        public string Password { get; set; }
    }
}
