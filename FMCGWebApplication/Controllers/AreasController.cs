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
    public class AreasController : Controller
    {
        private readonly SystemDbContext _context;

        public AreasController(SystemDbContext context)
        {
            _context = context;
        }

        //public async Task<IActionResult> Index()
        //{
        //    var areas = await _context.TISAreaMsts.Include(a => a.Companys).ToListAsync();
        //    return View(areas);
        //}
        public async Task<IActionResult> Index(string search, string areaGroupFilter, string sortOrder, string filter, int page = 1, int pageSize = 10)
        {
            ViewBag.CurrentAreaGroup = areaGroupFilter;

            var areas = _context.TISAreaMsts.AsQueryable();

            // Search functionality
            if (!string.IsNullOrEmpty(search))
            {
                areas = areas.Where(a => a.AreaName.Contains(search) || a.AreaCode.Contains(search));
            }

            //// Filtering by Area Group
            //if (!string.IsNullOrEmpty(filter))
            //{
            //    areas = areas.Where(a => a.AreaGroup == filter);
            //}

            // Filter by Area Group
            if (!string.IsNullOrEmpty(areaGroupFilter))
            {
                areas = areas.Where(a => a.AreaGroup == areaGroupFilter);
            }
            // Get distinct Area Groups for filtering dropdown
            ViewBag.AreaGroups = await _context.TISAreaMsts.Select(a => a.AreaGroup).Distinct().ToListAsync();

            // Sorting logic
            ViewData["NameSortParam"] = sortOrder == "name" ? "name_desc" : "name";
            ViewData["CodeSortParam"] = sortOrder == "code" ? "code_desc" : "code";

            areas = sortOrder switch
            {
                "name" => areas.OrderBy(a => a.AreaName),
                "name_desc" => areas.OrderByDescending(a => a.AreaName),
                "code" => areas.OrderBy(a => a.AreaCode),
                "code_desc" => areas.OrderByDescending(a => a.AreaCode),
                _ => areas.OrderBy(a => a.AreaCode)
            };

            // Pagination
            int totalItems = await areas.CountAsync();
            var paginatedAreas = await areas.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            ViewData["TotalItems"] = totalItems;
            ViewData["PageSize"] = pageSize;
            ViewData["CurrentPage"] = page;

            return View("Index", paginatedAreas);
        }

        public async Task<IActionResult> Details(short id)
        {
            var area = await _context.TISAreaMsts.FindAsync(id);
            if (area == null) return NotFound();
            return View(area);
        }
        //[Authorize(Roles = "Admin,Manager")] // Role-based access
        public async Task<IActionResult> CreateAsync()
        {
            // Get distinct Area Groups for filtering dropdown
            var areaGroups = await _context.TISAreaMsts.Select(a => a.AreaGroup).Distinct().ToListAsync();

            // Send areaGroups to the view as a SelectList
            ViewBag.AreaGroups = new SelectList(areaGroups);
        
        return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Area area)
        {
            //string companyCode = 
            area.CompanyCode = Convert.ToInt16(GlobalVariables.SelectedCompany);
            if (ModelState.IsValid)
            {
                _context.Add(area);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(area);
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
        public async Task<IActionResult> Edit(short companyCode, string areaCode)
        {
            //int companyCode = Convert.ToInt16(GlobalVariables.SelectedCompany);
            string comp = companyCode.ToString();
            //var area = await _context.TISAreaMsts.FindAsync(companyCode.ToString(), areaCode);
            //var area = await _context.TISAreaMsts.FindAsync(comp, areaCode);
            var area = await _context.TISAreaMsts
    .FirstOrDefaultAsync(a => a.CompanyCode == companyCode && a.AreaCode == areaCode);
            ////.AsQueryable();

            if (area == null) return NotFound();
                        // Fetch distinct AreaGroup values for dropdown
            var areaGroups = await _context.TISAreaMsts
                .Where(a => a.AreaGroup != null)
                .Select(a => a.AreaGroup)
                .Distinct()
                .ToListAsync();

           ViewBag.AreaGroups = new SelectList(areaGroups, area.AreaGroup); // Pre-select current value


            return View(area);
            //return View("~/Views/TISAreaMst/Edit.cshtml", area);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short companyCode, string areaCode, Area area)
        {
            if (companyCode != area.CompanyCode && areaCode != area.AreaCode) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(area);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(area);
        }


        //[Authorize(Roles = "Admin")]//comment
        public async Task<IActionResult> Delete(short companyCode, string areaCode)//short id)
        {
            var area = await _context.TISAreaMsts
   .FirstOrDefaultAsync(a => a.CompanyCode == companyCode && a.AreaCode == areaCode);

           // var area = await _context.TISAreaMsts.FindAsync(companyCode, areaCode);//(id);
            if (area == null) return NotFound();
            return View(area);
        }
             

        [HttpPost]//, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(short companyCode, string areaCode)
        {
            var area = await _context.TISAreaMsts
   .FirstOrDefaultAsync(a => a.CompanyCode == companyCode && a.AreaCode == areaCode);
            //var area = await _context.TISAreaMsts.FindAsync(id);
            if (area != null)
            {
                _context.TISAreaMsts.Remove(area);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}






   

    

    


    



