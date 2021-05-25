using Albelli_Assignment.BusinessLogic.Interfaces;
using Albelli_Assignment.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Albelli_Assignment.BusinessLogic
{
    public class BinMinWidthCalculator : IBinMinWidthCalculator
    {
        public decimal Calculate(Order order)
        {
            // I use the following formula a lot in this kind of problems
            // It uses the integer division with subtracting 1 first to keep the last item that is
            // supposed to be on the same stack in place. Then it adds 1 to compensate for the
            // missing stack that the integer division will eat.

            decimal minWidth = 0;

            foreach (var item in order.Items)
                minWidth += ((item.Quantity - 1) / item.ProductType.BinStackCapacity + 1) * item.ProductType.Width;

            return minWidth;
        }
    }
}
