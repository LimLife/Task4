using ItemManagementSystem.Grpc.OrderItemService;
using Microsoft.AspNetCore.Components;
using CrudClient.Model;
using CrudClient.Tools;

namespace CrudClient.Shared.Item
{
    public partial class ItemsTable : ComponentBase
    {
        [EditorRequired][Parameter] public int OrderId { get; set; }
        [CascadingParameter][Inject] public OrderItemService.OrderItemServiceClient OrderItemService { get; set; }

        private OrderItem _item;
        private List<OrderItem> _items;
        private bool _isLoad = false;
        protected override async Task OnInitializedAsync()
        {
            _items = new List<OrderItem?>();
            var replyOrderItem = await OrderItemService.GetListOrderItemsByOrderIdAsync(new GetListOrderItemsByOrderIdRequest() { Id = OrderId });
            if (replyOrderItem is not null)
            {
                _items = replyOrderItem.Order.Select(item => new OrderItem
                {
                    Id = item.Id,
                    Name = item.Name,
                    Quantity = RpcCovert.GetDecimal(item.Quantity),
                    Unit = item.Unit,
                    Order = new Order { Id = OrderId }
                }).ToList();
            }
        }
        protected override void OnParametersSet()
        {
            if (_items is not null)
                _isLoad = true;
        }
        private async Task DeleteOrderItemAsync(int id)
        {
            await OrderItemService.DeleteOrderItemAsync(new DeleteOrderItemReques() { Id = id });
        }
    }
}
