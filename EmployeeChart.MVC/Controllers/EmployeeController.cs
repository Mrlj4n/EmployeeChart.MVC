using EmployeeChart.MVC.Models;
using EmployeeChart.MVC.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EmployeeChart.MVC.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly EmployeeService _employeeService;

        public EmployeeController(IConfiguration configuration, IHttpClientFactory httpClientFactory, EmployeeService employeeService)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
            _employeeService = employeeService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();

            var apiKey = _configuration["ApiSettings:EmployeeApiKey"];
            var response = await client.GetStringAsync($"https://rc-vault-fap-live-1.azurewebsites.net/api/gettimeentries?code={apiKey}");

            var employees = JsonConvert.DeserializeObject<List<Employee>>(response);
            var viewModelList = _employeeService.ProcessEmployees(employees);

            return View(viewModelList);
        }
    }
}
