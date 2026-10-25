using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Aschilean_Roberta_Lab2.Models;

namespace Aschilean_Roberta_Lab2.Pages.Categories;

public class DetailsModel : PageModel
{
    private readonly Aschilean_Roberta_Lab2Context _context;
    public DetailsModel(Aschilean_Roberta_Lab2Context context)
    {
        _context = context;
    }

    public Category Category { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var category = await _context.Category.FirstOrDefaultAsync(m => m.ID == id);
        if (category is null)
        {
            return NotFound();
        }
        else
        {
            Category = category;
        }

        return Page();
    }
}
