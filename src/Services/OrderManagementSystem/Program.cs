using Microsoft.AspNetCore.Server.Kestrel.Core;
using OrderManagementSystem.Model.Repository;
using OrderManagementSystem.Model.DataBase;
using OrderManagementSystem.Middleware;
using System.Security.Authentication;
using OrderManagementSystem.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<IRepository, Repository>();
builder.Services.AddDbContext<OrderDB>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("orderConnection")));
builder.Services.AddGrpc(options => options.Interceptors.Add<CheckDBConnect>());


builder.Services.AddSingleton<CheckDBConnect>();
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
app.UseCors();
app.MapGrpcService<OrderApiService>();
app.MapGrpcService<ProviderApiService>();

app.Run();
