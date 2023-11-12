using Grpc.Core;
using Google.Protobuf.WellKnownTypes;
using OrderManagementSystem.Grpc.Provider;
using OrderManagementSystem.Model.Repository;
using OrderManagementSystem.Model.Repository.ProviderRepository;

namespace OrderManagementSystem.Services
{
    public class ProviderApiService : ProviderService.ProviderServiceBase
    {
        private readonly IProviderRepository _repository;
        public ProviderApiService(IRepository repository) => _repository = repository;


        public override async Task<ListProviderReply> GetProviders(Empty request, ServerCallContext context)
        {
            var item = await _repository.GetProvidersAsync() ?? throw new RpcException(new Status(StatusCode.NotFound, "Nothing elements"));
            var reply = new ListProviderReply();
            var collectionReply = item.Select(provider => new ProviderReply { Id = provider.Id, ProviderId = provider.ProviderId });
            reply.Provider.AddRange(collectionReply);
            return reply;
        }
        public override async Task<ProviderReply> GetProviderById(GetProvidersByIdReques request, ServerCallContext context)
        {
            var item = await _repository.GetProviderByIdAsync(request.Id) ?? throw new RpcException(new Status(StatusCode.NotFound, $"Element by id: {request.Id}"));
            return new ProviderReply
            {
                Id = item.Id,
                ProviderId = item.ProviderId
            };
        }


    }
}
