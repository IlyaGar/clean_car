using CleanCar.DAL.Models;
using CleanCar.Logic.Models;
using CleanCar.Logic.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Results;

namespace CleanCar.WEB.Controllers
{
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class OrdersController : ApiController
    {
        private readonly IOrderService _service;

        public OrdersController(IOrderService service) => _service = service ?? throw new ArgumentNullException(nameof(service));

        [HttpGet]
        public async Task<JsonResult<IEnumerable<OrderDTO>>> Get(int id)
        {
            return Json(await _service.GetOrderDTOsByCustomerIdAsync(id));
        }

        [HttpPost]
        public async Task<JsonResult<string>> Post([FromBody] IEnumerable<Order> customerOperations)
        {
            await _service.CreateListOrderAsync(customerOperations);
            return Json("Ok");
        }
    }
}