namespace CrudClient.Model
{
    public class FilterOrderModel
    {
        public string Number { get; set; }
        public DateTime Start { get; set; } = DateTime.Today.AddMonths(-1);
        public DateTime End { get; set; } = DateTime.Now;
        public Provider Provider { get; set; }
    }
}
