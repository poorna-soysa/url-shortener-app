namespace UrlShortener.Api.Features.ShortenUrls.CreateShortenUrl;

public sealed record CreateShortUrlRequest(string Url);
public sealed class CreateShortUrlEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/short-url", async (
            CreateShortUrlRequest request,
            ICreatetShortUrlHandler handler,
            CancellationToken ct) =>
        {
            if (!Uri.TryCreate(request.Url, UriKind.Absolute, out _))
            {
               return Results.BadRequest("Inavlid URl");
            }

            var command = new CreateShortUrlCommand(request.Url);

            var result = await handler.HandleAsync(command, ct);

            return Results.Ok(result.ShortUrl);
        })
       .WithName("CreateShortUrl")
       .Produces(StatusCodes.Status200OK)
       .ProducesProblem(StatusCodes.Status400BadRequest)
       .WithSummary("Create Short Url")
       .WithDescription("Create Short Url");
    }
}
