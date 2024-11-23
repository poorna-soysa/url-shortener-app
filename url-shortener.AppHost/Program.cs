var builder = DistributedApplication.CreateBuilder(args);

var shortenUrlDb = builder.AddPostgres("shortenUrl-db")
    .WithDataVolume()
    .WithPgAdmin();

builder.AddProject<Projects.UrlShortener_Api>("urlshortener-api")
    .WithReference(shortenUrlDb);

builder.Build().Run();
