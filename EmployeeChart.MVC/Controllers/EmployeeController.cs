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
        private readonly ChartService _chartService;

        public EmployeeController(IConfiguration configuration, IHttpClientFactory httpClientFactory, EmployeeService employeeService, ChartService chartService)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
            _employeeService = employeeService;
            _chartService = chartService;
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

        public async Task<IActionResult> Detail(string name)
        {
            // I assume you're fetching all employee data from an API or similar:
            var client = _httpClientFactory.CreateClient();
            var apiKey = _configuration["ApiSettings:EmployeeApiKey"];
            var response = await client.GetStringAsync($"https://rc-vault-fap-live-1.azurewebsites.net/api/gettimeentries?code={apiKey}");
            var employees = JsonConvert.DeserializeObject<List<Employee>>(response);

            // Use your service to filter data for the specific employee
            List<Employee> employeeEntries = _employeeService.GetEmployeeEntriesByName(employees, name);

            // Now continue with your chart generation logic...
            string safeName = name.Replace(" ", ""); // Avoid spaces in file names
            string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", $"{safeName}PieChart.png");
            _chartService.CreatePieChartForEmployee(employeeEntries, imagePath); // Ensure this method matches your implementation

            ViewBag.ImagePath = $"/images/{safeName}PieChart.png";
            return View();
        }
    }
}
