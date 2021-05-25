using Albelli_Assignment.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Albelli_Assignment.BusinessLogic
{
    public interface IBinMinWidthCalculator
    {
        decimal Calculate(Order order);
    }
}
