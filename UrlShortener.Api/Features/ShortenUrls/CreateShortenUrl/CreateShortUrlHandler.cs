using UrlShortener.Api.Abstractions;

namespace UrlShortener.Api.Features.ShortenUrls.CreateShortenUrl;

public interface ICreateShortUrlHandler: IHandler
{
    Task<CreateShortUrlResult> HandleAsync(CreateShortUrlCommand command, CancellationToken cancellationToken);
}
public sealed record CreateShortUrlCommand(string Url);
public sealed record CreateShortUrlResult(string ShortUrl);
public sealed class CreateShortUrlHandler(
    IShortenUrlService shortenUrlService,
    ApplicationDbContext dbConetxt,
    IHttpContextAccessor httpContextAccessor)
    : ICreateShortUrlHandler
{
    public async Task<CreateShortUrlResult> HandleAsync(
        CreateShortUrlCommand command,
        CancellationToken cancellationToken)
    {
        var uniqueCode = await shortenUrlService.GenerateUniqueCode();
        var shortUrl = FormatShortUrl(uniqueCode);

        ShortenUrl shortenUrl = new()
        {
            Id = Guid.NewGuid(),
            LongUrl = command.Url,
            ShortUrl = shortUrl,
            UniqueCode = uniqueCode,
            CreatedOnUtc = DateTime.UtcNow
        };

        dbConetxt.ShortenUrls.Add(shortenUrl);
        await dbConetxt.SaveChangesAsync();

        return new CreateShortUrlResult(shortUrl);
    }

    private string FormatShortUrl(string uniqueCode) =>
        $"{httpContextAccessor?.HttpContext?.Request.Scheme}://{httpContextAccessor?.HttpContext?.Request.Host}/{uniqueCode}";
}
