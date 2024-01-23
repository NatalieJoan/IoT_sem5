using System.Dynamic;
using Database.Entities.EmployeeLogs;
using Employees.Service;

namespace EmployeeLogs.Service
{
    public class EmployeeLogsService
    {

        private readonly EmployeesService employeesService = new EmployeesService();
        private List<TimeLog> EmployeeLogs { get; } = new List<TimeLog>();

        public EmployeeLogsService(EmployeesService _employeesService){
            this.employeesService = _employeesService;
        }
        public TimeLog LogTime(int employeeId)
        {
            var currentTime = DateTime.Now;
            var ifExists = EmployeeLogs
                .Where(t => t.EmployeeId == employeeId)
                .Select(t => t.EmployeeId)
                .FirstOrDefault();

            if (ifExists == 0)
            {
                var timelog = AddToTimestamps(employeeId, currentTime);
                return timelog;
            }
            else
            {
                UpdateEmployeeTotalHours(employeeId, (int)(currentTime - GetTime(ifExists)).TotalSeconds);
                RemoveFromTimestamps(employeeId);
                return null;
            }
        }
        public DateTime GetTime(int employeeId)
        {
            var timestamp = EmployeeLogs
                .Where(t => t.EmployeeId == employeeId)
                .Select(t => t.Timestamp)
                .FirstOrDefault();
            return timestamp;
        }
        public IEnumerable<TimeLog> Get()
        {
            return EmployeeLogs;
        }
        public TimeLog AddToTimestamps(int employeeId, DateTime timestamp)
        {
            var curtimestamp = new TimeLog
            {
                EmployeeId = employeeId,
                Timestamp = timestamp,
                EmployeeLogsId = EmployeeLogs.Count + 1
            };
            EmployeeLogs.Add(curtimestamp);
            return curtimestamp;
        }

        public void UpdateEmployeeTotalHours(int employeeId, int duration)
        {
            var employees = this.employeesService.Get();
            var employeefound = employees.FirstOrDefault(t => t.Id == employeeId);
            if (employeefound != null){
                this.employeesService.Update(employeefound.Id,employeefound._firstName, employeefound._lastName,employeefound._timeWorking + duration);
            }
            
        }

        public void RemoveFromTimestamps(int employeeId)
        {
            EmployeeLogs.RemoveAll(t => t.EmployeeId == employeeId);
        }
    }
    public class TimeLog
{
    public int EmployeeId { get; set; } = 0;
    public int EmployeeLogsId { get; set; } = 0;
    public DateTime Timestamp { get; set; } = DateTime.MinValue; 
}

}