using FMCGWebApplication.Data;
using FMCGWebApplication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;


namespace FMCGWebApplication.Controllers
{
    [Authorize] // Ensure authentication is required
    public class GroupsController : Controller
    {
        private readonly SystemDbContext _context;

        public GroupsController(SystemDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(string search, string groupGroupFilter, string sortOrder, string filter, int page = 1, int pageSize = 10)
        {
            ViewBag.CurrentAreaGroup = groupGroupFilter;

            var groups = _context.TISGroupMsts.AsQueryable();

            // Search functionality
            if (!string.IsNullOrEmpty(search))
            {
                groups = groups.Where(a => a.GroupName.Contains(search) || a.GroupCode.Contains(search));
            }

            //// Filtering by Area Group
            //if (!string.IsNullOrEmpty(filter))
            //{
            //    areas = areas.Where(a => a.AreaGroup == filter);
            //}

            //// Filter by Area Group
            //if (!string.IsNullOrEmpty(areaGroupFilter))
            //{
            //    groups = groups.Where(a => a.AreaGroup == areaGroupFilter);
            //}
            //// Get distinct Area Groups for filtering dropdown
            //ViewBag.AreaGroups = await _context.TISGroupMsts.Select(a => a.AreaGroup).Distinct().ToListAsync();

            // Sorting logic
            ViewData["NameSortParam"] = sortOrder == "name" ? "name_desc" : "name";
            ViewData["CodeSortParam"] = sortOrder == "code" ? "code_desc" : "code";

            groups = sortOrder switch
            {
                "name" => groups.OrderBy(a => a.GroupName),
                "name_desc" => groups.OrderByDescending(a => a.GroupName),
                "code" => groups.OrderBy(a => a.GroupCode),
                "code_desc" => groups.OrderByDescending(a => a.GroupCode),
                _ => groups.OrderBy(a => a.GroupCode)
            };

            // Pagination
            int totalItems = await groups.CountAsync();
            var paginatedAreas = await groups.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            ViewData["TotalItems"] = totalItems;
            ViewData["PageSize"] = pageSize;
            ViewData["CurrentPage"] = page;

            return View("Index", paginatedAreas);
        }

        public async Task<IActionResult> Details(short id)
        {
            var group = await _context.TISGroupMsts.FindAsync(id);
            if (group == null) return NotFound();
            return View(group);
        }
        //[Authorize(Roles = "Admin,Manager")] // Role-based access
        public async Task<IActionResult> CreateAsync()
        {
            //// Get distinct Area Groups for filtering dropdown
            //var areaGroups = await _context.TISGroupMsts.Select(a => a.GroupCode).Distinct().ToListAsync();

            //// Send areaGroups to the view as a SelectList
            //ViewBag.AreaGroups = new SelectList(areaGroups);
        
        return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Groups group)
        {
            //string companyCode = 
            group.CompanyCode = Convert.ToInt16(GlobalVariables.SelectedCompany);
            if (ModelState.IsValid)
            {
                _context.Add(group);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(group);
        }
        //[Authorize(Roles = "Admin,Manager")]//comment
        //public async Task<IActionResult> Edit(short id)
        //{
        //    var area = await _context.TISAreaMsts.FindAsync(id);
        //    if (area == null) return NotFound();
        //    return View(area);
        //}

        // EDIT: Show form"
        [HttpGet]
        public async Task<IActionResult> Edit(short companyCode, string groupCode)
        {
          
            string comp = companyCode.ToString();
         
            var group = await _context.TISGroupMsts
    .FirstOrDefaultAsync(a => a.CompanyCode == companyCode && a.GroupCode == groupCode);
            ////.AsQueryable();

            if (group == null) return NotFound();
                        // Fetch distinct AreaGroup values for dropdown
           // var areaGroups = await _context.TISGroupMsts
           //     .Where(a => a.GroupCode != null)
           //     .Select(a => a.AreaGroup)
           //     .Distinct()
           //     .ToListAsync();

           //ViewBag.AreaGroups = new SelectList(areaGroups, area.AreaGroup); // Pre-select current value


            return View(group);
           
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short companyCode, string groupCode, Groups group)
        {
            if (companyCode != group.CompanyCode && groupCode != group.GroupCode) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(group);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(group);
        }


        //[Authorize(Roles = "Admin")]//comment
        public async Task<IActionResult> Delete(short companyCode, string groupCode)//short id)
        {
            var group = await _context.TISGroupMsts
   .FirstOrDefaultAsync(a => a.CompanyCode == companyCode && a.GroupCode == groupCode);

           // var area = await _context.TISAreaMsts.FindAsync(companyCode, areaCode);//(id);
            if (group == null) return NotFound();
            return View(group);
        }
             

        [HttpPost]//, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(short companyCode, string groupCode)
        {
            var group = await _context.TISGroupMsts
   .FirstOrDefaultAsync(a => a.CompanyCode == companyCode && a.GroupCode == groupCode);
         
            if (group != null)
            {
                _context.TISGroupMsts.Remove(group);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}