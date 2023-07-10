using PracticePanther.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panther.Library.Models
{
    public class Time
    {

        public Time (DateTime entry, string narr, int hours, int pId, int eId)
        {
            DateEntry = entry;
            Narrative = narr;
            Hours = hours;
            pId = ProjectId;
            eId = EmployeeId;
        }

        public DateTime DateEntry { get; set; }
        public string DateDisplay { get {  return DateEntry.ToString(); } }
        public string Narrative { get; set; }

        public int Hours { get; set; }
        public string HoursDisplay { get { return Hours.ToString(); } }

        public int ProjectId { get; set; }
        public int EmployeeId { get; set; }
    }
}
