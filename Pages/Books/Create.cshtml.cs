using Aschilean_Roberta_Lab2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Aschilean_Roberta_Lab2.Pages.Books;

public class CreateModel : BookCategoriesPageModel
{
    private readonly Aschilean_Roberta_Lab2Context _context;

    public CreateModel(Aschilean_Roberta_Lab2Context context)
    {
        _context = context;
    }

    [BindProperty]
    public Book Book { get; set; } = default!;

    public IActionResult OnGet()
    {
        LoadDropdowns();

        var book = new Book();
        book.BookCategories = new List<BookCategory>();
        PopulateAssignedCategoryData(_context, book);
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(string[] selectedCategories)
    {
        var newBook = new Book();
        newBook.BookCategories = new List<BookCategory>();

        if (selectedCategories != null)
        {
            foreach (var cat in selectedCategories)
            {
                newBook.BookCategories.Add(new BookCategory
                {
                    CategoryID = int.Parse(cat)
                });
            }
        }

        Book.BookCategories = newBook.BookCategories;

        if (!ModelState.IsValid)
        {
            LoadDropdowns();
            PopulateAssignedCategoryData(_context, Book);
            return Page();
        }

        _context.Book.Add(Book);
        await _context.SaveChangesAsync();
        return RedirectToPage("./Index");
    }

    private void LoadDropdowns()
    {
        ViewData["AuthorID"] = new SelectList(_context.Set<Author>(), "ID", "FullName");
        ViewData["PublisherID"] = new SelectList(_context.Set<Publisher>(), "ID", "PublisherName");
    }
}