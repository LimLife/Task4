using ItemManagementSystem.Model.DataBase;
using ItemManagementSystem.Services;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<OrderItemDB>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("orderDB")));
builder.Services.AddGrpc();

builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ConfigureEndpointDefaults(listenOptions =>
    {
        listenOptions.Protocols = HttpProtocols.Http2;
    });
});


var app = builder.Build();

app.MapGrpcService<OrderItemApiService>();
app.Run();
