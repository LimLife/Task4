using CrudClient.Grpc.OrderItemService;
using Microsoft.AspNetCore.Components;
using CrudClient.Grpc.OrderService;
using CrudClient.Model;
using CrudClient.Tools;
using Grpc.Core;

namespace CrudClient.Shared.Item
{
    public partial class ItemEdit : ComponentBase
    {
        [EditorRequired][Parameter] public OrderItem OrderItem { get; set; }
        [CascadingParameter][Inject] public OrderItemService.OrderItemServiceClient OrderItemService { get; set; }
        [Inject] public OrderService.OrderServiceClient OrderService { get; set; }
        private bool _isCheckName = false;
        private async Task ApplayChangesAsync()
        {
            try
            {
                var isContain = await OrderService.IsStringParameterNumberAsync(new IsStringParameterRequest { IdOrder = OrderItem.Order.Id, Str = OrderItem.Name });
                if (isContain.Value == false)
                {
                    _isCheckName = true;
                    await OrderItemService.UpdateOrderItemAsync(new UpdateOrderItemReques
                    {
                        Id = OrderItem.Id,
                        Name = OrderItem.Name,
                        Unit = OrderItem.Unit,
                        Quantity = RpcCovert.GetReplyDecimal(OrderItem.Quantity)
                    });
                }
                else
                    _isCheckName = true;

            }
            catch (RpcException ex)
            {
                await Console.Out.WriteLineAsync(ex.Status.Detail);
            }
        }

    }
}
