using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using ItemManagementSystem.Grpc.OrderItemService;
using OrderManagementSystem.Model.Repository;
using OrderManagementSystem.Model.Repository.OrderItemRepository;

namespace OrderManagementSystem.Services
{
    public class OrderItemApiService : OrderItemService.OrderItemServiceBase
    {
        private readonly IOrderItemRepository _repositorty;
        public OrderItemApiService(IRepository repository) => _repositorty = repository;

        public override Task<ListOrderItemReply> GetListOrderItemsByOrderId(GetListOrderItemsByOrderIdRequest request, ServerCallContext context)
        {
            return base.GetListOrderItemsByOrderId(request, context);
        }

    }
}
