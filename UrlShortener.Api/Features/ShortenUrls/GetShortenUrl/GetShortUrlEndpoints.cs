namespace UrlShortener.Api.Features.ShortenUrls.GetShortenUrl;

public sealed class GetShortUrlEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/{code}", async (string code, ISender sender) =>
        {
            try
            {
                var query = new GetShortUrlQuery(code);

                var result = await sender.Send(query);

                return Results.Redirect(result.LongUrl);
            }
            catch (Exception)
            {
                return Results.NotFound();
            }
        })
       .WithName("GetShortUrl")
       .Produces(StatusCodes.Status308PermanentRedirect)
       .ProducesProblem(StatusCodes.Status404NotFound)
       .WithSummary("Get Short Url")
       .WithDescription("Get Short Url");
    }
}
