using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FMCGWebApplication.Data;
using FMCGWebApplication.Models;

namespace FMCGWebApplication.Controllers
{
    public class TISStateController : Controller
    {
        private readonly SystemDbContext _context;

        public TISStateController(SystemDbContext context)
        {
            _context = context;
        }

        // GET: TISState
        public async Task<IActionResult> Index()
        {
            return View(await _context.TISStates.ToListAsync());
        }

        // GET: TISState/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TISState/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StateName,CountryId")] TISState tisState)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tisState);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tisState);
        }

        // GET: TISState/Edit/5
        public async Task<IActionResult> Edit(string stateName)
        {
            if (stateName == null) return NotFound();

            var tisState = await _context.TISStates.FindAsync(stateName);
            if (tisState == null) return NotFound();

            return View(tisState);
        }

        // POST: TISState/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string stateName, [Bind("Id,StateName,CountryId")] TISState tisState)
        {
            if (stateName != tisState.StateName) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tisState);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.TISStates.Any(e => e.StateName == tisState.StateName))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tisState);
        }

        // GET: TISState/Delete/5
        public async Task<IActionResult> Delete(string stateName)
        {
            if (stateName == null) return NotFound();

            var tisState = await _context.TISStates.FirstOrDefaultAsync(m => m.StateName == stateName);
            if (tisState == null) return NotFound();

            return View(tisState);
        }

        // POST: TISState/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string stateName)
        {
            var tisState = await _context.TISStates.FindAsync(stateName);
            if (tisState != null)
            {
                _context.TISStates.Remove(tisState);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
