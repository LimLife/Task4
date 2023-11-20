using OrderManagementSystem.Grpc.ProviderService;
using OrderManagementSystem.Grpc.OrderService;
using OrderManagementSystem.Model.Entity;
using Google.Protobuf.WellKnownTypes;
using Google.Protobuf;

namespace OrderManagementSystem.Tools
{
    public static class RpcCovert
    {
        public static Order GetOrder(OrderReply reply)
        {
            return new Order
            {
                Id = reply.Id,
                Number = reply.Number,
                Date = reply.Date.ToDateTime(),
                Provider = GetProvider(reply.Provider),
            };
        }
        public static DateTime GetDateTime(Timestamp timestamp)
        {
            return timestamp.ToDateTime();
        }
        public static Timestamp GetTimestamp(DateTime dateTime)
        {
            return Timestamp.FromDateTime(dateTime.ToUniversalTime());
        }
        public static OrderReply GetOrderReply(Order order)
        {
            return new OrderReply
            {
                Id = order.Id,
                Number = order.Number,
                Date = Timestamp.FromDateTime(order.Date.ToUniversalTime()),
                Provider  = GetProviderReply(order.Provider)
            };
        }
        public static ListOrderRiply GetOrderReply(List<Order> order)
        {
            var reply = new ListOrderRiply();
            var items = order.Select(x => GetOrderReply(x)).ToList();
            reply.Orders.AddRange(items);
            return reply;
        }

        public static Provider GetProvider(ProviderReply reply)
        {
            return new Provider
            {
                Id = reply.Id,
                Name = reply.Name,
            };
        }
        public static ProviderReply GetProviderReply(Provider provider)
        {
            return new ProviderReply
            {
                Id = provider.Id,
                Name = provider.Name,
            };
        }


        public static Order ConvertRequestToOrder<T>(T request) where T : IMessage
        {
            if (request is GetOrderRequest get)
            {
                return new Order()
                {
                    Id = get.Id,
                };
            }
            else if (request is CreateOrderRequest create)
            {
                return new Order
                {
                    Number = create.Number,
                    Date = create.Date.ToDateTime(),
                    Provider = GetProvider(create.Provider),
                };
            }
            else if (request is UpdateOrderRequest update)
            {
                return new Order
                {
                    Id = update.Id,
                    Number = update.Number,
                    Date = update.Date.ToDateTime(),
                    Provider = GetProvider(update.Provider)
                };
            }
            else if (request is DeleteOrderRequest delete)
            {
                return new Order { Id = delete.Id, };
            }
            return null;
        }     
    }
}
