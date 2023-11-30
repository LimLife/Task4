using OrderManagementSystem.Model.Entity;

namespace ItemManagementSystem.Grpc.OrderItemService;
public partial class ListOrderItemReply
{
    public static implicit operator ListOrderItemReply(List<OrderItem> items)
    {
        var orderItemReplyList = new ListOrderItemReply();
        var replyList = items.Select(item => new OrderItemReply
        {
            Id = item.Id,
            Name = item.Name,
            Unit = item.Unit,
            Quantity = item.Quantity,
        })
        .ToList();
        orderItemReplyList.Order.AddRange(replyList);
        return orderItemReplyList;
    }
    public static implicit operator List<OrderItem>(ListOrderItemReply list)
    {
        return list.Order.Select(item => new OrderItem { Id = item.Id, Name = item.Name, Quantity = item.Quantity, Unit = item.Unit }).ToList();
    }
}

