﻿using Panther.Library.Models;

namespace Panther.Library.Services
{
    public class EmployeeService
    {
        private EmployeeService() 
        {
            employees = new List<Employee>();
            AddEmployee("Penelope Sosa", 2.99);
        }

        public List<Employee> employees;
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
            // Makes sure Name and Rate aren't empty
            if (!string.IsNullOrEmpty(Name) && Rate != null)
            {
                employees.Add(new Employee(Name, Rate.Value));
                return true;
            }
            return false;
        }

        // Finds Employee with ID
        public Employee? FindEmployee(int ID)
        { return employees.FirstOrDefault(c => c.Id == ID); }

        public void RemoveEmployee (Employee employee)
        { employees.Remove(employee); }

        // Returns searched employee list
        public List<Employee> Search(string query)
        { return employees.Where(c => c.Name.ToUpper().Contains(query.ToUpper())).ToList(); }

    }
}
