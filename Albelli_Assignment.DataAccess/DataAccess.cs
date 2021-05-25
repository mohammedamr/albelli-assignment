using Albelli_Assignment.DataAccess.Interfaces;
using Albelli_Assignment.Entities;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Albelli_Assignment.DataAccess
{
    public class DataAccess : IDataAccess
    {
        private DatabaseContext _context;

        public DataAccess(DatabaseContext context)
        {
            // Write code to initialize data access layer using database connection string
            _context = context;
        }

        #region Implementation

        public async Task<bool> AddOrder(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return true;
        }
        public async Task<IEnumerable<Order>> GetOrders()
        {
            return await _context.Orders.Include("Items").Include("Items.ProductType").ToArrayAsync();
        }
        public async Task<Order> GetOrderById(int id)
        {
            return await _context.Orders.Include("Items").Include("Items.ProductType").FirstOrDefaultAsync(o => o.OrderID == id);
        }

        public async Task<ProductType> GetProductTypeById(int productTypeID)
        {
            return await _context.ProductTypes.FirstOrDefaultAsync(pt => pt.ID == productTypeID);
        }

        #endregion
    }
}
