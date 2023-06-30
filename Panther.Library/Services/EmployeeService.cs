using Panther.Library.Models;
using PracticePanther.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panther.Library.Services
{
    public class EmployeeService
    {
        private EmployeeService() 
        {
            employees = new List<Employee>();
            AddEmployee("Penelope Sosa", 2.99);
        }

        private List<Employee> employees;
        public List<Employee> Employees
        { get { return employees; } }

        private static EmployeeService? instance;
        private static object _lock = new object();

        public static EmployeeService Current
        {
            get
            {
                lock (_lock)
                {
                    if (instance == null)
                        instance = new EmployeeService();
                }

                return instance;
            }
        }


        public bool AddEmployee(string Name, double? Rate)
        {
            if (Name != string.Empty)
            {
                employees.Add(new Employee());
                employees.Last().Name = Name;
                employees.Last().Rate = Rate;
                return true;
            }

            return false;
        }

        public Employee? FindEmployee(int ID)
        { return employees.FirstOrDefault(c => c.Id == ID); }

        public void RemoveEmployee (Employee employee)
        { employees.Remove(employee); }


        public List<Employee> Search(string query)
        {
            return employees.Where(c => c.Name.ToUpper().Contains(query.ToUpper())).ToList();
        }

    }
}
