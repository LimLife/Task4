using OrderManagementSystem.Model.Repository.ProviderRepository;
using OrderManagementSystem.Grpc.ProviderService;
using OrderManagementSystem.Model.Repository;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace OrderManagementSystem.Services
{
    public class ProviderApiService : ProviderService.ProviderServiceBase
    {
        private readonly IProviderRepository _repository;
        public ProviderApiService(IRepository repository) => _repository = repository;

        public override async Task<ListProviderReply> GetListProviders(Empty request, ServerCallContext context)
        {
            var items = await _repository.GetProvidersAsync() ?? throw new RpcException(new Status(StatusCode.NotFound, "Nothing found"));
            var replyList = items.Select(provider => new ProviderReply { Id = provider.Id, Name = provider.Name }).ToList();
            var list = new ListProviderReply();
            list.Provider.AddRange(replyList);
            return list;
        }
    }
}
