using Microsoft.AspNetCore.Components;
using CrudClient.Model;
using CrudClient.Tools;

namespace CrudClient.Shared.Item
{
    public partial class ItemEdit : ComponentBase
    {
        [Parameter] public int ItemID { get; set; }
        private OrderItem _orderItem;

        private List<OrderItem> _orderItems;
        protected override void OnInitialized()
        {
            _orderItems = new List<OrderItem>()
            {
                new OrderItem
                {
                      Id =1,
                      Name = "Qwerty",
                      Quantity = 353431.008M,
                      Unit = "sdfsdf"
                },
                 new OrderItem
                {
                      Id =2,
                      Name = "Jora",
                      Quantity = 0.00003M,
                      Unit = "sdfsdf"
                },
                  new OrderItem
                {
                      Id =3,
                      Name = "Varenta",
                      Quantity = 2.00003M,
                      Unit = "sdfsdf"
                },
                   new OrderItem
                {
                      Id =4,
                      Name = "Genneya",
                      Quantity = 10.02003M,
                      Unit = "sdfsdf"
                }
            };
            _orderItem = new OrderItem()
            {
                Id = 4,
                Name = "dsfsdf",
                Quantity = 4,
                Unit = "fdfdfdf"
            };
        }

        private async Task ApplayChangesAsync()
        {
            await Console.Out.WriteLineAsync("Work");
            var s = Tool.HasDefaultValues(_orderItem);
          
            if (!_orderItem.HasDefaultValues()) //Don`t working fixed
            {
                await Console.Out.WriteLineAsync("Added Changes");
            }
        }
    }
}
