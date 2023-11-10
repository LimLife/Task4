namespace ItemManagementSystem.Model.Entity
{
    public class OrderItem
    {
        public int Id { get; set; }
        public Order OrderId { get; set; }
        public string Name { get; set; }
        public decimal Quantity { get; set; }
        public string Unit { get; set; }
    }
}
