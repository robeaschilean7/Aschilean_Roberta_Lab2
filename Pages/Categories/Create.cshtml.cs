using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Aschilean_Roberta_Lab2.Models;

namespace Aschilean_Roberta_Lab2.Pages.Categories;

public class CreateModel : PageModel
{
    private readonly Aschilean_Roberta_Lab2Context _context;

    public CreateModel(Aschilean_Roberta_Lab2Context context)
    {
        _context = context;
    }

    public IActionResult OnGet()
    {
        return Page();
    }

    [BindProperty]
    public Category Category { get; set; } = default!;

    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _context.Category.Add(Category);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
