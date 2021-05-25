using Albelli_Assignment.BusinessLogic.Interfaces;
using Albelli_Assignment.DataAccess.Interfaces;
using Albelli_Assignment.Entities;
using System.Linq;
using System.Collections.Generic;
using Albelli_Assignment.BusinessLogic.Models;
using System.Threading.Tasks;
using System;

namespace Albelli_Assignment.BusinessLogic
{
    public class BusinessLogic : IBusinessLogic
    {
        private readonly IDataAccess _dataAccess;
        private readonly IBinMinWidthCalculator _binMinWidthCalculator;

        #region Contructor

        public BusinessLogic(IDataAccess dataAccess, IBinMinWidthCalculator binMinWidthCalculator)
        {
            _dataAccess = dataAccess;
            _binMinWidthCalculator = binMinWidthCalculator;
        }

        #endregion

        #region Implementation

        public async Task<IAddOrderResult> AddOrder(OrderDTO orderDto)
        {
            var validationResult = ValidateOrder(orderDto);

            if (validationResult.NoErrors())
            {
                // create order object from the DTO to be saved to the database
                var order = CreateOrder(orderDto);

                try
                {
                    var success = await _dataAccess.AddOrder(order);

                    if (success)
                    {
                        // adding order succeeded

                        // calculate bin minimum width
                        var binMinWidth = _binMinWidthCalculator.Calculate(order);

                        return new AddOrderResult
                        {
                            Success = success,
                            RequiredBinWidth = binMinWidth
                        };

                    }
                    else
                        return new AddOrderResult { Success = false };
                }
                catch (Exception ex)
                {
                    return new AddOrderResult { Success = false, Errors = new string[] { ex.Message } };
                }
            }
            else
                return new AddOrderResult { Success = false, Errors = validationResult.Errors.ToArray() };
        }

        public async Task<IEnumerable<OrderModel>> GetOrders()
        {
            var orders = await _dataAccess.GetOrders();

            return from order in orders
                   select GetModel(order);
        }

        public async Task<OrderModel> GetOrderById(int id)
        {
            var order = await _dataAccess.GetOrderById(id);

            if (order != null)
                return GetModel(order);
            else
                return null;
        }

        #endregion

        #region Helper Methods

        private OrderModel GetModel(Order order)
        {
            return new OrderModel
            {
                OrderID = order.OrderID,
                CustomerName = order.CustomerName,
                Items = from item in order.Items
                        select new OrderItemModel
                        {
                            ProductTypeID = item.ProductType.ID,
                            ProductTypeName = item.ProductType.Name,
                            Quantity = item.Quantity
                        },
                RequiredBinWidth = _binMinWidthCalculator.Calculate(order)
            };
        }

        private OrderValidationResult ValidateOrder(OrderDTO order)
        {
            var result = new OrderValidationResult();
            // Do some code to validate the order
            if (order.Items.Count < 1)
                result.Errors.Add("Order must have at least one item.");

            return result;
        }

        private Order CreateOrder(OrderDTO orderDto)
        {
            // This is useful, if it is expected that one product type may appear in items more than once
            var groupedItems = GroupItemsByProductType(orderDto);

            return new Order
            {
                CustomerName = orderDto.CustomerName,
                Items = (from item in groupedItems
                         select new OrderItem
                         {
                             ProductType = _dataAccess.GetProductTypeById(item.Key).Result,
                             Quantity = (short)item.Sum(i => i.Quantity)
                         }).ToList()
            };
        }

        private static IEnumerable<IGrouping<int, OrderItemDTO>> GroupItemsByProductType(OrderDTO orderDto)
        {
            return orderDto.Items.GroupBy(i => i.ProductTypeID);
        }

        #endregion
    }
}
