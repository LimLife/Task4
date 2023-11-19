namespace OrderManagementSystem.Model.EntityDTO
{
    public class Filter
    {
        public string? Number { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int? ProviderID { get; set; }
        public string? Name { get; set; }
        public string? Unit { get; set; }
    }
}
