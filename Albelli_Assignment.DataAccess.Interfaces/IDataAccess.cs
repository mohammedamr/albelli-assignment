using Albelli_Assignment.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Albelli_Assignment.DataAccess.Interfaces
{
    public interface IDataAccess
    {
        Task<bool> AddOrder(Order order);
        Task<IEnumerable<Order>> GetOrders();
        Task<Order> GetOrderById(int id);
        Task<ProductType> GetProductTypeById(int productTypeID);
    }
}
