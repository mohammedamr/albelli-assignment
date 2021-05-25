using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Albelli_Assignment.Entities
{
    public class OrderItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public int OrderID { get; set; }

        [ForeignKey("OrderID")]
        public Order Order { get; set; }

        public ProductType ProductType { get; set; }

        public short Quantity { get; set; }
    }
}