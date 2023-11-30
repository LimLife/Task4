using OrderManagementSystem.Model.Repository.FilterRepository;
using ItemManagementSystem.Grpc.FilterService;
using OrderManagementSystem.Grpc.OrderService;
using OrderManagementSystem.Model.Repository;
using OrderManagementSystem.Model.EntityDTO;
using Grpc.Core;

namespace OrderManagementSystem.Services
{
    public class FilterApiService : FilterService.FilterServiceBase
    {
        private readonly IFilterRepository _repository;
        public FilterApiService(IRepository repository) => _repository = repository;

        public override async Task<ListOrderRiply> GetOrderByFilter(FilterReply request, ServerCallContext context)
        {
            try
            {
                if (request is null) return new ListOrderRiply();
                return await _repository.GetOrdersByFilterAsync(new Filter
                {
                    Number = request.Number,
                    ProviderID = request.ProviderId,
                    End = request.End.ToDateTime(),
                    Start = request.Start.ToDateTime(),
                    Name = request.Name,
                    Unit = request.Unit
                }) ?? throw new RpcException(new Status(StatusCode.NotFound, "Nothing found"));
            }
            catch (Exception)
            {
                return new ListOrderRiply();
            }
        }
    }
}
