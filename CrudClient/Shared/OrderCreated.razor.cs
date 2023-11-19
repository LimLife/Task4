using OrderManagementSystem.Grpc.OrderService;
using Microsoft.AspNetCore.Components;
using CrudClient.Model;
using CrudClient.Tools;

namespace CrudClient.Shared
{
    public partial class OrderCreated : ComponentBase
    {
        [CascadingParameter] public List<Provider> Providers { get; set; }
        [Inject] public OrderService.OrderServiceClient OrderService { get; set; }
        private Order _order;

        protected override void OnInitialized()
        {
            _order = new Order
            {
                Provider = Providers[0]
            };
        }

        private async Task CreatedOrderHandlerAsync()
        {
            await OrderService.CreateOrderAsync(new CreateOrderRequest
            {
                Number = _order.Number,
                Date = RpcCovert.GetTimestamp(_order.DateTime),
                Provider = RpcCovert.GetProviderReply(_order.Provider)
            });
            _order = new Order { Provider = Providers.First() };
        }
    }
}
