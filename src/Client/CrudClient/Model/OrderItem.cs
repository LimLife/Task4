using System.ComponentModel.DataAnnotations;

namespace CrudClient.Model
{
    public class OrderItem
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Не может быть пустым")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Длина должна быть от {2} до {1}")]
        public string Name { get; set; }
        [Required (ErrorMessage ="Не может быть пустым")]
 
        [RegularExpression(@"^(\d{1,15}(\.\d{1,3})?|0\.\d{1,3})$", ErrorMessage = "Не корректный ввод")]
        public decimal Quantity { get; set; }
        [Required(ErrorMessage = "Не может быть пустым")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Длина должна быть от {2} до {1}")]
        public string Unit { get; set; }

        public Order Order { get; set; }

    }
}
