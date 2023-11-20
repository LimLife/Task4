﻿using Microsoft.AspNetCore.Components;
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
        private List<Order> _orders;
        private Order _order;
        private bool _isLoad;
        protected override void OnInitialized()
        {
            _order = new Order()
            {
                Provider = Providers[0] ?? new Provider() { Id = 0, Name = "" }
            };
        }

        private async Task CreatedOrderHandlerAsync()
        {
            var isConstain = await OrderService.IsConstainProviderOrderAsync(new IsConstainProviderInOrderRequest { Number = _order.Number, ProviderId = _order.Provider.Id });
            if (isConstain.Value == false)
            {
                try
                {
                    var order = await OrderService.CreateOrderAsync(new CreateOrderRequest
                    {
                        Number = _order.Number,
                        Date = RpcCovert.GetTimestamp(_order.DateTime),
                        Provider = RpcCovert.GetProviderReply(_order.Provider)
                    });
                    _order = new Order { Provider = Providers.First() };
                    _orders.Add(RpcCovert.GetOrder(order));
                    StateHasChanged();
                }
                catch (RpcException ex)
                {
                    await Console.Out.WriteLineAsync(ex.Status.Detail);
                }
            }
        }
        protected override void OnParametersSet()
        {
            _isLoad = _orders is not null;
        }
    }
}
