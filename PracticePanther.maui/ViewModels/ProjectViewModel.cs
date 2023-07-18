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

            AssociatedClients = new ObservableCollection<Client>();

            UpdateTitle = $"Updating {SelectedProject.Name}";
            Notes = SelectedProject.Notes;

            ProjectsOpenDate = SelectedProject.OpenDate;
            ProjectsCloseDate = SelectedProject.CloseDate;
            

        }

        // Shows clients
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

        // Whether to gray out close date picker or not
        public bool IsEnabled { get; set; }
        public DateTime ProjectsOpenDate { get; set; }
        public string ProjectsOpenDateFormat { get { return ProjectsOpenDate.ToString("MM/dd/yyyy"); } }
        public DateTime? ProjectsCloseDate { get; set; }
        public string ProjectsCloseDateFormat { get { return SelectedProject.IsActive ? "N.A." : ProjectsCloseDate?.ToString("MM/dd/yyyy"); } }
        
        // Clients associated with Projects
        public ObservableCollection<Client> AssociatedClients { get; set; }
        // Shows Project ID
        public int AssociatedID { get; set; }
        // Prompt that shows what project is currently being updated
        public string UpdateTitle { get; set; }
        // Entry name that's currently being updated
        public string ProjectToUpdateName { get; set; }
        public string Notes { get; set; }

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

            foreach(Client c in SelectedProject.Clients)
                c.Project.Remove(SelectedProject);
            SelectedProject.Clients = AssociatedClients.ToList();
            foreach (Client c in SelectedProject.Clients)
                c.Project.Add(SelectedProject);


            SelectedProject.Notes = Notes;
        }

        public string ClientNameDetail
        {
            get
            {
                if (SelectedProject.Clients == null || SelectedProject.Clients.Count == 0)
                    return "No clients assigned.";

                string NameOfProjects = string.Empty;

                foreach (Client clients in SelectedProject.Clients.Distinct())
                {
                    NameOfProjects += clients.Name + ", ";
                }

                return NameOfProjects.Remove(NameOfProjects.Count() - 2);
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

        // Search
        public string Query { get; set; }

        public void Delete()
        {
            if (SelectedProject == null)
                return;

            foreach (Client c in SelectedProject.Clients)
                c.Project.Remove(SelectedProject);

            ProjectService.Current.RemoveProject(SelectedProject);
            // Works now
            NotifyPropertyChanged("Projects");
        }
        // Used for selected Project, updating project, and viewing a project
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
        // Recieves string name
        public string NewProject { get; set; }
        // Prompt: what project has been added
        public string AddedProject { get; set; }

        public void Search()
        { NotifyPropertyChanged("Projects"); }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }


}
