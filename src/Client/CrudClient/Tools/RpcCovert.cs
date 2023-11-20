using CrudClient.Grpc.OrderItemService;
using CrudClient.Grpc.ProviderService;
using Google.Protobuf.WellKnownTypes;
using CrudClient.Grpc.OrderService;
using CrudClient.Model;
using System;

namespace CrudClient.Tools
{
    public static class RpcCovert
    {
        public static Order GetOrder(OrderReply reply)
        {
            return new Order
            {
                Id = reply.Id,
                Number = reply.Number,
                DateTime = reply.Date.ToDateTime(),
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
                Date = Timestamp.FromDateTime(order.DateTime.ToUniversalTime()),
                Provider = GetProviderReply(order.Provider)
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
       
    } 
}
