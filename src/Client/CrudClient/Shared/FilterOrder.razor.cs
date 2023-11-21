using Microsoft.AspNetCore.Components;
using Google.Protobuf.WellKnownTypes;
using CrudClient.Grpc.FilterService;
using CrudClient.Model;
using Grpc.Core;

namespace CrudClient.Shared
{
    public partial class FilterOrder : ComponentBase
    {
        [CascadingParameter] public List<Provider> Providers { get; set; }
        [Inject] public FilterService.FilterServiceClient FilterServiceClient { get; set; }
        [Parameter] public List<Order> Orders { get; set; }
        private FilterModel _filter;
        private bool _isOrders;
        protected override void OnInitialized()
        {
            Orders = new List<Order>();
            _filter = new FilterModel
            {
                Provider = Providers[0]
            };
        }
        protected override void OnParametersSet()
        {
            _isOrders = Orders is not null;
        }
        private async Task GetFilterElementsAsync()
        {
            try
            {
                var items = await FilterServiceClient.GetOrderByFilterAsync(new FilterReply
                {
                    Number = _filter.Number,
                    ProviderId = _filter?.Provider?.Id,
                    End = Timestamp.FromDateTime(_filter.End),
                    Start = Timestamp.FromDateTime(_filter.Start),
                    Name = _filter.Name,
                    Unit = _filter.Unit
                });
                Orders = items.Orders.Select(item => new Order
                {
                    Id = item.Id,
                    Provider = item.Provider,
                    Number = item.Number,
                    DateTime = item.Date.ToDateTime()
                }).ToList();
                StateHasChanged();
            }
            catch (RpcException ex)
            {
                await Console.Out.WriteLineAsync(ex.Status.Detail);
            }
        }

    }
}
