namespace OrderManagementSystem.Model.Entity
{
    public class Order
    {
        public int Id { get; set; }
        public int ProviderId { get; set; }
        public string Number { get; set; }
        public DateTime Date { get; set; }
    }
}
