using CrudClient.Model;
using CrudClient.Tools;
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Components;
using OrderManagementSystem.Grpc.OrderService;
using OrderManagementSystem.Grpc.ProviderService;

namespace CrudClient.Pages
{
    public partial class Index : ComponentBase
    {
        [Inject] public ProviderService.ProviderServiceClient ProviderService { get; set; }
        [Inject] public OrderService.OrderServiceClient OrderService { get; set; }
        [CascadingParameter] public List<Provider> Providers { get; set; }
        private List<Order> _orders;
        protected override async Task OnInitializedAsync()
        {
            Providers = new List<Provider>();
            var replyListProvider = await ProviderService.GetListProvidersAsync(new Empty());
            Providers = replyListProvider.Provider.Select(item => new Provider { Id = item.Id, Name = item.Name }).ToList();

            var replyOrder = await OrderService.GetListOrdersAsync(new Empty());
            _orders = replyOrder.Orders.Select(item => RpcCovert.GetOrder(item)).ToList();
        }
    }
}
