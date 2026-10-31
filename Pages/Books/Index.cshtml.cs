using Aschilean_Roberta_Lab2.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
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
    public BookData BookD { get; set; } = new BookData();
    public int BookID { get; set; }
    public int CategoryID { get; set; }

    public string TitleSort { get; set; } = "";
    public string AuthorSort { get; set; } = "";
    public string CurrentFilter { get; set; } = "";

    public async Task OnGetAsync(int? id, int? categoryID, string? sortOrder, string? searchString)
    {
        BookD = new BookData();

        TitleSort = string.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
        AuthorSort = sortOrder == "author" ? "author_desc" : "author";
        CurrentFilter = searchString ?? "";

        var books = await _context.Book
            .Include(b => b.Author)
            .Include(b => b.Publisher)
            .Include(b => b.BookCategories!)
                .ThenInclude(b => b.Category)
            .AsNoTracking()
            .OrderBy(b => b.Title)
            .ToListAsync();

        IEnumerable<Book> result = books;

        if (!string.IsNullOrWhiteSpace(searchString))
        {
            result = result.Where(s =>
                (s.Title ?? "").Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                (s.Author?.FirstName ?? "").Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                (s.Author?.LastName ?? "").Contains(searchString, StringComparison.OrdinalIgnoreCase));
        }

        switch (sortOrder)
        {
            case "title_desc":
                result = result.OrderByDescending(s => s.Title);
                break;
            case "author_desc":
                result = result.OrderByDescending(s => s.Author?.FullName);
                break;
            case "author":
                result = result.OrderBy(s => s.Author?.FullName);
                break;
            default:
                result = result.OrderBy(s => s.Title);
                break;
        }

        BookD.Books = result.ToList();

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