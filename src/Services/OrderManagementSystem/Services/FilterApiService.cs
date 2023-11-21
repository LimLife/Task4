using OrderManagementSystem.Model.Repository.FilterRepository;
using ItemManagementSystem.Grpc.FilterService;
using OrderManagementSystem.Grpc.OrderService;
using OrderManagementSystem.Model.Repository;
using OrderManagementSystem.Model.EntityDTO;
using Google.Protobuf.WellKnownTypes;
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
                End = request.End.ToDateTime(),
                Start = request.Start.ToDateTime(),
                Name = request.Name,
                Unit = request.Unit
            });
            if (items is null) return new ListOrderRiply();

            var replyList = new ListOrderRiply();
            var toReply = items.Select(item => new OrderReply
            {
                Id = item.Id,
                Number = item.Number,
                Date = Timestamp.FromDateTime(item.Date)
            });
            replyList.Orders.AddRange(toReply);
            return replyList;
        }
    }
}
