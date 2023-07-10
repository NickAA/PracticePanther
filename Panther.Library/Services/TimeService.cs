using Panther.Library.Models;
using PracticePanther.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panther.Library.Services
{
    public class TimeService
    {
        private TimeService()
        {
            times = new List<Time>();
        }

        public List<Time> times;

        private static TimeService? instance;
        private static object _lock = new object();

        public static TimeService Current
        {
            get
            {
                lock (_lock)
                {
                    if (instance == null)
                        instance = new TimeService();

                    return instance;
                }
            }
        }

        public bool AddTime (DateTime entry, string narr, int hours, int pId, int eId)
        {
            times.Add(new Time(entry, narr, hours, pId, eId));
            return true;
        }

        public List<Time> Search(string query)
        {
            List<Time> ContainTimes = new List<Time>();
            foreach (Project p in ProjectService.Current.Search(query))
                if (times.Exists(c => c.ProjectId == p.Id))
                    ContainTimes.AddRange(times.Where(c => c.ProjectId == p.Id).ToList());

            foreach (Employee e in EmployeeService.Current.Search(query))
                if (times.Exists(c => c.EmployeeId == e.Id))
                    ContainTimes.AddRange(times.Where(c => c.EmployeeId == e.Id).ToList());

            return ContainTimes;
        }
    }
}
