using CrudClient.Grpc.ProviderService;
using Microsoft.AspNetCore.Components;
using Google.Protobuf.WellKnownTypes;
using CrudClient.Grpc.OrderService;
using CrudClient.Model;
using Grpc.Core;

namespace CrudClient.Pages
{
    public partial class OrderData : ComponentBase
    {
        [Inject] public NavigationManager NavigationManager { get; set; }
        [Inject] public OrderService.OrderServiceClient OrderService { get; set; }
        [Inject] public ProviderService.ProviderServiceClient ProviderService { get; set; }
        [EditorRequired][Parameter] public int OrderId { get; set; }
        private Order _order;
        private List<Provider> _providers;
        private bool _isLoad;

        protected async override Task OnInitializedAsync()
        {
            _providers = new List<Provider>();
            try
            {
                var replyListProvider = await ProviderService.GetListProvidersAsync(new Empty());
                _providers = replyListProvider.Provider.Select(item => new Provider
                {
                    Id = item.Id,
                    Name = item.Name
                }).ToList();

                var replyOrder = await OrderService.GetOrderAsync(new GetOrderRequest() { Id = OrderId });
                _order = new Order
                {
                    Id = replyOrder.Id,
                    Number = replyOrder.Number,
                    Provider = replyOrder.Provider,
                    DateTime = replyOrder.Date.ToDateTime()
                };

            }
            catch (RpcException ex)
            {
                await Console.Out.WriteLineAsync(ex.Status.Detail);
            }

        }
        private async Task HandleValidSubmitAsync()
        {
            try
            {
                await OrderService.UpdateOrderAsync(new UpdateOrderRequest()
                {
                    Id = _order.Id,
                    Number = _order.Number,
                    Date = RpcCovert.GetTimestamp(_order.DateTime),
                    Provider = RpcCovert.GetProviderReply(_order.Provider)
                });
            }
            catch (RpcException ex)
            {
                await Console.Out.WriteLineAsync(ex.Status.Detail);
            }
        }
        protected override void OnParametersSet()
        {
            _isLoad = _order is not null;
        }
        private async Task DeleteOrdersAsync()
        {
            try
            {
                await OrderService.DeleteOrderAsync(new DeleteOrderRequest() { Id = OrderId });
                NavigationManager.NavigateTo("/", true);
            }
            catch (RpcException ex)
            {
                await Console.Out.WriteLineAsync(ex.Status.Detail);
            }
        }
    }
}
