using Microsoft.AspNetCore.Components;
using CrudClient.Model;

namespace CrudClient.Shared
{
    public partial class FilterOrder : ComponentBase
    {
        private List<Provider> Providers { get; set; }
        private List<Order> _orders;
        private FilterOrderModel _filter;
        protected override void OnInitialized()
        {
            _filter = new FilterOrderModel();
            Providers = new List<Provider>()
            {
                new Provider {Id =1, Name ="sdad"},
                new Provider {Id =2, Name ="Ira"},
                new Provider {Id =3, Name ="svas"}
            };
            _orders = new List<Order>()
            {
                new Order() { Id = 1, Number = "3213123", DateTime = DateTime.Now , Provider = new Provider {Id =1, Name ="sdad"} },
                new Order() { Id = 2, Number = "32dds123", DateTime = DateTime.Now , Provider = new Provider {Id =2, Name ="Ira"} },
                new Order() { Id = 3, Number = "qqq", DateTime = DateTime.Now , Provider = new Provider {Id =3, Name ="svas"} },
                new Order() { Id = 4, Number = "vcvcxv", DateTime = DateTime.Now , Provider = new Provider {Id =4, Name ="shhh"} },
                new Order() { Id = 5, Number = "]a]aa]a]", DateTime = DateTime.Now , Provider = new Provider {Id =5, Name ="sddfdfdad"} },
                new Order() { Id = 6, Number = "fdfdfsf", DateTime = DateTime.Now , Provider = new Provider {Id =6, Name ="dddd"} }
            };
            _filter.Provider = Providers[0];
        }
        private void GetFilterElementsAsync()
        {
            Console.WriteLine($"{_filter.Number}:{_filter.Start}:{_filter.End}:{Providers[_filter.Provider.Id-1].Name}");
        }

    }
}
