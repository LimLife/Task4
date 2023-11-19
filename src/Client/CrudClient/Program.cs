using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Components.Web;
using CrudClient.Grpc.OrderItemService;
using CrudClient.Grpc.ProviderService;
using CrudClient.Grpc.FilterService;
using CrudClient.Grpc.OrderService;
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




