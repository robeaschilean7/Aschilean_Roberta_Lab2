using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Aschilean_Roberta_Lab2.Models;

namespace Aschilean_Roberta_Lab2.Pages.BorrowingPages;

public class CreateModel : PageModel
{
    private readonly Aschilean_Roberta_Lab2Context _context;

    public CreateModel(Aschilean_Roberta_Lab2Context context)
    {
        _context = context;
    }

    public IActionResult OnGet()
    {
        PopulateDropDowns();
        return Page();
    }

    private void PopulateDropDowns()
    {
        var bookList = _context.Book
            .Include(b => b.Author)
            .Select(x => new
            {
                x.ID,
                BookFullName = x.Title + " - " + x.Author.LastName + " " + x.Author.FirstName
            });

        ViewData["BookID"] = new SelectList(bookList, "ID", "BookFullName");
        ViewData["MemberID"] = new SelectList(_context.Member, "ID", "FullName");
    }

    [BindProperty]
    public Borrowing Borrowing { get; set; } = default!;

    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            PopulateDropDowns();
            return Page();
        }

        _context.Borrowing.Add(Borrowing);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
