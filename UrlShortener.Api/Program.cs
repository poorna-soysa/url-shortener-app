var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddOpenApi();

builder.Services.AddScoped<IShortenUrlService, ShortenUrlService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddCarter();

builder.Services.AddPostgreSqlConfig(builder.Configuration);
builder.Services.AddRedisConfig(builder.Configuration);
builder.Services.AddHybridCacheConfig();
builder.Services.AddHandlersFromAssembly(typeof(Program).Assembly);

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
