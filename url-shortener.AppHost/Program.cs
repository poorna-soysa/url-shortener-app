var builder = DistributedApplication.CreateBuilder(args);

var shortenUrlDb = builder.AddPostgres("shortenUrl-db")
    .WithDataVolume()
    .WithPgAdmin();

var shortenUrlCache = builder.AddRedis("shortenUrl-cache")
    .WithDataVolume()
    .WithRedisCommander();

builder.AddProject<Projects.UrlShortener_Api>("urlshortener-api")
    .WithReference(shortenUrlDb)
    .WithReference(shortenUrlCache);

builder.Build().Run();
