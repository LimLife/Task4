using System.ComponentModel.DataAnnotations;

namespace CrudClient.Model
{
    public class Order
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Не может быть пустым")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Длина должна быть от {2} до {1}")]
        public string Number { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DateTime { get; set; } = DateTime.Now;
        [Required(ErrorMessage = "Не может быть пустым")]
        public Provider Provider { get; set; }
    }
}
