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
            try
            {
                return await _repository.GetProvidersAsync() ?? throw new RpcException(new Status(StatusCode.NotFound, "Nothing found"));
            }
            catch (Exception)
            {
                return new ListProviderReply();
            }
        }
    }
}
