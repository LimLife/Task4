using Microsoft.AspNetCore.Components;
using CrudClient.Model;

namespace CrudClient.Shared
{
    public partial class OrderCreated : ComponentBase
    {
        private Order _order;
        private List<Provider> _providers;

        protected override void OnInitialized()
        {
            _order = new Order();
            _providers = new List<Provider>()
            {
                new Provider() { Id =1, Name ="Alla"},
                new Provider() { Id =2, Name ="Ira"},
                new Provider() { Id =3, Name ="Eva"},
                new Provider() { Id =4, Name ="Anjela"},
                new Provider() { Id =5, Name ="Alesya"}
            };
            _order.Provider = _providers.First();
        }

        private async Task CreatedOrder()
        {
            await Task.Delay(1000);
            await Console.Out.WriteLineAsync($"{_order.Id}:{_order.Number}: {_order.DateTime}:{_order.Provider.Name}");
            await Console.Out.WriteLineAsync("Created");
        }
    }
}
