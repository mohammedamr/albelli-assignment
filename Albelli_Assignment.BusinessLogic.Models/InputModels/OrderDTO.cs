using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Albelli_Assignment.BusinessLogic.Models
{
    public class OrderDTO
    {
        [Required]
        public string CustomerName { get; set; }
        public IList<OrderItemDTO> Items { get; set; } = new List<OrderItemDTO>();
    }
}
