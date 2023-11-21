using Microsoft.AspNetCore.Components;
using CrudClient.Grpc.OrderService;
using CrudClient.Model;
using Grpc.Core;
using Google.Protobuf.WellKnownTypes;

namespace CrudClient.Shared
{
    public partial class OrderCreated : ComponentBase
    {
        [CascadingParameter] public List<Provider> Providers { get; set; }
        [Inject] public OrderService.OrderServiceClient OrderService { get; set; }
        private List<Order> _orders;
        private Order _order;
        private bool _isCheckName;
        protected override void OnInitialized()
        {
            _orders = new List<Order>();
            _order = new Order()
            {
                Provider = Providers[0] ?? new Provider() { Id = 0, Name = "" }
            };
        }

        private async Task CreatedOrderHandlerAsync()
        {
            try
            {
                var isConstain = await OrderService.IsConstainProviderOrderAsync(new IsConstainProviderInOrderRequest
                {
                    Number = _order.Number,
                    ProviderId = _order.Provider.Id
                });
                if (isConstain.Value == false)
                {
                    var order = await OrderService.CreateOrderAsync(new CreateOrderRequest
                    {
                        Number = _order.Number,
                        Date = Timestamp.FromDateTime(_order.DateTime),
                        Provider = _order.Provider
                    });
                    _order = new Order { Provider = Providers.First() };
                    _orders.Add(new Order
                    {
                        Id = order.Id,
                        Number = order.Number,
                        Provider = order.Provider,
                        DateTime = order.Date.ToDateTime()
                    });
                    StateHasChanged();
                    _isCheckName = true;
                }
                else
                    _isCheckName = false;
            }
            catch (RpcException ex)
            {
                await Console.Out.WriteLineAsync(ex.Status.Detail);
            }
        }
    }
}

