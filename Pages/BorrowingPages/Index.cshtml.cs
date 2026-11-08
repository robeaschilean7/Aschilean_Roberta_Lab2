using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Aschilean_Roberta_Lab2.Models;

namespace Aschilean_Roberta_Lab2.Pages.BorrowingPages;

public class IndexModel : PageModel
{
    private readonly Aschilean_Roberta_Lab2Context _context;

    public IndexModel(Aschilean_Roberta_Lab2Context context)
    {
        _context = context;
    }

    public IList<Borrowing> Borrowing { get; set; } = default!;

    public async Task OnGetAsync()
    {
        if (_context.Borrowing != null)
        {
            Borrowing = await _context.Borrowing
                .Include(b => b.Book)
                    .ThenInclude(b => b.Author)
                .Include(b => b.Member)
                .ToListAsync();
        }
    }
}
