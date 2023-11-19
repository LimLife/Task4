using ItemManagementSystem.Grpc.FilterService;
using Microsoft.AspNetCore.Components;
using CrudClient.Model;
using CrudClient.Tools;

namespace CrudClient.Shared
{
    public partial class FilterOrder : ComponentBase
    {
        [CascadingParameter] public List<Provider> Providers { get; set; }
        [Inject] public FilterService.FilterServiceClient FilterServiceClient { get; set; }
        [Parameter] public List<Order> Orders { get; set; }
        private FilterModel _filter;
        private bool _isOrders = false;
        protected override void OnInitialized()
        {
            _filter = new FilterModel
            {
                Provider = Providers[0]
            };
        }
        protected override void OnParametersSet()
        {
            if (Orders is not null)
                _isOrders = true;
            _isOrders = false;
        }
        private async Task GetFilterElementsAsync()
        {
            var items = await FilterServiceClient.GetOrderByFilterAsync(new FilterOrderReply
            {
                Number = _filter.Number,
                ProviderId = _filter?.Provider?.Id,
                End = RpcCovert.GetTimestamp(_filter.End),
                Start = RpcCovert.GetTimestamp(_filter.Start)
                //Name
                //Unit
            });
            Orders = items.Orders.Select(item => RpcCovert.GetOrder(item)).ToList();
        }

    }
}
