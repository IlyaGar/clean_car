using CleanCar.DAL.Models;
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
    public class OperationsController : ApiController
    {
        private readonly IOperationService _service;

        public OperationsController(IOperationService service) => _service = service ?? throw new ArgumentNullException(nameof(service));

        [HttpGet]
        public async Task<JsonResult<IEnumerable<Operation>>> Get()
        {
            return Json(await _service.GetOperationsAsync());
        }
    }
}
