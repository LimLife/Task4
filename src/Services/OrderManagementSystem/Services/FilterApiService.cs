using OrderManagementSystem.Model.Repository.FilterRepository;
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

        public override async Task<ListOrderRiply> GetOrderByFilter(FilterReply request, ServerCallContext context)
        {
            if (request is null) return new ListOrderRiply();
            var items = await _repository.GetOrdersByFilterAsync(new Filter
            {
                Number = request.Number,
                ProviderID = request.ProviderId,
                End = RpcCovert.GetDateTime(request.End),
                Start = RpcCovert.GetDateTime(request.Start),
                Name = request.Name,
                Unit = request.Unit
            });
            var replyList = new ListOrderRiply();
            var toReply = items.Select(RpcCovert.GetOrderReply).ToList();
            replyList.Orders.AddRange(toReply);
            return replyList;
        }
    }
}
