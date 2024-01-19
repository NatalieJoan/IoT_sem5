namespace Database.Entities.EmployeeLogs
{
    public class EmployeeLogsEntity
    {
        internal object EmployeeLogs;
        public int EmployeeLogsId { get; set; }
        public int EmployeeId { get; set; }
        public int Timestamp { get; set; }
    }
}