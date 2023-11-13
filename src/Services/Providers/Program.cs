using Microsoft.AspNetCore.Server.Kestrel.Core;
using System.Security.Authentication;
using Microsoft.EntityFrameworkCore;
using Providers.Model.Repository;
using Providers.Model.DataBase;
using Providers.Middleware;
using Providers.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<IRepository, Repository>();
builder.Services.AddDbContext<ProviderDB>(op => op.UseSqlite(builder.Configuration.GetConnectionString("providerConnection")));

builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ConfigureEndpointDefaults(listenOptions =>
    {
        listenOptions.Protocols = HttpProtocols.Http2;
    });
    serverOptions.ConfigureHttpsDefaults(op =>
    {
        op.SslProtocols = SslProtocols.Tls12 | SslProtocols.Tls13;
    });
});
builder.Services.AddCors(polici => polici.AddPolicy("AllowAll", options =>
{
    options.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader()
           .WithExposedHeaders("Grpc-Status", "Grpc-Message", "Grpc-Encoding", "Grpc-Accept-Encoding");
}));
builder.Services.AddGrpc();

var app = builder.Build();
app.UseMiddleware<CheckDBConnect>();
app.UseCors();
app.MapGrpcService<ProviderApiService>();
app.Run();
