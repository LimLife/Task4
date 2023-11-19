using Microsoft.AspNetCore.Server.Kestrel.Core;
using OrderManagementSystem.Model.Repository;
using OrderManagementSystem.Model.DataBase;
using System.Security.Authentication;
using OrderManagementSystem.Services;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<IRepository, Repository>();
builder.Services.AddDbContext<OrderDB>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("orderConnection")));
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
           .WithExposedHeaders("Grpc-Status", "Grpc-Message", "Grpc-Encoding", "Grpc-Accept-Encoding")
           .WithOrigins("https://localhost:7219", "http://localhost:5133");
}));

var app = builder.Build();

app.UseRouting();
app.UseGrpcWeb(new GrpcWebOptions { DefaultEnabled = true });
app.UseCors();


app.MapGrpcService<OrderApiService>().RequireCors("AllowAll");
app.MapGrpcService<ProviderApiService>().RequireCors("AllowAll");
app.MapGrpcService<FilterApiService>().RequireCors("AllowAll");
app.MapGrpcService<OrderItemApiService>().RequireCors("AllowAll");


app.Run();
