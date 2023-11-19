using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrudClient.Model
{
    public class OrderItem
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Не может быть пустым")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Длина должна быть от {2} до {1}")]
        public string Name { get; set; }
        [Required (ErrorMessage ="Не может быть пустым")]
        [Column(TypeName = "decimal(18, 3)")]
        [RegularExpression("^(0\\.0[1-9][0-9]*|[1-9][0-9]*\\.?[0-9]*)$", ErrorMessage = "Не может быть негативным")]
        public decimal Quantity { get; set; }
        [Required(ErrorMessage = "Не может быть пустым")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Длина должна быть от {2} до {1}")]
        public string Unit { get; set; }

        public Order Order { get; set; }

    }
}
