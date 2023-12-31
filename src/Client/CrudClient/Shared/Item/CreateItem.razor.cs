﻿using Microsoft.AspNetCore.Components.Forms;
using CrudClient.Grpc.OrderItemService;
using Microsoft.AspNetCore.Components;
using CrudClient.Grpc.OrderService;
using CrudClient.Model;
using Grpc.Core;

namespace CrudClient.Shared.Item
{
    public partial class CreateItem : ComponentBase
    {
        [EditorRequired][Parameter] public int OrderId { get; set; }
        [Inject] public OrderService.OrderServiceClient OrderService { get; set; }
        [Inject] public OrderItemService.OrderItemServiceClient OrderItemService { get; set; }
        private EditContext _editContext;
        private OrderItem _orderItem { get; set; }
        private bool _isCheckName;

        protected override void OnInitialized()
        {
            _orderItem = new OrderItem();
            _editContext = new(_orderItem);
        }
        private async Task AddItemChangesAsync()
        {
            try
            {
                var isContain = await OrderService.IsConstainNumberOrderAsync(new IsConstainStringOrderRequest { IdOrder = OrderId, Str = _orderItem.Name });
                if (isContain.Value == false)
                {
                    _isCheckName = true;
                    var order = await OrderItemService.CreateOrderItemAsync(new CreateOrderItemReques
                    {
                        Name = _orderItem.Name,
                        Unit = _orderItem.Unit,
                        Quantity = _orderItem.Quantity,
                        Order = OrderId
                    });
                    _orderItem = new OrderItem();
                    StateHasChanged();
                    _isCheckName = true;
                }
                _isCheckName = false;
            }
            catch (RpcException ex)
            {
                await Console.Out.WriteLineAsync(ex.Status.Detail);
            }
        }
    }
}
