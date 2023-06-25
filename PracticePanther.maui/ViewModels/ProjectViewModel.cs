using Panther.Library.Services;
using PracticePanther.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PracticePanther.maui.ViewModels
{
    internal class ProjectViewModel : INotifyPropertyChanged
    {
        public ProjectViewModel(bool NewProjects = false)
        {
            if (NewProjects)
                NotifyPropertyChanged("Projects");
        }

        public ProjectViewModel(int InputtedProjectId)
        {
            AssociatedID = InputtedProjectId;
            SelectedProject = ProjectService.Current.FindProject(InputtedProjectId);
            AvaliableClients = ClientService.Current.Clients;

            ProjectToUpdateName = SelectedProject.Name;

            if (SelectedProject.IsActive == true)
                Activity = "Active";
            else
                Activity = "InActive";

            AssociatedClients = SelectedProject.Clients;

            UpdateTitle = $"Updating {SelectedProject.Name}";
            Notes = SelectedProject.Notes;

            ProjectsOpenDate = SelectedProject.OpenDate;
            ProjectsCloseDate = SelectedProject.CloseDate;

        }
        public List<Client> AvaliableClients { get; set; }
        private string activity;
        public string Activity
        {
            get { return activity; }
            set
            {
                activity = value;

                if (Activity == "Active")
                    IsEnabled = false;
                else
                    IsEnabled = true;

                NotifyPropertyChanged("IsEnabled");
            }
        }

        public bool IsEnabled { get; set; }
        public DateTime ProjectsOpenDate { get; set; }
        public string ProjectsOpenDateFormat { get { return ProjectsOpenDate.ToString("MM/dd/yyyy"); } }
        public DateTime? ProjectsCloseDate { get; set; }
        public string ProjectsCloseDateFormat { get { return SelectedProject.IsActive ? "N.A." : ProjectsCloseDate?.ToString("MM/dd/yyyy"); } }

        public List<Client> AssociatedClients { get; set; }
        public int AssociatedID { get; set; }
        public string UpdateTitle { get; set; }
        public string Notes { get; set; }
        public string ProjectToUpdateName { get; set; }

        public void Save ()
        {
            SelectedProject.Name = ProjectToUpdateName;
            UpdateTitle = $"Updating {SelectedProject.Name}";
            NotifyPropertyChanged("UpdateTitle");

            if (Activity == "Active")
                SelectedProject.IsActive = true;
            else
                SelectedProject.IsActive = false;

            SelectedProject.OpenDate = ProjectsOpenDate;
            if (IsEnabled)
                SelectedProject.CloseDate = ProjectsCloseDate;

            SelectedProject.Clients = AssociatedClients;

            SelectedProject.Notes = Notes;
        }

        public string ClientNameDetail
        {
            get
            {
                if (AssociatedClients == null)
                    return "Not Assigned";

                string NameOfProjects = string.Empty;

                foreach (Client clients in AssociatedClients)
                {
                    NameOfProjects += clients.Name + " ";
                }

                return NameOfProjects;
            }
        }

        public ObservableCollection<Project> Projects
        {
            get
            {
                if (string.IsNullOrEmpty(Query))
                    return new ObservableCollection<Project>(ProjectService.Current.projects);

                return new ObservableCollection<Project>(ProjectService.Current.Search(Query));
            }
        }

        public string Query { get; set; }

        public void Delete()
        {
            if (SelectedProject == null)
                return;

            ProjectService.Current.RemoveProject(SelectedProject);
            // Works now
            NotifyPropertyChanged("Projects");
        }
        public Project SelectedProject { get; set; }

        public void Add()
        {
            if (string.IsNullOrEmpty(NewProject))
                return;

            ProjectService.Current.AddProject(NewProject);
            AddedProject = $"{NewProject} has been added";
            NotifyPropertyChanged(nameof(AddedProject));

            NewProject = string.Empty;
            NotifyPropertyChanged(nameof(NewProject));
        }
        public string NewProject { get; set; }
        public string AddedProject { get; set; }

        public void Search()
        {
            NotifyPropertyChanged("Projects");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }


}
