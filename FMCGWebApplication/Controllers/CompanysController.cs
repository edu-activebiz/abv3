//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using WebFMCGSystem.Data;
//using WebFMCGSystem.Models;
//using Microsoft.AspNetCore.Mvc.Rendering;


//namespace WebFMCGSystem.Controllers
//{
//    [Authorize]
//    //[Authorize(Roles = "Admin,User")] // Role-based access
//    public class CompanysController : Controller
//    {
//        private readonly SystemDbContext _context;

//        public CompanysController(SystemDbContext context)
//        {
//            _context = context;
//        }
//        [HttpGet]
//        public async Task<IActionResult> Index()
//        {
//            var modal = await _context.TISCompanyMsts.ToListAsync();
//            return View("~/Views/TISCompanyMst/Index.cshtml", modal);
//        }

//        [HttpGet]
//        public async Task<IActionResult> Details(short id)
//        {
//            var company = await _context.TISCompanyMsts.FindAsync(id);
//            if (company == null)
//            {
//                return NotFound();
//            }
//            return View("~/Views/TISCompanyMst/Details.cshtml", company);
//        }

//        //[Authorize(Roles = "Admin")]
//        public async Task<IActionResult> CreateAsync()
//        {
//            return View("~/Views/TISCompanyMst/Create.cshtml");
//        }

//        //[HttpPost]
//        //[ValidateAntiForgeryToken]
//        //[Authorize(Roles = "Admin")]
//        //public async Task<IActionResult> CreateAsync(Company company)
//        //{
//        //    if (ModelState.IsValid)
//        //    {
//        //        //_context.Add(company);
//        //        //var companys = await _context.SaveChangesAsync();
//        //        return View("~/Views/TISCompanyMst/Create.cshtml");
//        //        //return RedirectToAction(nameof(Index));
//        //    }
//        //    return View(company);
//        //}

//        [HttpPost]
//        public async Task<IActionResult> Create(Company company)
//        {
//            if (ModelState.IsValid)
//            {
//                _context.TISCompanyMsts.Add(company);
//                await _context.SaveChangesAsync();
//                return RedirectToAction(nameof(Index));
//            }
//            return View(company);
//        }

//        [Authorize(Roles = "Admin")]
//        public async Task<IActionResult> Edit(short id)
//        {
//            var company = await _context.TISCompanyMsts.FindAsync(id);
//            if (company == null)
//            {
//                return NotFound();
//            }
//            return View("~/Views/TISCompanyMst/Edit.cshtml", company);
//        }

//        [HttpPost]
//        [Authorize(Roles = "Admin")]
//        public async Task<IActionResult> Edit(short id, Company company)
//        {
//            if (id != company.CompanyCode)
//            {
//                return NotFound();
//            }

//            if (ModelState.IsValid)
//            {
//                _context.Update(company);
//                await _context.SaveChangesAsync();
//                return RedirectToAction(nameof(Index));
//            }
//            return View("~/Views/TISCompanyMst/Edit.cshtml", company);
//        }

//        [Authorize(Roles = "Admin")]
//        public async Task<IActionResult> Delete(short id)
//        {
//            var company = await _context.TISCompanyMsts.FindAsync(id);
//            if (company == null)
//            {
//                return NotFound();
//            }
//            return View("~/Views/TISCompanyMst/Delete.cshtml", company);
//        }

//        [HttpPost, ActionName("Delete")]
//        [Authorize(Roles = "Admin")]
//        public async Task<IActionResult> DeleteConfirmed(short id)
//        {
//            var company = await _context.TISCompanyMsts.FindAsync(id);
//            if (company != null)
//            {
//                _context.TISCompanyMsts.Remove(company);
//                await _context.SaveChangesAsync();
//            }
//            return RedirectToAction(nameof(Index));
//        }
//    }
//}
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FMCGWebApplication.Data;
using FMCGWebApplication.Models;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Rendering;



namespace FMCGWebApplication.Controllers
{
    //[Authorize] // Ensures only authenticated users can access
    public class CompanysController : Controller
    {
        private readonly SystemDbContext _context;
        private readonly ILogger<CompanysController> _logger;
        public CompanysController(SystemDbContext context, ILogger<CompanysController> logger)
        {
            _context = context;
            _logger = logger;
        }

       

        // GET: List of Companies
        //[HttpGet]
        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("Company Index method started");
            try
            {
                var companies = await _context.TISCompanyMsts.ToListAsync();
                _logger.LogInformation($"Fetched {companies.Count} companies from DB.");
                return View("Index",companies);
               // return RedirectToAction("Index", "Home"); //return View("~/Views/Home/Index.cshtml", companies);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in Index: {ex.Message}");
                return RedirectToAction("Error", "Home");
            }

            // var companies = await _context.TISCompanyMsts.ToListAsync();
            // // return View("Index", "Companys");
            // //return View("~/Views/Home/Index.cshtml", "Home");
            // return View("~/Views/TISCompanyMst/Index.cshtml", companies);
            //// return View();
        }

        // GET: Company Details
        //[HttpGet]
        public async Task<IActionResult> Details(short id)
        {
            var company = await _context.TISCompanyMsts.FindAsync(id);
            if (company == null)
            {
                return NotFound();
            }
            return View("~/Views/TISCompanyMst/Details.cshtml", company);
        }

        // GET: Create Company
        [Authorize(Roles = "Admin")] // Only Admins can access
        [HttpGet]
        public IActionResult Create()
        {
            return View("~/Areas/Admin/Views/TISCompanyMst/Create.cshtml");
        }

        // POST: Create Company
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken] // Prevents CSRF attacks
        public async Task<IActionResult> Create(Company company)
        {
            if (ModelState.IsValid)
            {
                _context.TISCompanyMsts.Add(company);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Company created successfully!";
                return RedirectToAction(nameof(Index));
            }
            return View("~/Areas/Admin/Views/TISCompanyMst/Create.cshtml", company);
        }

        // GET: Edit Company
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Edit(short id)
        {
            var company = await _context.TISCompanyMsts.FindAsync(id);
            if (company == null)
            {
                return NotFound();
            }
            return View("~/Areas/Admin/Views/TISCompanyMst/Edit.cshtml", company);
        }

        // POST: Edit Company
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short id, Company company)
        {
            if (id != company.CompanyCode)
            {
                return BadRequest("Invalid request.");
            }

            if (ModelState.IsValid)
            {
                _context.Update(company);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Company updated successfully!";
                return RedirectToAction(nameof(Index));
            }
            return View("~/Areas/Admin/Views/TISCompanyMst/Edit.cshtml", company);
        }

        // GET: Delete Confirmation Page
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Delete(short id)
        {
            var company = await _context.TISCompanyMsts.FindAsync(id);
            if (company == null)
            {
                return NotFound();
            }
            return View("~/Areas/Admin/Views/TISCompanyMst/Delete.cshtml", company);
        }

        // POST: Confirm Delete
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(short id)
        {
            var company = await _context.TISCompanyMsts.FindAsync(id);
            if (company != null)
            {
                _context.TISCompanyMsts.Remove(company);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Company deleted successfully!";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}

