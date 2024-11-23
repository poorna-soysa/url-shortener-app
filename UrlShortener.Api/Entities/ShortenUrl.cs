namespace UrlShortener.Api.Entities;

public sealed class ShortenUrl
{
    public Guid Id { get; set; }
    public string LongUrl { get; set; } = string.Empty;
    public string UniqueCode { get; set; } = string.Empty;
    public string ShortUrl { get; set; } = string.Empty;
    public DateTime CreatedOnUtc { get; set; }
}

