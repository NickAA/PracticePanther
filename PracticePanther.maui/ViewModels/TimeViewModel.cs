using Panther.Library.Models;
using Panther.Library.Services;
using PracticePanther.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PracticePanther.maui.ViewModels
{
    class TimeViewModel : INotifyPropertyChanged
    {
        public TimeViewModel(bool NewTime = false)
        {
            if (NewTime)
            {
                NotifyPropertyChanged(nameof(Times));
            }
            AvaliableEmployees = EmployeeService.Current.employees;
            AvaliableProjects = ProjectService.Current.projects;
            Time = DateTime.Today;
        }
        public List<Employee> AvaliableEmployees { get; set; }
        public List<Project> AvaliableProjects { get; set; }
        public TimeViewModel(int TimeID)
        {
            AvaliableEmployees = EmployeeService.Current.employees;
            AvaliableProjects = ProjectService.Current.projects;
            SelectedTime = TimeService.Current.FindTime(TimeID);
            AddedHours = SelectedTime.Hours;
            Time = SelectedTime.DateEntry;
            SelectedProject = SelectedTime.project;
            SelectedEmployee = SelectedTime.employee;
            Narrative = SelectedTime.Narrative;
        }

        public ObservableCollection<Time> Times
        {
            get
            {
                if (string.IsNullOrEmpty(Query))
                    return new ObservableCollection<Time>(TimeService.Current.times);

                return new ObservableCollection<Time>(TimeService.Current.Search(Query));
            }
        }
        
        // Search
        public string Query { get; set; }

        public void Search()
        { NotifyPropertyChanged(nameof(Times)); }

        public void Delete()
        {
            if (SelectedTime == null)
                return;

            TimeService.Current.RemoveTime(SelectedTime);
            NotifyPropertyChanged(nameof(Times));
        }

        // Used to select time and grab time being updated
        public Time SelectedTime { get; set; }

        public void Add()
        {
            if (SelectedEmployee == null || SelectedProject == null || AddedHours == null)
                return;

            TimeService.Current.AddTime(Time, AddedHours.Value, SelectedProject, SelectedEmployee);

            AddedTime = $"{SelectedEmployee} clocked {SelectedProject} for {AddedHours} hours";
            Time = DateTime.Today;
            AddedHours = null;
            SelectedProject = null;
            SelectedEmployee = null;
            NotifyPropertyChanged(nameof(AddedTime));
            NotifyPropertyChanged(nameof(Time));
            NotifyPropertyChanged(nameof(AddedHours));
            NotifyPropertyChanged(nameof(SelectedEmployee));
            NotifyPropertyChanged(nameof(SelectedProject));
        }
        // Prompt shows what employee cloacked what project for how much hours
        public string AddedTime { get; set; }
        public DateTime Time { get; set; }
        public int? AddedHours { get; set; }
        public Employee SelectedEmployee { get; set; }
        public Project SelectedProject { get; set; }

        public void Save()
        {
            SelectedTime.Hours = AddedHours.Value;
            SelectedTime.DateEntry = Time;
            SelectedTime.project = SelectedProject;
            SelectedTime.employee = SelectedEmployee;
            SelectedTime.Narrative = Narrative;
        }
        public string Narrative { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
