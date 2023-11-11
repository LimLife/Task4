using Google.Protobuf.WellKnownTypes;
using Providers.Model.Repository;
using Providers.Grpc.Provider;
using Grpc.Core;

namespace Providers.Services
{
    public class ProviderApiService : ProviderService.ProviderServiceBase
    {
        private readonly IRepository _repository;

        public ProviderApiService(IRepository repository) => _repository = repository;

        public override async Task<ProviderReply> GetProviderById(GetRequestById request, ServerCallContext context)
        {
            var item = await _repository.ProviderByIdAsync(request.Id) ?? throw new RpcException(new Status(StatusCode.NotFound, $"Element by: {request.Id}"));
            return new ProviderReply
            {
                Id = item.Id,
                Name = item.Name,
            };
        }
        public override async Task<ProviderReply> GetProviderByName(GetRequestByName request, ServerCallContext context)
        {
            var item = await _repository.ProviderByNameAsync(request.Name) ?? throw new RpcException(new Status(StatusCode.NotFound, $"Element by: {request.Name}"));
            return new ProviderReply
            {
                Id = item.Id,
                Name = item.Name,
            };
        }
        public override async Task<ListProviderReply> GetListProviders(Empty request, ServerCallContext context)
        {
            var items = await _repository.ProvidersAsync() ?? throw new RpcException(new Status(StatusCode.NotFound, "Nothing found"));
            var replyList = items.Select(provider => new ProviderReply { Id = provider.Id, Name = provider.Name }).ToList();
            var list = new ListProviderReply();
            list.Provider.AddRange(replyList);
            return list;
        }

    }
}
