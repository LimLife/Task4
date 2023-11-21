using CrudClient.Grpc.ProviderService;
using Microsoft.AspNetCore.Components;
using Google.Protobuf.WellKnownTypes;
using CrudClient.Grpc.OrderService;
using CrudClient.Model;
using Grpc.Core;

namespace CrudClient.Pages
{
    public partial class Index : ComponentBase
    {
        [Inject] public ProviderService.ProviderServiceClient ProviderService { get; set; }
        [Inject] public OrderService.OrderServiceClient OrderService { get; set; }
        [CascadingParameter] public List<Provider> Providers { get; set; }

        private List<Order> _orders;

        private bool _isLoad;
        protected override async Task OnInitializedAsync()
        {
            Providers = new List<Provider>();
            _orders = new List<Order>();
            try
            {
                var replyListProvider = await ProviderService.GetListProvidersAsync(new Empty());
                Providers = replyListProvider.Provider.Select(item => new Provider
                {
                    Id = item.Id,
                    Name = item.Name
                }).ToList();

                var replyListOrder = await OrderService.GetListOrdersAsync(new Empty());
                _orders = replyListOrder.Orders.Select(item => new Order
                {
                    Id = item.Id,
                    Number = item.Number,
                    Provider = item.Provider,
                    DateTime = item.Date.ToDateTime()
                }).ToList();
            }
            catch (RpcException ex)
            {
                await Console.Out.WriteLineAsync(ex.Status.Detail);
            }
        }
        protected override void OnParametersSet()
        {
            _isLoad = Providers is not null;
        }
    }
}
