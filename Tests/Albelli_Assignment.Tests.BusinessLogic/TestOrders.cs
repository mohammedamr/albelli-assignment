using Albelli_Assignment.BusinessLogic.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace Albelli_Assignment.Tests.BusinessLogic
{
    [TestClass]
    public class TestOrders
    {
        [TestMethod("1- Test adding normal order (Happy Scenario)")]
        public void TestAddNormalOrder()
        {
            var newOrder = new OrderDTO
            {
                CustomerName = "Test customer",
                Items = new List<OrderItemDTO>
                {
                    new OrderItemDTO { ProductTypeID = 1, Quantity = 2 }, // photobook
                }
            };

            var result = Helpers.TestAddOrderWith(newOrder);

            Assert.IsTrue(result.result.Success);

            Assert.AreEqual(newOrder.CustomerName, result.order.CustomerName);
            Assert.AreEqual(newOrder.Items.Count, result.order.Items.Count);
            Assert.IsNotNull(result.order.Items[0].ProductType);
            Assert.AreEqual(2, result.order.Items[0].Quantity);
        }

        [TestMethod("2- Test adding order with no items")]
        public void TestAddOrderWithNoItems()
        {
            var newOrder = new OrderDTO
            {
                CustomerName = "Test customer",
                Items = new List<OrderItemDTO>
                {
                }
            };

            var result = Helpers.TestAddOrderWith(newOrder);

            Assert.IsFalse(result.result.Success);
        }

        [TestMethod("3- Test required bin width logic")]
        public void TestAddRequiredBinWidth()
        {
            var newOrder = new OrderDTO
            {
                CustomerName = "Test customer",
                Items = new List<OrderItemDTO>
                {
                    new OrderItemDTO { ProductTypeID = 1, Quantity = 2 }, // photobook
                }
            };

            var result = Helpers.TestAddOrderWith(newOrder);

            Assert.AreEqual(2 * 19, result.result.RequiredBinWidth);
        }

        [TestMethod("4- Test required bin width edge case 1 [|1.]")]
        public void TestRequiredBinWidthEdgeCase1()
        {
            var newOrder = new OrderDTO
            {
                CustomerName = "Test customer",
                Items = new List<OrderItemDTO>
                {
                    new OrderItemDTO { ProductTypeID = 1, Quantity = 1 }, // photobook
                    new OrderItemDTO { ProductTypeID = 5, Quantity = 1 }, // mug
                }
            };

            var result = Helpers.TestAddOrderWith(newOrder);

            Assert.AreEqual(19 + 94, result.result.RequiredBinWidth);
        }

        [TestMethod("5- Test required bin width edge case 2 [|4.]")]
        public void TestRequiredBinWidthEdgeCase2()
        {
            var newOrder = new OrderDTO
            {
                CustomerName = "Test customer",
                Items = new List<OrderItemDTO>
                {
                    new OrderItemDTO { ProductTypeID = 1, Quantity = 1 }, // photobook
                    new OrderItemDTO { ProductTypeID = 5, Quantity = 4 }, // mug
                }
            };

            var result = Helpers.TestAddOrderWith(newOrder);

            Assert.AreEqual(19 + 94, result.result.RequiredBinWidth);
        }

        [TestMethod("6- Test required bin width edge case 3 [|4.1.]")]
        public void TestRequiredBinWidthEdgeCase3()
        {
            var newOrder = new OrderDTO
            {
                CustomerName = "Test customer",
                Items = new List<OrderItemDTO>
                {
                    new OrderItemDTO { ProductTypeID = 1, Quantity = 1 }, // photobook
                    new OrderItemDTO { ProductTypeID = 5, Quantity = 5 }, // mug
                }
            };

            var result = Helpers.TestAddOrderWith(newOrder);

            Assert.AreEqual(19 + 2 * 94, result.result.RequiredBinWidth);
        }

        [TestMethod("7- Test products grouping in add order")]
        public void TestProductsGrouping()
        {
            var newOrder = new OrderDTO
            {
                CustomerName = "Test customer",
                Items = new List<OrderItemDTO>
                {
                    new OrderItemDTO { ProductTypeID = 1, Quantity = 1 }, // photobook
                    new OrderItemDTO { ProductTypeID = 5, Quantity = 1 }, // mug
                    new OrderItemDTO { ProductTypeID = 1, Quantity = 1 }, // photobook
                }
            };

            var result = Helpers.TestAddOrderWith(newOrder);

            Assert.AreEqual(2, result.order.Items.Count);
            Assert.AreEqual(2, result.order.Items.Where(i => i.ProductType.ID == 1).Sum(i => i.Quantity));
            Assert.AreEqual(1, result.order.Items.Where(i => i.ProductType.ID == 5).Sum(i => i.Quantity));
        }

        [TestMethod("3- Test required bin width with fraction")]
        public void TestRequiredBinWidthWithFraction()
        {
            var newOrder = new OrderDTO
            {
                CustomerName = "Test customer",
                Items = new List<OrderItemDTO>
                {
                    new OrderItemDTO { ProductTypeID = 4, Quantity = 2 }, // cards set
                    new OrderItemDTO { ProductTypeID = 3, Quantity = 1 }, // canvas
                }
            };

            var result = Helpers.TestAddOrderWith(newOrder);

            Assert.AreEqual(2 * 4.7m + 16, result.result.RequiredBinWidth);
        }
    }
}
