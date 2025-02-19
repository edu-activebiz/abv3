using Microsoft.AspNetCore.Identity;

namespace FMCGWebApplication.Models
{
    public class ApplicationUser : IdentityUser
    {     // Additional custom properties
        public string FullName { get; set; } = string.Empty;  // User's full name
        public string ProfilePictureUrl { get; set; } = string.Empty;  // Profile image URL
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;  // Account creation date   
        //public string? FullName { get; set; }

        //public string? Group { get; set; } // For role-based filtering
    }

}