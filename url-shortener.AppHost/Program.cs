var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.UrlShortener_Api>("urlshortener-api");

builder.Build().Run();
