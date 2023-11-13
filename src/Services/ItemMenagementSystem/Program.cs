using Microsoft.AspNetCore.Server.Kestrel.Core;
using ItemManagementSystem.Model.DataBase;
using ItemManagementSystem.Middleware;
using System.Security.Authentication;
using ItemManagementSystem.Services;
using Microsoft.EntityFrameworkCore;
using ItemManagementSystem.Model.Repository;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<IRepository, Repository>();
builder.Services.AddDbContext<OrderItemDB>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("orderItemDB")));
builder.Services.AddGrpc();

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
var app = builder.Build();
app.UseMiddleware<CheckDBConnect>();
app.MapGrpcService<OrderItemApiService>();
app.Run();
