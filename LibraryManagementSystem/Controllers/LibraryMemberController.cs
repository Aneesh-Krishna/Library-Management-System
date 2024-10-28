using LibraryManagementSystem.Data;
using LibraryManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Controllers
{
    public class LibraryMemberController : Controller
    {
        private readonly ApplicationDbContext _context;
        public LibraryMemberController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.LibraryMembers.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LibraryMember libraryMember)
        {
            if(ModelState.IsValid)
            {
                _context.LibraryMembers.Add(libraryMember);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(libraryMember);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var libraryMember = await _context.LibraryMembers.FindAsync(id);
            if (libraryMember == null)
            {
                return NotFound();
            }

            return View(libraryMember);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, LibraryMember libraryMember)
        {
            if(id != libraryMember.LibraryMemberId)
            {
                return NotFound();
            }

            if(ModelState.IsValid)
            {
                try
                {
                    _context.LibraryMembers.Update(libraryMember);
                    await _context.SaveChangesAsync();
                }
                catch(DbUpdateConcurrencyException)
                {
                    if(! _context.LibraryMembers.Any(lm => lm.LibraryMemberId==id))
                        return NotFound();
                    
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(libraryMember);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if(id == null)
                return NotFound();
            
            var libraryMember = await _context.LibraryMembers.FindAsync(id);
            if(libraryMember == null)
            {
                return NotFound();
            }
            return View(libraryMember);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var libraryMember= await _context.LibraryMembers.FirstOrDefaultAsync(lm => lm.LibraryMemberId == id);
            if (libraryMember == null)
                return NotFound();

            _context.LibraryMembers.Remove(libraryMember);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if(id == null)
                return NotFound();

            var libraryMember= await _context.LibraryMembers.FirstOrDefaultAsync(lm => lm.LibraryMemberId == id);
            if(libraryMember == null)
                return NotFound();

            return View(libraryMember);
        }
    }
}
