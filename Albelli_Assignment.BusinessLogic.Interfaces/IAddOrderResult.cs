using System;
using System.Collections.Generic;
using System.Text;

namespace Albelli_Assignment.BusinessLogic.Interfaces
{
    public interface IAddOrderResult
    {
        bool Success { get; }
        decimal RequiredBinWidth { get; }
        string[] Errors { get; }
    }
}
