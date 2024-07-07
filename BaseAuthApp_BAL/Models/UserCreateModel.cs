using System.ComponentModel.DataAnnotations;

namespace BaseAuthApp_BAL.Models
{
    public class UserCreateModel
    {
        [Required(ErrorMessage = "Username is required.")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Username must be between 5 and 100 characters.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 100 characters.")]
        public string Password { get; set; }
    }
}
