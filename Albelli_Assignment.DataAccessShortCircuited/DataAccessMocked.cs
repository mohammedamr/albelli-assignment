using Albelli_Assignment.DataAccess.Interfaces;
using Albelli_Assignment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albelli_Assignment.DataAccess
{
    /// <summary>
    /// This class short-circuits the data access layer.
    /// </summary>
    public class DataAccessMocked : IDataAccess
    {
        #region Events

        public Func<Order, bool> OnAddOrder = null;
        public Func<IEnumerable<Order>> OnGetOrders = null;
        public Func<int, Order> OnGetOrderById = null;
        public Func<int, ProductType> OnGetProductTypeById = null;

        #endregion

        #region Repository

        // For simplicity, I short-circuited database by implementing the in-memory repository

        private static List<Order> Orders = new List<Order>();
        public static Dictionary<int, ProductType> ProductTypes = new Dictionary<int, ProductType>();

        static DataAccessMocked()
        {
            // Populating the repository with some data

            var photoBook = new ProductType { ID = 1, Name = "Photobook", Width = 19, BinStackCapacity = 1 };
            var calendar = new ProductType { ID = 2, Name = "Calendar", Width = 10, BinStackCapacity = 1 };
            var canvas = new ProductType { ID = 3, Name = "Canvas", Width = 16, BinStackCapacity = 1 };
            var cardsSet = new ProductType { ID = 4, Name = "Cards Set", Width = 4.7m, BinStackCapacity = 1 };
            var mug = new ProductType { ID = 5, Name = "Mug", Width = 94, BinStackCapacity = 4 };

            ProductTypes.Add(1, photoBook);
            ProductTypes.Add(2, calendar);
            ProductTypes.Add(3, canvas);
            ProductTypes.Add(4, cardsSet);
            ProductTypes.Add(5, mug);

            Orders.Add(new Order
            {
                OrderID = 1,
                CustomerName = "Mohammed Amr",
                Items = new List<OrderItem>
                {
                    new OrderItem { ProductType = photoBook, Quantity = 5 },
                    new OrderItem { ProductType = mug, Quantity = 5 },
                }
            });
        }

        #endregion

        public DataAccessMocked()
        {
        }

        #region Implementation

        public async Task<bool> AddOrder(Order order)
        {
            OnAddOrder?.Invoke(order);
            
            order.OrderID = Orders.Count + 1;
            await Task.Run(() => Orders.Add(order));
            return true;
        }
        public async Task<IEnumerable<Order>> GetOrders()
        {
            OnGetOrders?.Invoke();

            return await Task.Run(() => Orders);
        }
        public async Task<Order> GetOrderById(int id)
        {
            OnGetOrderById?.Invoke(id);

            return await Task.Run(() => Orders.FirstOrDefault(o => o.OrderID == id));
        }

        public async Task<ProductType> GetProductTypeById(int productTypeID)
        {
            OnGetProductTypeById?.Invoke(productTypeID);

            return await Task.Run(() =>
            {
                if (ProductTypes.ContainsKey(productTypeID))
                    return ProductTypes[productTypeID];
                else
                    return null;
            });
        }

        #endregion
    }
}
