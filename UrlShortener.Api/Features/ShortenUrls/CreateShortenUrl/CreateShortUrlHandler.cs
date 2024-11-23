namespace UrlShortener.Api.Features.ShortenUrls.CreateShortenUrl;

public sealed record CreateShortUrlCommand(string Url) : IRequest<CreateShortUrlResult>;
public sealed record CreateShortUrlResult(string ShortUrl);
public sealed class CreateShortUrlHandler(
    IShortenUrlService shortenUrlService,
    ApplicationDbContext dbConetxt,
    HttpContext httpContext) 
    : IRequestHandler<CreateShortUrlCommand, CreateShortUrlResult>
{
    public async Task<CreateShortUrlResult> Handle(
        CreateShortUrlCommand request,
        CancellationToken cancellationToken)
    {
        if (!Uri.TryCreate(request.Url, UriKind.Absolute, out _))
        {
            throw new ArgumentException("");
        }

        var uniqueCode = await shortenUrlService.GenerateUniqueCode();
        var shortUrl = FormatShortUrl(uniqueCode);

        ShortenUrl shortenUrl = new()
        {
            Id = Guid.NewGuid(),
            LongUrl = request.Url,
            ShortUrl = shortUrl,
            UniqueCode = uniqueCode,
            CreatedOnUtc = DateTime.UtcNow
        };

        dbConetxt.ShortenUrls.Add(shortenUrl);
        await dbConetxt.SaveChangesAsync();

        return new CreateShortUrlResult(shortUrl);
    }

    private string FormatShortUrl(string uniqueCode)
    {
        return $"{httpContext.Request.Scheme}://{httpContext.Request.Host}/{uniqueCode}";
    }
}
