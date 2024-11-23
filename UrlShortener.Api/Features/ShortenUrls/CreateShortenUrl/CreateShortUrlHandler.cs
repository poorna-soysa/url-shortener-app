using UrlShortener.Api.Entities;

namespace UrlShortener.Api.Features.ShortenUrls.CreateShortenUrl;

public sealed record CreateShortUrlCommand(string Url) : IRequest<CreateShortUrlResult>;
public sealed record CreateShortUrlResult(string ShortUrl);
public class CreateShortUrlHandler(
    IShortenUrlService shortenUrlService,
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

        var shortUrl = FormatShortUrl(await shortenUrlService.GenerateUniqueCode());

        ShortenUrl shortenUrl = new()
        {
            Id = Guid.NewGuid(),
            LongUrl = request.Url,
            ShortUrl = shortUrl,
            CreatedOnUtc = DateTime.UtcNow
        };

        return new CreateShortUrlResult(shortUrl);
    }

    private string FormatShortUrl(string uniqueCode)
    {
        return $"{httpContext.Request.Scheme}://{httpContext.Request.Host}/{uniqueCode}";
    }
}
