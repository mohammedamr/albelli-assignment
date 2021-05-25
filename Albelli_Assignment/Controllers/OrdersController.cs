using Albelli_Assignment.BusinessLogic.Interfaces;
using Albelli_Assignment.BusinessLogic.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Albelli_Assignment.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : Controller
    {
        private readonly IBusinessLogic _businessLogic;

        private readonly ILogger<OrdersController> _logger;

        #region Constructor

        public OrdersController(ILogger<OrdersController> logger, IBusinessLogic businessLogic)
        {
            _businessLogic = businessLogic;
            _logger = logger;
        }

        #endregion

        [HttpGet]
        public async Task<IEnumerable<OrderModel>> Get()
        {
            return await _businessLogic.GetOrders();
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<OrderModel>> Get(int id)
        {
            var order = await _businessLogic.GetOrderById(id);

            if (order == null)
                return NotFound();

            return order;
        }

        [HttpPost]
        public async Task<ActionResult<IAddOrderResult>> Post(OrderDTO order)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _businessLogic.AddOrder(order);

            if (result.Success)
                return Json(result);
            else
                return BadRequest(result);
        }
    }
}
