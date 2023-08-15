using Panther.Library.Models;
using Panther.Library.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

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
            if (string.IsNullOrEmpty(NewEmployee) || EmployeeRate == null)
                return;

            SelectedEmployee.Name = NewEmployee;
            SelectedEmployee.Rate = EmployeeRate.Value;
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
            if (NewEmployee == string.Empty || NewEmployee == null || EmployeeRate == null)
                return;

            EmployeeService.Current.AddEmployee(NewEmployee, EmployeeRate);
            AddedEmployee = $"{NewEmployee} has been added";
            NotifyPropertyChanged(nameof(AddedEmployee));

            NewEmployee = string.Empty;
            EmployeeRate = null;
            NotifyPropertyChanged(nameof(NewEmployee));
            NotifyPropertyChanged(nameof(EmployeeRate));
        }
        // Used for showing the name of new employee or updating name of existing employee
        public string NewEmployee { get; set; }
        // Used for getting employee rate
        public double? EmployeeRate { get; set; }
        // Shows prompt of added employee
        public string AddedEmployee { get; set; }

        public void Search()
        { NotifyPropertyChanged("Employees"); }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
