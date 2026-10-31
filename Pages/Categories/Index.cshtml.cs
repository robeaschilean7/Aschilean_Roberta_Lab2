using Aschilean_Roberta_Lab2.Models;
using Aschilean_Roberta_Lab2.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Aschilean_Roberta_Lab2.Pages.Categories;

public class IndexModel : PageModel
{
    private readonly Aschilean_Roberta_Lab2Context _context;

    public IndexModel(Aschilean_Roberta_Lab2Context context)
    {
        _context = context;
    }

    public IList<Category> Category { get; set; } = default!;
    public CategoryIndexData CategoryData { get; set; }
    public int CategoryID { get; set; }

    public async Task OnGetAsync(int? id)
    {
        CategoryData = new CategoryIndexData();

        CategoryData.Categories = await _context.Category
            .Include(c => c.BookCategories)
                .ThenInclude(bc => bc.Book)
                    .ThenInclude(b => b.Author)
            .OrderBy(c => c.CategoryName)
            .ToListAsync();

        if (id != null)
        {
            CategoryID = id.Value;
            Category category = CategoryData.Categories
                .Where(c => c.ID == id.Value).Single();

            CategoryData.Books = category.BookCategories.Select(bc => bc.Book);
        }
    }
}