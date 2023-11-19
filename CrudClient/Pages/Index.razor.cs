using CrudClient.Grpc.ProviderService;
using Microsoft.AspNetCore.Components;
using Google.Protobuf.WellKnownTypes;
using CrudClient.Grpc.OrderService;
using CrudClient.Model;
using Grpc.Core;

namespace CrudClient.Pages
{
    public partial class Index : ComponentBase
    {
        [Inject] public ProviderService.ProviderServiceClient ProviderService { get; set; }
        [Inject] public OrderService.OrderServiceClient OrderService { get; set; }
        [CascadingParameter] public List<Provider> Providers { get; set; }


        private bool _isLoad;
        protected override async Task OnInitializedAsync()
        {
            Providers = new List<Provider>();
            try
            {
                var replyListProvider = await ProviderService.GetListProvidersAsync(new Empty());
                Providers = replyListProvider.Provider.Select(item => new Provider { Id = item.Id, Name = item.Name }).ToList();
            }
            catch (RpcException ex)
            {
                await Console.Out.WriteLineAsync(ex.Status.Detail);
            }
        }
        protected override void OnParametersSet()
        {
            _isLoad = Providers is not null;
        }
    }
}
