using Microsoft.EntityFrameworkCore;

public class Aschilean_Roberta_Lab2Context(DbContextOptions<Aschilean_Roberta_Lab2Context> options) : DbContext(options)
{
    public DbSet<Aschilean_Roberta_Lab2.Models.Book> Book { get; set; } = default!;
    public DbSet<Aschilean_Roberta_Lab2.Models.Publisher> Publisher { get; set; } = default!;

    public DbSet<Aschilean_Roberta_Lab2.Models.Author> Authors { get; set; } = default!;
}
