using Google.Protobuf.WellKnownTypes;
using OrderManagementSystem.Model.Entity;

namespace OrderManagementSystem.Grpc.OrderService;

public partial class ListOrderRiply
{
    public static implicit operator ListOrderRiply(List<Order> orders)
    {
        var orderReplyList = new ListOrderRiply();
        var listReply = orders.Select(item => new OrderReply
        {
            Id = item.Id,
            Date = item.Date.ToUniversalTime().ToTimestamp(),
            Number = item.Number,
            Provider = item.Provider
        }).ToList();
        orderReplyList.Orders.AddRange(listReply);
        return orderReplyList;
    }
    public static implicit operator List<Order>(ListOrderRiply orders)
    {
        return orders.Orders.Select(item => new Order
        {
            Id = item.Id,
            Number = item.Number,
            Date = item.Date.ToDateTime(),
            Provider = item.Provider
        }).ToList();
    }
}

