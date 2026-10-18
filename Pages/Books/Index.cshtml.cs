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

    public IList<Book> Book { get; set; } = default!;

    [BindProperty(SupportsGet = true)]
    public int? AuthorFilterID { get; set; }

    public SelectList AuthorList { get; set; } = default!;

    public async Task OnGetAsync()
    {
        AuthorList = new SelectList(_context.Set<Author>(), "ID", "FullName");

        var query = _context.Book
            .Include(b => b.Author)
            .Include(b => b.Publisher)
            .AsQueryable();

        if (AuthorFilterID.HasValue)
        {
            query = query.Where(b => b.AuthorID == AuthorFilterID);
        }

        Book = await query.ToListAsync();
    }
}