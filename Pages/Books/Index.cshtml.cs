using Aschilean_Roberta_Lab2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Aschilean_Roberta_Lab2.Pages.Books;

public class IndexModel : PageModel
{
    private readonly Aschilean_Roberta_Lab2Context _context;

    public IndexModel(Aschilean_Roberta_Lab2Context context)
    {
        _context = context;
    }

    public BookData BookD { get; set; } = new BookData();
    public int BookID { get; set; }
    public int CategoryID { get; set; }

    [BindProperty(SupportsGet = true)]
    public int? AuthorFilterID { get; set; }

    public SelectList AuthorList { get; set; } = default!;

    public async Task OnGetAsync(int? id, int? categoryID)
    {
        BookD = new BookData();
        AuthorList = new SelectList(_context.Set<Author>(), "ID", "FullName");

        var query = _context.Book
            .Include(b => b.Author)
            .Include(b => b.Publisher)
            .Include(b => b.BookCategories)
            .ThenInclude(b => b.Category)
            .AsNoTracking()
            .AsQueryable();

        if (AuthorFilterID.HasValue)
        {
            query = query.Where(b => b.AuthorID == AuthorFilterID);
        }

        BookD.Books = await query.OrderBy(b => b.Title).ToListAsync();

        if (id != null)
        {
            BookID = id.Value;
            Book? book = BookD.Books.FirstOrDefault(i => i.ID == id.Value);
            if (book != null && book.BookCategories != null)
            {
                BookD.Categories = book.BookCategories.Select(s => s.Category);
            }
        }
    }
}