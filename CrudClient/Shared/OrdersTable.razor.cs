using Microsoft.AspNetCore.Components;
using CrudClient.Model;

namespace CrudClient.Shared
{
    public partial class OrdersTable : ComponentBase
    {
        [Parameter] public List<Order> Orders { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        private Order _order;

        protected override void OnInitialized()
        {
            _order = new Order();
        }
        protected override void OnParametersSet()
        {
           // Orders = new List<Order>();
        }
        private void NavigateToOrder(int orderId)
        {
            NavigationManager.NavigateTo($"/order/{orderId}");
        }
    }
}
