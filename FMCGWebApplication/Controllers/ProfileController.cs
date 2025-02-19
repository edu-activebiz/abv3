using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System.Security.Claims;
using FMCGWebApplication.Models;

namespace FMCGWebApplication.Controllers
{
    //[Authorize]
    public class ProfileController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account"); // Redirect to login if not authenticated
            }

            // Debugging: Log all claims
            Console.WriteLine("=== User Claims ===");
            foreach (var claim in User.Claims)
            {
                Console.WriteLine($"Claim Type: {claim.Type}, Claim Value: {claim.Value}");
            }
            Console.WriteLine("===================");
            
            var token = User.FindFirst("Token")?.Value;
            Console.WriteLine($"Retrieved Token: {token}");

            var username = User.Identity.Name; // Gets the logged-in user's username
            var email = User.FindFirst(ClaimTypes.Email)?.Value; // Gets email claim
            var role = User.FindFirst(ClaimTypes.Role)?.Value; // Gets role claim
            var token1 = token ?? string.Empty;
            //var token = User.FindFirst("Token")?.Value.ToString(); // Gets token claim

            // Retrieve selected company from cookies
            //short selectedCompany = Request.Cookies["SelectedCompany"];
            string selectedCompanyName = Request.Cookies["SelectedCompanyName"];

            var model = new ProfileViewModel
            {
                Username = username,
                Email = email,
                Role = role,
                Token = token,

              //  SelectedCompany = selectedCompany,
                SelectedCompanyName = selectedCompanyName
            };

            return View();// "Index", "Home");
        }
        //[HttpGet("profile")]
        //public IActionResult Profile()
        //{
        //    var expirationClaim = User.FindFirst("TokenExpiration");
        //    if (expirationClaim != null && DateTime.UtcNow > DateTime.Parse(expirationClaim.Value))
        //    {
        //        return RedirectToAction("Login", "Account");  // Redirect if token expired
        //    }

        //    return View();
        //}
    }
}
