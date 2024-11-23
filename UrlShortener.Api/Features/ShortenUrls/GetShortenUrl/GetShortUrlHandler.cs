namespace UrlShortener.Api.Features.ShortenUrls.GetShortenUrl;

public sealed record GetShortUrlQuery(string Code) : IRequest<GetShortUrlResult>;
public sealed record GetShortUrlResult(string LongUrl);
public sealed class GetShortUrlHandler(
    ApplicationDbContext dbConetxt,
    HybridCache cache) : IRequestHandler<GetShortUrlQuery, GetShortUrlResult>
{
    public async Task<GetShortUrlResult> Handle(
        GetShortUrlQuery query,
        CancellationToken cancellationToken)
    {
        ArgumentException.ThrowIfNullOrEmpty(nameof(query.Code));

        ShortenUrl? shortenUrl = await GetOrCreateAsync(query.Code, cancellationToken);

        if (shortenUrl is null)
        {
            throw new ArgumentException("Not Found!");
        }

        return new GetShortUrlResult(shortenUrl.LongUrl!);
    }

    private async Task<ShortenUrl?> GetOrCreateAsync(
        string uniqueCode,
        CancellationToken cancellationToken) =>
             await cache.GetOrCreateAsync(
             $"code-{uniqueCode}",
            async _ =>
              {
                  return await dbConetxt
                         .ShortenUrls
                         .SingleOrDefaultAsync(d => string.Equals(d.UniqueCode, uniqueCode));
              },
            cancellationToken: cancellationToken);
}
