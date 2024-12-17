using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

public class LibraryController : Controller
{
    private readonly LibraryContext _context;

    public LibraryController(LibraryContext context)
    {
        _context = context;
    }

    // Index action to list books with optional search functionality
    public async Task<IActionResult> Index(string searchString)
    {
        var books = from b in _context.Books
                    select b;

        if (!string.IsNullOrEmpty(searchString))
        {
            books = books.Where(b => b.Title.Contains(searchString) || b.Author.Contains(searchString));
        }

        return View(await books.ToListAsync());
    }


    // Action to borrow a book
    public async Task<IActionResult> BorrowBook(int id, int borrowerId)
    {
        var book = await _context.Books.FindAsync(id);
        var borrower = await _context.Borrowers.FindAsync(borrowerId);

        if (book != null && borrower != null && !book.IsBorrowed)
        {
            book.IsBorrowed = true;
            book.BorrowerId = borrowerId;
            book.DueDate = DateTime.Now.AddDays(14);

            _context.BorrowingHistories.Add(new BorrowingHistory
            {
                BookId = book.Id,
                BorrowedDate = DateTime.Now,
                DueDate = book.DueDate.Value
            });

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return NotFound();
    }

    // Action to return a book
    public async Task<IActionResult> ReturnBook(int id)
    {
        var book = await _context.Books.FindAsync(id);
        if (book != null && book.IsBorrowed)
        {
            book.IsBorrowed = false;
            book.BorrowerId = null;
            book.DueDate = null;

            var history = await _context.BorrowingHistories
                .FirstOrDefaultAsync(h => h.BookId == book.Id && h.ReturnedDate == null);

            if (history != null)
            {
                history.ReturnedDate = DateTime.Now;
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return NotFound();
    }

    // Action to view borrowing history
    public async Task<IActionResult> BorrowingHistory()
    {
        var history = await _context.BorrowingHistories
            .Include(h => h.Book)  // Include the related Book entity
            .ToListAsync();

        return View(history);
    }
}
