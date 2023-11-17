using Microsoft.AspNetCore.Components;
using CrudClient.Model;

namespace CrudClient.Shared.Item
{
    public partial class ItemsTable : ComponentBase
    {
        private OrderItem _item;
        private List<OrderItem> _items;
        protected override void OnInitialized()
        {
            _items = new List<OrderItem>()
            {
                new OrderItem
                {
                      Id =1,
                      Name = "Qwerty",
                      Quantity = 353431.008M,
                      Unit = "sdfsdf",
                       Order = new Order()
                },
                 new OrderItem
                {
                      Id =2,
                      Name = "Jora",
                      Quantity = 0.00003M,
                      Unit = "sdfsdf",
                      Order = new Order()
                }
            };
            _item = new OrderItem()
            {
                Name = "",
                Quantity = 0,
                Unit = "",
                Id = 1,
                Order = new Order
                {
                    Id = 1,
                    DateTime = DateTime.Now,
                    Number = "",
                    Provider = new Provider { Id = 1, Name = "" }
                }
            };
        }

        private async Task DeleteOrderItemAsync(int id)
        {
            await Console.Out.WriteLineAsync($"Deleted:{id}");
        }
    }
}
