using Panther.Library.Models;
using Panther.Library.Services;
using PracticePanther.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PracticePanther.maui.ViewModels
{
    class EmployeeViewModel : INotifyPropertyChanged
    {
        public EmployeeViewModel(bool NewEmployee = false)
        {
            if (NewEmployee)
                NotifyPropertyChanged("Employees");
        }

        public EmployeeViewModel(int InputtedEmployeeId)
        {
            SelectedEmployee = EmployeeService.Current.FindEmployee(InputtedEmployeeId);
            NewEmployee = SelectedEmployee.Name;
            EmployeeRate = SelectedEmployee.Rate;
        }

        public void Save ()
        {
            if (SelectedEmployee.Name == string.Empty || SelectedEmployee.Rate == null)
                return;

            SelectedEmployee.Name = NewEmployee;
            SelectedEmployee.Rate = EmployeeRate;
        }

        public ObservableCollection<Employee> Employees
        {
            get
            {
                if (string.IsNullOrEmpty(Query))
                    return new ObservableCollection<Employee>(EmployeeService.Current.Employees);

                return new ObservableCollection<Employee>(EmployeeService.Current.Search(Query));
            }
        }
        public string Query { get; set; }

        public void Delete()
        {
            if (SelectedEmployee == null)
                return;

            EmployeeService.Current.RemoveEmployee(SelectedEmployee);
            // Works now
            NotifyPropertyChanged("Employees");
        }
        public Employee SelectedEmployee { get; set; }

        public void Add ()
        {
            if (NewEmployee == string.Empty || EmployeeRate == null)
                return;

            EmployeeService.Current.AddEmployee(NewEmployee, EmployeeRate);
            AddedEmployee = $"{NewEmployee} has been added";
            NotifyPropertyChanged(nameof(AddedEmployee));

            NewEmployee = string.Empty;
            EmployeeRate = null;
            NotifyPropertyChanged(nameof(NewEmployee));
            NotifyPropertyChanged(nameof(EmployeeRate));
        }
        public string NewEmployee { get; set; }
        public double? EmployeeRate { get; set; }
        public string AddedEmployee { get; set; }

        public void Search()
        {
            NotifyPropertyChanged("Employees");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
