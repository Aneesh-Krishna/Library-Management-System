using LibraryManagementSystem.Data;
using LibraryManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Controllers
{
    public class BorrowingRecordController : Controller
    {
        private readonly ApplicationDbContext _context;
        
        public BorrowingRecordController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var borrowingRecords = await _context.BorrowingRecords.ToListAsync();
            if(borrowingRecords == null)
            {
                return View();
            }
            return View(borrowingRecords);  
        }
        public IActionResult BorrowBook()
        {
            ViewData["Books"] = new SelectList(_context.Books, "BookId", "Title");
            ViewData["LibraryMembers"] = new SelectList(_context.LibraryMembers, "LibraryMemberId", "FullName");
            return View();
        }

        [HttpPost, ActionName("BorrowBook")]
        [ValidateAntiForgeryToken]  
        public async Task<IActionResult> BorrowBook(BorrowingRecord borrowingRecord)
        {
            var book = await _context.Books.FindAsync(borrowingRecord.BookId);
            var member = await _context.LibraryMembers.FindAsync(borrowingRecord.LibraryMemberId);
            if (book == null || member==null)
            {
                return NotFound();
            }
            if (book.AvailableCopies <= 0)
                return View("NoCopiesAvailable");

            borrowingRecord.BorrowedDate = DateTime.Now;
            borrowingRecord.DueDate = DateTime.Now.AddDays(14);

            book.AvailableCopies -= 1;
            _context.BorrowingRecords.Add(borrowingRecord);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> ReturnBook(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var record = await _context.BorrowingRecords
                .Include(br => br.Book)
                .Include(br => br.LibraryMember)
                .FirstOrDefaultAsync(br => br.BorrowingRecordId == id);

            if (record == null)
            {
                return NotFound();
            }

            return View(record); // Return a view showing the record details
        }

        [HttpPost, ActionName("ReturnBook")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ReturnBookConfirmed(int id)
        {
            var record= await _context.BorrowingRecords
                .Include(br => br.Book)
                .FirstOrDefaultAsync(br => br.BorrowingRecordId == id);
         
            if (record == null)
            {
                return NotFound();
            }

            record.ReturnedDate = DateTime.Now;

            if(record.Book != null)
            {
                record.Book.AvailableCopies += 1;
                _context.Books.Update(record.Book);
            }
            _context.BorrowingRecords.Update(record);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var borrowingRecords = await _context.BorrowingRecords
                .Include(b => b.Book)
                .Include(b => b.LibraryMember)
                .FirstOrDefaultAsync(br => br.BorrowingRecordId==id);
            if(borrowingRecords == null)
            {
                return NotFound();
            }

            return View(borrowingRecords);
        }
    }
}
