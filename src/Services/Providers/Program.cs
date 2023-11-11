using Microsoft.EntityFrameworkCore;
using Providers.Model.DataBase;
using Providers.Model.Repository;
using Providers.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<IRepository, Repository>();
builder.Services.AddDbContext<ProviderDB>(op => op.UseSqlite(builder.Configuration.GetConnectionString("providerConnection")));

builder.Services.AddGrpc();

var app = builder.Build();

app.MapGrpcService<ProviderApiService>();
app.Run();
