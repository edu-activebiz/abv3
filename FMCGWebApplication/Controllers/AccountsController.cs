using FMCGWebApplication.Data;
using FMCGWebApplication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Linq;
using System.Threading.Tasks;

namespace FMCGWebApplication.Controllers
{
    [Authorize] // Ensure authentication is required
    public class AccountsController : Controller
    {

        private readonly SystemDbContext _context;

        public AccountsController(SystemDbContext context)
        {
            _context = context;
        }
        //public IActionResult Index()
        //{
        //    return View();
        //}

        public async Task<IActionResult> Index(string search, string sortOrder, string filter, int page = 1, int pageSize = 10)
        {
           // ViewBag.CurrentAreaGroup = accountGroupFilter;

            var accounts = _context.TISAccountMsts.AsQueryable();

            // Search functionality
            if (!string.IsNullOrEmpty(search))
            {
                accounts = accounts.Where(a => a.AcName.Contains(search) || a.AcCode.Contains(search));
            }

            //// Filtering by Area Group
            //if (!string.IsNullOrEmpty(filter))
            //{
            //    areas = areas.Where(a => a.AreaGroup == filter);
            //}

            ////// Filter by Area Group
            ////if (!string.IsNullOrEmpty(accounts))
            ////{
            ////    accounts = accounts.Where(a => a.AreaGroup == accounts);
            ////}
            ////// Get distinct Area Groups for filtering dropdown
            ////ViewBag.AreaGroups = await _context.TISAreaMsts.Select(a => a.AreaGroup).Distinct().ToListAsync();

            // Sorting logic
            ViewData["NameSortParam"] = sortOrder == "name" ? "name_desc" : "name";
            ViewData["CodeSortParam"] = sortOrder == "code" ? "code_desc" : "code";

            accounts = sortOrder switch
            {
                "name" => accounts.OrderBy(a => a.AcName),
                "name_desc" => accounts.OrderByDescending(a => a.AcName),
                "code" => accounts.OrderBy(a => a.AreaCode),
                "code_desc" => accounts.OrderByDescending(a => a.AreaCode),
                _ => accounts.OrderBy(a => a.AreaCode)
            };

            // Pagination
            int totalItems = await accounts.CountAsync();
            var paginatedAreas = await accounts.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            ViewData["TotalItems"] = totalItems;
            ViewData["PageSize"] = pageSize;
            ViewData["CurrentPage"] = page;

            return View("Index", paginatedAreas);
        }

        public async Task<IActionResult> Details(short companyCode, string acCode)
        {
            var account = await _context.TISAccountMsts
               .FirstOrDefaultAsync(a => a.CompanyCode == companyCode && a.AcCode == acCode);
            if (account == null) return NotFound();
            return View(account);
        }
        public async Task<IActionResult> Create()
        {
            // Get distinct Area Groups for filtering dropdown
            //// var areaGroups = await _context.TISAccountMsts.Select(a => a.AreaGroup).Distinct().ToListAsync();

            // Send areaGroups to the view as a SelectList
            //// ViewBag.AreaGroups = new SelectList(areaGroups);
            ViewData["GroupCode"] = new SelectList(await _context.TISGroupMsts.ToListAsync(), "GroupCode", "GroupName");
            //ViewData["StateName"] = new SelectList(await _context.TISS.ToListAsync(), "Id", "StateName");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Accounts account)
        {
            //string companyCode = 
            account.CompanyCode = Convert.ToInt16(GlobalVariables.SelectedCompany);
            if (ModelState.IsValid)
            {
                _context.Add(account);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            // If validation fails, reload the GroupCode dropdown
            ViewData["GroupCode"] = new SelectList(await _context.TISGroupMsts.ToListAsync(), "GroupCode", "GroupName", account.GroupCode);
            return View(account);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(short companyCode, string acCode)
        {
            //int companyCode = Convert.ToInt16(GlobalVariables.SelectedCompany);
            string comp = companyCode.ToString();
            //var area = await _context.TISAreaMsts.FindAsync(companyCode.ToString(), areaCode);
            //var area = await _context.TISAreaMsts.FindAsync(comp, areaCode);
            var account = await _context.TISAccountMsts
    .FirstOrDefaultAsync(a => a.CompanyCode == companyCode && a.AcCode == acCode);
            ////.AsQueryable();

            if (account == null) return NotFound();
            // Fetch distinct AreaGroup values for dropdown
            //var areaGroups = await _context.TISAreaMsts
            //    .Where(a => a.AreaGroup != null)
            //    .Select(a => a.AreaGroup)
            //    .Distinct()
            //    .ToListAsync();

            //ViewBag.AreaGroups = new SelectList(areaGroups, account.AreaCode); // Pre-select current value


            return View(account);
            //return View("~/Views/TISAreaMst/Edit.cshtml", area);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short companyCode, string acCode, Accounts account)
        {
            if (companyCode != account.CompanyCode && acCode != account.AcCode) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(account);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(account);
        }
        public async Task<IActionResult> Delete(short companyCode, string acCode)//short id)
        {
            var account = await _context.TISAccountMsts
   .FirstOrDefaultAsync(a => a.CompanyCode == companyCode && a.AreaCode == acCode);

           
            if (account == null) return NotFound();
            return View(account);
        }


        [HttpPost]//, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(short companyCode, string acCode)
        {
            var account = await _context.TISAccountMsts
   .FirstOrDefaultAsync(a => a.CompanyCode == companyCode && a.AcCode == acCode);
           
            if (account != null)
            {
                _context.TISAccountMsts.Remove(account);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }


    }
}
