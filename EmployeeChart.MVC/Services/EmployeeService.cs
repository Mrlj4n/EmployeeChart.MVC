using EmployeeChart.MVC.Models;

namespace EmployeeChart.MVC.Services
{
    public class EmployeeService
    {
        public List<EmployeeTimeViewModel> ProcessEmployees(List<Employee> employees)
        {
            return employees
                .Where(entry => entry.DeletedOn == null)
                .Select(entry =>
                {
                    entry.EmployeeName = string.IsNullOrWhiteSpace(entry.EmployeeName) ? "Unknown Employee" : entry.EmployeeName;
                    return entry;
                })
                .GroupBy(e => e.EmployeeName)
                .Select(group => new EmployeeTimeViewModel
                {
                    EmployeeName = group.Key,
                    TotalTimeWorked = group.Sum(g => (g.EndTimeUtc - g.StarTimeUtc).TotalHours)
                })
                .OrderByDescending(e => e.TotalTimeWorked)
                .ToList();
        }
    }
}
