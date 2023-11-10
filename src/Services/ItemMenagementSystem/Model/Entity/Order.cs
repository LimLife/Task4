namespace ItemManagementSystem.Model.Entity
{
    public class Order
    {
        public Order()
        {
            Items = new List<OrderItem>();
        }
        public int Id { get; set; }
        public int OrderId { get; set; }

        public ICollection<OrderItem> Items { get; set; }
    }
}
