namespace UrlShortener.Api.Features.ShortenUrls;

public interface IShortenUrlService
{
    Task<string> GenerateUniqueCode();
}

