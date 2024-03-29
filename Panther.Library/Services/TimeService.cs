﻿using Panther.Library.Models;
using PracticePanther.Models;

namespace Panther.Library.Services
{
    public class TimeService
    {
        private TimeService()
        {
            times = new List<Time>();
            AddTime(DateTime.Today, 51, ProjectService.Current.projects.First(), EmployeeService.Current.employees.Last() );
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

        public bool AddTime (DateTime entry, int hours, Project pId, Employee eId)
        {
            times.Add(new Time(entry, hours, pId, eId));
            return true;
        }

        // Returns back list of time entries with project name and employee name
        public List<Time> Search(string query)
        {
            List<Time> ContainTimes = new List<Time>();
            foreach (Project p in ProjectService.Current.Search(query))
                if (times.Exists(c => c.project == p))
                    ContainTimes.AddRange(times.Where(c => c.project == p).ToList());

            foreach (Employee e in EmployeeService.Current.Search(query))
                if (times.Exists(c => c.employee == e))
                    ContainTimes.AddRange(times.Where(c => c.employee == e).ToList());

            return ContainTimes.Distinct().ToList();
        }

        public void RemoveTime(Time DeleteTime)
        { times.Remove(DeleteTime); }

        public Time FindTime(int ID)
        { return times.FirstOrDefault(c => c.Id == ID); }

    }
}
