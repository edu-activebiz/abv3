using System.ComponentModel.DataAnnotations;

namespace FMCGWebApplication.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; } = string.Empty;  // ✅ Default value added

        [Required(ErrorMessage = "Password is required.")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters.")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;  // ✅ Default value added

        public bool RememberMe { get; set; }  // ✅ Add Remember Me
    }
}
