namespace CrudClient.Model
{
    public class FilterModel
    {
        public string? Number { get; set; }
        public DateTime Start { get; set; } = DateTime.Today.AddMonths(-1);
        public DateTime End { get; set; } = DateTime.Now;
        public Provider? Provider { get; set; }

        public string? Name { get; set; }
        public string? Unit { get; set; }
    }
}
