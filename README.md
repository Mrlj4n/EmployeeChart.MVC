# Employee Work Hours Visualization

This ASP.NET Core MVC project visualizes employee work hours, displaying data in both a tabular format and as individual pie charts for each employee.

## Features

- **Employee Table**: Lists employees with their total work hours, ordered by total time worked.
- **Work Hours Pie Chart**: Visualizes individual employee work hours, showing standard, excess, and remaining hours.

## Getting Started

These instructions will help you get the project up and running on your local machine for development and testing.

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- An IDE like [Visual Studio](https://visualstudio.microsoft.com/vs/), [VS Code](https://code.visualstudio.com/) with the C# extension, or similar.

### Installation

1. Clone the repository to your local machine.
2. Open the solution file (`EmployeeChart.sln`) in your IDE.
3. Restore the required NuGet packages by running `dotnet restore`.
4. Add your API key to `appsettings.Development.json`:

    ```json
    "ApiSettings": {
      "EmployeeApiKey": "Your_Api_Key_Here"
    }
    ```

    Replace `"Your_Api_Key_Here"` with your actual API key.

5. Build the solution (`dotnet build`).
6. Start the application (`dotnet run` or through your IDE).

## Usage

1. Visit the homepage to view the employee table.
2. Click an employee's name to see detailed work hours and a pie chart visualization.

## Development Notes

- `EmployeeService` aggregates employee time entries.
- `ChartService` generates pie charts, detailing work hours distribution.


