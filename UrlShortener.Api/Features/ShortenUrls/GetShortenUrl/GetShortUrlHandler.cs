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
        var shortenUrl = await cache.GetOrCreateAsync(
            $"code-{query.Code}",
            async _ =>
            {
                return await dbConetxt
                       .ShortenUrls
                       .SingleOrDefaultAsync(d => string.Equals(d.UniqueCode, query.Code));
            },
            cancellationToken: cancellationToken);

        if (shortenUrl is null)
        {
            throw new ArgumentException("Not Found!");
        }

        return new GetShortUrlResult(shortenUrl.LongUrl!);
    }
}
