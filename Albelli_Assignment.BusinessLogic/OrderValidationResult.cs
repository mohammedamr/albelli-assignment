using System;
using System.Collections.Generic;
using System.Text;

namespace Albelli_Assignment.BusinessLogic
{
    public class OrderValidationResult
    {
        public List<string> Errors = new List<string>();
        public bool NoErrors() => Errors.Count == 0;
    }
}
