using Albelli_Assignment.BusinessLogic.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Albelli_Assignment.BusinessLogic.Interfaces
{
    public interface IBusinessLogic
    {
        Task<IAddOrderResult> AddOrder(OrderDTO order);
        Task<IEnumerable<OrderModel>> GetOrders();
        Task<OrderModel> GetOrderById(int id);
    }
}
