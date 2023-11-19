using OrderManagementSystem.Grpc.OrderService;
using Microsoft.AspNetCore.Components;
using CrudClient.Model;
using CrudClient.Tools;
using OrderManagementSystem.Grpc.ProviderService;
using Google.Protobuf.WellKnownTypes;

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
        private bool _isLoad = false;

        protected async override Task OnInitializedAsync()
        {
            _providers = new List<Provider>();
            var replyListProvider = await ProviderService.GetListProvidersAsync(new Empty());
            _providers = replyListProvider.Provider.Select(item => new Provider { Id = item.Id, Name = item.Name }).ToList();

            var replyOrder = await OrderService.GetOrderAsync(new GetOrderRequest() { Id = OrderId });
            _order = RpcCovert.GetOrder(replyOrder);
        }

        private async Task HandleValidSubmit()
        {
            await OrderService.UpdateOrderAsync(new UpdateOrderRequest()
            {
                Id = _order.Id,
                Number = _order.Number,
                Date = RpcCovert.GetTimestamp(_order.DateTime)
            });
        }
        protected override void OnParametersSet()
        {
            if (_order is not null)
                _isLoad = true;
        }
        private async Task DeleteOrdersAsync()
        {
            await OrderService.DeleteOrderAsync(new DeleteOrderRequest() { Id = OrderId });
            NavigationManager.NavigateTo("/", true);
        }
    }
}
