using System.Collections.Generic;

namespace Albelli_Assignment.BusinessLogic.Models
{
    public class OrderModel
    {
        public int OrderID { get; set; }
        public string CustomerName { get; set; }
        public IEnumerable<OrderItemModel> Items { get; set; }
        public decimal RequiredBinWidth { get; set; }
    }
}
