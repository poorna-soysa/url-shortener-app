namespace UrlShortener.Api.Database;

public sealed class ApplicationDbContext(DbContextOptions options)
    : DbContext(options)
{
    public DbSet<ShortenUrl> ShortenUrls { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ShortenUrl>(builder =>
        {
            builder.HasIndex(p => p.UniqueCode)
            .IsUnique();
        });
    }
}
