using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Albelli_Assignment.Entities
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderID { get; set; }

        // For simplicity, I'm using the customer name directly here in the order
        // Otherwise, it should have a separate structure in the database
        public string CustomerName { get; set; }

        public IList<OrderItem> Items { get; set; } = new List<OrderItem>();
    }
}
