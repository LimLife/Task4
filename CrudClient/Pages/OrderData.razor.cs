using Microsoft.AspNetCore.Components;
using CrudClient.Model;
using CrudClient.Tools;

namespace CrudClient.Pages
{
    public partial class OrderData : ComponentBase
    {
        [Inject] public NavigationManager NavigationManager { get; set; }
        [Parameter] public string OrderId { get; set; }
        private Order _order;
        private List<Provider> _providers;
        protected override void OnInitialized()
        {
            _order = new Order()
            {
                Id = 1,
                Number = "1231",
                DateTime = DateTime.Now,
                Provider = new Provider() { Id = 1, Name = "Anna" }
            };
            _providers = new List<Provider>()
            {
                 new Provider {Id =1, Name ="sdad"},
                new Provider {Id =2, Name ="Ira"},
                new Provider {Id =3, Name ="svas"}
            };
        }

        private async Task HandleValidSubmit()
        {
            if (!_order.HasDefaultValues())
            {
                await Console.Out.WriteLineAsync($"{_order.Id}:{_order.Number}: {_order.DateTime}:{_order.Provider.Name}");
            }
        }
        private void DeleteOrder()
        {
            NavigationManager.NavigateTo("/", true);
        }
    }
}
