using OrderManagementSystem.Model.Repository;
using Grpc.Core.Interceptors;
using Grpc.Core;

namespace OrderManagementSystem.Middleware
{
    public class CheckDBConnect : Interceptor
    {
        private readonly IRepository _repository;

        public CheckDBConnect(IRepository repository)
        {
            _repository = repository;
        }

        public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(
            TRequest request,
            ServerCallContext context,
            UnaryServerMethod<TRequest, TResponse> continuation)
        {
            if (await _repository.IsConnectAsync())
                return await continuation(request, context);
            else
                throw new RpcException(new Status(StatusCode.Internal, "Database is not available"));

        }
    }
}

