using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panther.Library.Models
{
    public class Employee
    {

        public Employee ()
        {
            Id = ++EmployeeCreated;
        }

        private static int EmployeeCreated = 0;
        public int Id { get; set; }
        public string IdDisplay { get { return Id.ToString(); } }

        public string Name { get; set; }
        public double? Rate { get; set; }
        public string RateDisplay { get { return Rate.ToString(); } }
        public override string ToString()
        { return $"{Name}"; }
    }
}
