using PracticePanther.Models;

namespace Panther.Library.Models
{
    public class Time
    {
        // Assigns needed values
        public Time (DateTime entry, int hours, Project pId, Employee eId)
        {
            DateEntry = entry;
            Hours = hours;
            project = pId;
            employee = eId;
            Id = ++NumberTimes;
        }

        private static int NumberTimes = 0;
        public int Id; 

        public DateTime DateEntry { get; set; }
        public string DateDisplay { get {  return DateEntry.ToString("MM/dd/yyyy"); } }
        public string Narrative { get; set; }

        public int Hours { get; set; }
        public string HoursDisplay { get { return Hours.ToString(); } }

        // Assigned Project and Employees
        public int ProjectId { get { return project.Id; } }
        public Project project { get; set; }
        public int EmployeeId { get { return employee.Id; } }
        public Employee employee { get; set; }
    }
}
