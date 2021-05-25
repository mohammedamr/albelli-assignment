namespace Albelli_Assignment.BusinessLogic.Interfaces
{
    public class AddOrderResult : IAddOrderResult
    {
        public bool Success { get; set; }
        public decimal RequiredBinWidth { get; set; }
        public string[] Errors { get; set; }
    }
}
