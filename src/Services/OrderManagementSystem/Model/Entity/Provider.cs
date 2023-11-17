namespace OrderManagementSystem.Model.Entity
{
    public class Provider
    {
        public Provider()
        {
            Orders = new List<Order>(); 
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
