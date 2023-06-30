using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panther.Library.Models
{
    public class Time
    {

        public DateTime DateEntry { get; set; }

        public string Narrative { get; set; }
        public int Hours { get; set; }
        public int ProjectId { get; set; }
        public int EmployeeId { get; set; }
    }
}
