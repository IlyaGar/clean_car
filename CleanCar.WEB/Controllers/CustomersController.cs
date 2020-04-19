using CleanCar.DAL.Models;
using CleanCar.DAL.Repositories.IRepositories;
using CleanCar.Logic.Models;
using CleanCar.Logic.Services.IServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Results;

namespace CleanCar.WEB.Controllers
{
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class CustomersController : ApiController
    {
        private readonly ICustomerService _customerService;
        private readonly IPdfReportService _pdfReportService;

        public CustomersController(ICustomerService customerService, IPdfReportService pdfReportService)
        {
            _customerService = customerService;
            _pdfReportService = pdfReportService;
        }

        [HttpGet]
        public async Task<JsonResult<IEnumerable<CustomerDTO>>> Get()
        {
            var r = await _customerService.GetCustomersAsync();
            return Json(r);
        }

        [HttpGet]
        public async Task<HttpResponseMessage> Get([FromUri] int id)
        {
            var pathFile = await _pdfReportService.GetFilePath(id);
            var dataBytes = File.ReadAllBytes(pathFile);

            var response = new HttpResponseMessage();
            response.Content = new ByteArrayContent(dataBytes);
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = Path.GetFileName(pathFile)
            };
            return response;
        }

        [HttpPost]
        public async Task<JsonResult<int>> Post([FromBody] Customer customer)
        {
            return Json(await _customerService.CreateCustomerAsync(customer));
        }
    }
}
