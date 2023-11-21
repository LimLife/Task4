using CrudClient.Model;

namespace CrudClient.Grpc.OrderItemService;

public partial class OrderItemReply
{
    public OrderItemReply(int id, string name, decimal quantity, string unit)
    {
        Id = id;
        Name = name;
        Unit = unit;
        Quantity = quantity;
    }

    public static implicit operator OrderItem(OrderItemReply orderItemReply)
    {
        return new OrderItem
        {
            Id = orderItemReply.Id,
            Name = orderItemReply.Name,
            Quantity = orderItemReply.Quantity,
            Unit = orderItemReply.Unit
        };
    }
    public static implicit operator OrderItemReply(OrderItem reply)
    {
        return new OrderItemReply
        {
            Id = reply.Id,
            Name = reply.Name,
            Unit = reply.Unit,
            Quantity = reply.Quantity
        };
    }
}
