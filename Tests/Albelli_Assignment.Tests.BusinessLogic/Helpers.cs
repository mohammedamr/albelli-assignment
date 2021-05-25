using Albelli_Assignment.BusinessLogic.Interfaces;
using Albelli_Assignment.BusinessLogic.Models;
using Albelli_Assignment.DataAccess;
using Albelli_Assignment.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Albelli_Assignment.Tests.BusinessLogic
{
    public static class Helpers
    {
        public static (IAddOrderResult result, Order order) TestAddOrderWith(OrderDTO newOrder)
        {
            Order orderToBeAdded = null;
            var businessLogic = new Albelli_Assignment.BusinessLogic.BusinessLogic(new DataAccessMocked
            {
                OnAddOrder = (order) =>
                {
                    orderToBeAdded = order;
                    return true;
                }
            }, new Albelli_Assignment.BusinessLogic.BinMinWidthCalculator());

            var result = businessLogic.AddOrder(newOrder).Result;

            return (result, orderToBeAdded);
        }
    }
}
