using UrlShortener.Api.Features.ShortenUrls.CreateShortenUrl;
using UrlShortener.Api.Features.ShortenUrls.GetShortenUrl;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddOpenApi();

builder.Services.AddScoped<IShortenUrlService, ShortenUrlService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddCarter();

builder.Services.AddPostgreSqlConfig(builder.Configuration);
builder.Services.AddRedisConfig(builder.Configuration);
builder.Services.AddHybridCacheConfig();
//builder.Services.AddMediatRConfig();

builder.Services.AddScoped<IGetShortUrlHandler, GetShortUrlHandler>();
builder.Services.AddScoped<ICreatetShortUrlHandler, CreateShortUrlHandler>();

var app = builder.Build();

app.MapDefaultEndpoints();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.ApplyMigrations();
}

app.MapCarter();
app.UseHttpsRedirection();

app.Run();
