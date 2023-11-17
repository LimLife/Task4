namespace OrderManagementSystem.Model.EntityDTO
{
    public class FilterOrder
    {
        public string? Number { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int? ProviderID { get; set; }
    }
}
