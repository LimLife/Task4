using Microsoft.AspNetCore.Server.Kestrel.Core;
using OrderManagementSystem.Model.DataBase;
using OrderManagementSystem.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<OrderDB>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("orderDB")));
builder.Services.AddGrpc();

builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ConfigureEndpointDefaults(listenOptions =>
    {
        listenOptions.Protocols = HttpProtocols.Http2;
    });
});

var app = builder.Build();

app.MapGrpcService<OrderApiService>();

app.Run();
