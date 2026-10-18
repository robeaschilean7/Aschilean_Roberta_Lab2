using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Aschilean_Roberta_Lab2.Models;

namespace Aschilean_Roberta_Lab2.Pages.Authors;

public class DeleteModel : PageModel
{
    private readonly Aschilean_Roberta_Lab2Context _context;

    public DeleteModel(Aschilean_Roberta_Lab2Context context)
    {
        _context = context;
    }

    [BindProperty]
    public Author Authors { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var authors = await _context.Authors.FirstOrDefaultAsync(m => m.ID == id);
        if (authors is null)
        {
            return NotFound();
        }
        else
        {
            Authors = authors;
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var authors = await _context.Authors.FindAsync(id);
        if (authors != null)
        {
            Authors = authors;
            _context.Authors.Remove(Authors);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}
