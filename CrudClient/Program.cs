using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ItemManagementSystem.Grpc.OrderItemService;
using OrderManagementSystem.Grpc.ProviderService;
using ItemManagementSystem.Grpc.FilterService;
using OrderManagementSystem.Grpc.OrderService;
using Microsoft.AspNetCore.Components.Web;
using Grpc.Net.Client.Web;
using CrudClient;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");



var url = builder.Configuration.GetConnectionString("Order");
var httpHandler = new GrpcWebHandler(GrpcWebMode.GrpcWeb, new HttpClientHandler());
builder.Services.AddGrpcClient<FilterService.FilterServiceClient>(options =>
{
    options.Address = new Uri(url);
}).ConfigurePrimaryHttpMessageHandler(() => httpHandler);
builder.Services.AddGrpcClient<ProviderService.ProviderServiceClient>(options =>
{
    options.Address = new Uri(url);
}).ConfigurePrimaryHttpMessageHandler(() => httpHandler);
builder.Services.AddGrpcClient<OrderService.OrderServiceClient>(options =>
{
    options.Address = new Uri(url);
}).ConfigurePrimaryHttpMessageHandler(() => httpHandler);
builder.Services.AddGrpcClient<OrderItemService.OrderItemServiceClient>(options =>
{
    options.Address = new Uri(url);
}).ConfigurePrimaryHttpMessageHandler(() => httpHandler);

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();




