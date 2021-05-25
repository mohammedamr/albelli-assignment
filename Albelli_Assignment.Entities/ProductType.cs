using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Albelli_Assignment.Entities
{
    public class ProductType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// The width of the unit when it is packed in the bin. This width is constant for any product type.
        /// </summary>
        public decimal Width { get; set; }

        /// <summary>
        /// The number of units that can be stacked on top of each other in the bin. This number is constant for any product type.
        /// </summary>
        public byte BinStackCapacity { get; set; }
    }
}