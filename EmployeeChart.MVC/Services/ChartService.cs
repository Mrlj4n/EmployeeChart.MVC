using EmployeeChart.MVC.Models;
using System.Drawing;
using System.Drawing.Imaging;

namespace EmployeeChart.MVC.Services
{
    public class ChartService
    {
        public void CreatePieChartForEmployee(List<Employee> employeeEntries, string filePath)
        {
            double totalHoursWorked = employeeEntries.Sum(entry => (entry.EndTimeUtc - entry.StarTimeUtc).TotalHours);
            double standardWorkHours = 176; // Standard monthly working hours
            float workRatio = (float)(totalHoursWorked / standardWorkHours); // Actual ratio for worked hours, can exceed 100%
            float cappedWorkRatio = Math.Min(workRatio, 1f); // Cap it at 1 (or 100%)
            float excessRatio = Math.Max(0, workRatio - 1f); // Additional ratio for hours beyond the standard

            using (Bitmap bitmap = new Bitmap(300, 300))
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                Rectangle rect = new Rectangle(10, 10, 280, 280);

                Brush workBrush = new SolidBrush(Color.Blue); // Worked standard hours
                Brush excessBrush = new SolidBrush(Color.Red); // Exceeded hours
                Brush remainingBrush = new SolidBrush(Color.Gray); // Remaining standard hours, if not exceeded

                graphics.FillPie(workBrush, rect, 0, cappedWorkRatio * 360); // Worked hours segment within standard
                if (excessRatio > 0)
                {
                    graphics.FillPie(excessBrush, rect, cappedWorkRatio * 360, excessRatio * 360); // Excess hours segment
                }
                if (workRatio < 1)
                {
                    graphics.FillPie(remainingBrush, rect, cappedWorkRatio * 360, (1 - cappedWorkRatio) * 360); // Remaining hours
                }

                string totalPercentageText = $"{(workRatio * 100):N2}%";
                using (Font font = new Font("Arial", 16))
                {
                    Brush textBrush = new SolidBrush(Color.Black);
                    graphics.DrawString(totalPercentageText, font, textBrush, new PointF(120, 130)); 
                }

                string directoryPath = Path.GetDirectoryName(filePath);
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                bitmap.Save(filePath, ImageFormat.Png);
            }
        }
    }
}
