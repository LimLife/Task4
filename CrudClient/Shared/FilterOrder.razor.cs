    using Microsoft.AspNetCore.Components;
    using CrudClient.Grpc.FilterService;
    using CrudClient.Model;
    using CrudClient.Tools;
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
                        End = RpcCovert.GetTimestamp(_filter.End),
                        Start = RpcCovert.GetTimestamp(_filter.Start),
                        Name = _filter.Name,
                        Unit = _filter.Unit
                    });
                    Orders = items.Orders.Select(item => RpcCovert.GetOrder(item)).ToList();
                    StateHasChanged();
                }
                catch (RpcException ex)
                {
                    await Console.Out.WriteLineAsync(ex.Status.Detail);
                }
            }

        }
    }
