namespace OrderManagementSystem.Model.Entity
{
    public class Order
    {
        public Order()
        {
            OrderItems = new List<OrderItem>();
        }
        public int Id { get; set; }
        public int ProviderId { get; set; }
        public Provider Provider { get; set; }
        public string Number { get; set; }
        public DateTime Date { get; set; }
        public List<OrderItem> OrderItems { get; set; }

    }
}
