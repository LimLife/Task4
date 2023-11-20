using Microsoft.AspNetCore.Components;
using CrudClient.Grpc.OrderService;
using CrudClient.Model;
using CrudClient.Tools;
using Grpc.Core;

namespace CrudClient.Shared
{
    public partial class OrderCreated : ComponentBase
    {
        [Parameter] public RenderFragment ChildContent { get; set; }
        [CascadingParameter] public List<Provider> Providers { get; set; }
        [Inject] public OrderService.OrderServiceClient OrderService { get; set; }
        private Order _order;

        protected override void OnInitialized()
        {
            _order = new Order()
            {
                Provider = Providers[0] ?? new Provider() { Id = 0, Name = "" }
            };
        }

        private async Task CreatedOrderHandlerAsync()
        {
            //Проверить при создании что Что если у Number есть и у него одинаковый профайдер то гг
            if (_order.Provider.Id != 0)
                try
                {
                    await OrderService.CreateOrderAsync(new CreateOrderRequest
                    {
                        Number = _order.Number,
                        Date = RpcCovert.GetTimestamp(_order.DateTime),
                        Provider = RpcCovert.GetProviderReply(_order.Provider)
                    });
                    StateHasChanged();
                    _order = new Order { Provider = Providers.First() };
                }
                catch (RpcException ex)
                {
                    await Console.Out.WriteLineAsync(ex.Status.Detail);
                }
        }
    }
}
