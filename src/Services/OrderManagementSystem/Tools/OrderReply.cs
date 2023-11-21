using Google.Protobuf.WellKnownTypes;
using OrderManagementSystem.Model.Entity;

namespace OrderManagementSystem.Grpc.OrderService;
public partial class OrderReply
{
    public OrderReply(int id, string number, Provider provider, Timestamp timestamp)
    {
        Id = id;
        Number = number;
        Provider = provider;
        Date = timestamp;
    }

    public static implicit operator Order(OrderReply orderReply)
    {
        return new Order
        {
            Id = orderReply.Id,
            Number = orderReply.Number,
            Provider = orderReply.Provider,
            Date = orderReply.Date.ToDateTime(),
        };
    }
    public static implicit operator OrderReply(Order reply)
    {
        return new Order
        {
            Id = reply.Id,
            Number = reply.Number,
            Provider = reply.Provider,
            Date = reply.Date
        };
    }
}

