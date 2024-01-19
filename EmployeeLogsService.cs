using Database.Entities.EmployeeLogs;

namespace EmployeeLogs.Service
{
    public class EmployeeLogsService
    {
        private List<TimeLog> EmployeeLogs { get; } = new List<TimeLog>();
        public void LogTime(int employeeId)
        {
            var currentTime = DateTime.Now;
            var existingTimestamp = EmployeeLogs.FirstOrDefault(t => t.EmployeeId == employeeId);

            if (existingTimestamp == null)
            {
                AddToTimestamps(employeeId, currentTime);
            }
            else
            {
                UpdateEmployeeTotalHours(employeeId, currentTime - existingTimestamp.Timestamp);
                RemoveFromTimestamps(employeeId);
            }
        }
        public TimeLog GetTime(int employeeId)
        {
            var timestamp = EmployeeLogs.FirstOrDefault(t => t.EmployeeId == employeeId);
            EmployeeLogs.Add(timestamp);
            return timestamp;

        }

        public void AddToTimestamps(int employeeId, DateTime timestamp)
        {
            var timestamps = EmployeeLogs.FirstOrDefault(t => t.EmployeeId == employeeId);
        }

        public void UpdateEmployeeTotalHours(int employeeId, TimeSpan duration)
        {
            Console.WriteLine($"Updating total hours for employee {employeeId} with duration: {duration.TotalHours} hours");
        }

        public void RemoveFromTimestamps(int employeeId)
        {
            EmployeeLogs.RemoveAll(t => t.EmployeeId == employeeId);
        }
    }
    public class TimeLog
    {
        public int EmployeeId { get; set; }
        public int EmployeeLogsId { get; set; }
        public int Timestamp { get; set; }
    }
}