using OrderManagementSystem.Model.Repository.FilterRepository;
using ItemManagementSystem.Grpc.OrderItemService;
using ItemManagementSystem.Grpc.FilterService;
using OrderManagementSystem.Grpc.OrderService;
using OrderManagementSystem.Model.Repository;
using OrderManagementSystem.Model.EntityDTO;
using OrderManagementSystem.Tools;
using Grpc.Core;

namespace OrderManagementSystem.Services
{
    public class FilterApiService : FilterService.FilterServiceBase
    {
        private readonly IFilterRepository _repository;
        public FilterApiService(IRepository repository) => _repository = repository;

        public override async Task<ListOrderRiply> GetOrderByFilter(FilterOrderReply request, ServerCallContext context)
        {
            var items = await _repository.GetOrdersByFilterAsync(new FilterOrder
            {
                Number = request.Number,
                ProviderID = request.ProviderId,
                End = RpcCovert.GetDateTime(request.End),
                Start = RpcCovert.GetDateTime(request.Start)
            });
            var replyList = new ListOrderRiply();
            var toReply = items.Select(RpcCovert.GetOrderReply).ToList();
            replyList.Orders.AddRange(toReply);
            return replyList;
        }
        public override async Task<ListOrderItemReply> GetOrderItemsByFilter(FilterOrderItemReply request, ServerCallContext context)
        {
            var itmes = await _repository.GetOrderItemsByFilterAsync(new FilterOrderItem
            {
                Name = request.Name,
                Unit = request.Unit,
                Quantity = RpcCovert.GetDecimal(request.Quantity)
            });
            var toReply = itmes.Select(item => new OrderItemReply
            {
                Id = item.Id,
                Name = item.Name,
                Unit = item.Unit,
                Quantity = RpcCovert.GetReplyDecimal(item.Quantity)
            }).
                ToList();
            var replyList = new ListOrderItemReply();

            replyList.Order.AddRange(toReply);
            return replyList;
        }
    }
}
