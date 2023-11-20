using CrudClient.Grpc.OrderItemService;
using Microsoft.AspNetCore.Components;
using CrudClient.Model;
using Grpc.Core;

namespace CrudClient.Shared.Item
{
    public partial class ItemsTable : ComponentBase
    {
        [EditorRequired][Parameter] public int OrderId { get; set; }
        [CascadingParameter][Inject] public OrderItemService.OrderItemServiceClient OrderItemService { get; set; }
        private List<OrderItem> _itemsOrder { get; set; }

        private OrderItem _item;
        protected override async Task OnInitializedAsync()
        {
            _itemsOrder = new List<OrderItem>();
            try
            {
                var replyOrderItem = await OrderItemService.GetListOrderItemsByOrderIdAsync(new GetListOrderItemsByOrderIdRequest() { Id = OrderId });
                if (replyOrderItem is not null)
                {
                    _itemsOrder = replyOrderItem.Order.Select(item => new OrderItem
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Unit = item.Unit,
                        Order = new Order { Id = OrderId }
                    }).ToList();
                };
                await Console.Out.WriteLineAsync($"{replyOrderItem.Order.Count}");
            }
            catch (RpcException ex)
            {
                await Console.Out.WriteLineAsync(ex.Status.Detail);
            }
        }
        private async Task DeleteOrderItemAsync(int id)
        {
            try
            {
                await OrderItemService.DeleteOrderItemAsync(new DeleteOrderItemReques() { Id = id });
            }
            catch (RpcException ex)
            {
                await Console.Out.WriteLineAsync(ex.Status.Detail);

            }
        }
    }
}

