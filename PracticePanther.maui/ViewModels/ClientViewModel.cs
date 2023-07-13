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
using System.Windows.Input;

namespace PracticePanther.maui.ViewModels
{
    public class ClientViewModel : INotifyPropertyChanged
    {

        public ClientViewModel(bool NewClients = false) 
        {
            if (NewClients)
                NotifyPropertyChanged("Clients");
        }

        public ClientViewModel(int ClientsId)
        {
            AssociatedID = ClientsId;
            SelectedClient = ClientService.Current.FindClient(ClientsId);
            AvaliableProjects = new ObservableCollection<object>(ProjectService.Current.projects);

            ClientToUpdateName = SelectedClient.Name;

            if (SelectedClient.IsActive == true)
                Activity = "Active";
            else
                Activity = "InActive";

            SelectedProjects = new ObservableCollection<Project>();
            //SelectedProjects = new ObservableCollection<Project>(SelectedClient.Project);
            AssociatedProjects = new ObservableCollection<Project>(SelectedClient.Project);
            //NotifyPropertyChanged(nameof(SelectedProjects));
            //NotifyPropertyChanged(nameof(ShowedProjects));

            UpdateTitle = $"Updating {SelectedClient.Name}";
            Notes = SelectedClient.Notes;

            ClientsOpenDate = SelectedClient.OpenDate;
            ClientsCloseDate = SelectedClient.CloseDate;

        }

        public string ProjectNameDetail { get
            {
                if (AssociatedProjects == null || AssociatedProjects.Count == 0)
                    return "No projects assigned.";

                string ProjectMSG = string.Empty;

                foreach (Project c in AssociatedProjects)
                    ProjectMSG += $"{c.Name}, ";

                return ProjectMSG.Remove(ProjectMSG.Count() - 2);
            } }

        public ObservableCollection<Project> AssociatedProjects { get; set; }


        private ObservableCollection<Project> selectedProjects;
        public ObservableCollection<Project> SelectedProjects
        { 
            get { return selectedProjects; }
            set
            {
                if (selectedProjects != value)
                {
                    selectedProjects = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public ObservableCollection<Client> Clients
        {
            get 
            {
                if (string.IsNullOrEmpty(Query))
                    return new ObservableCollection<Client>(ClientService.Current.Clients);

                return new ObservableCollection<Client>(ClientService.Current.Search(Query));
            }
        }



        public int AssociatedID { get; set; }

        public ObservableCollection<object> AvaliableProjects { get; set; }

        private string activity;
        public string Activity { get 
            {
                if (SelectedClient.Project == null || SelectedClient.Project.Count == 0)
                    CheckProjects = true;

                foreach (Project p in SelectedClient.Project)
                    if (p.IsActive == true)
                        CheckProjects = false;
                    else
                    {
                        CheckProjects = true;
                        break;
                    }

                NotifyPropertyChanged(nameof(CheckProjects));

                return activity; 
            } 
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

        public bool CheckProjects { get; set; }

        public bool IsEnabled { get; set; }

        public string Query { get; set; }

        public string Notes { get; set; }

        public string NewClient { get; set; }

        public string AddedClient { get; set; }

        public string UpdateTitle { get; set; }

        public string ClientToUpdateName { get; set; }

        public Client SelectedClient { get; set; }

        public DateTime ClientsOpenDate { get; set; }
        public string ClientsOpenDateFormat { get { return ClientsOpenDate.ToString("MM/dd/yyyy"); } }
        public DateTime? ClientsCloseDate { get; set; }
        public string ClientsCloseDateFormat { get { return SelectedClient.IsActive ? "N.A." : ClientsCloseDate?.ToString("MM/dd/yyyy"); } }


        public void Search ()
        {
            NotifyPropertyChanged("Clients");
        }

        public void Add()
        {
            if (string.IsNullOrEmpty(NewClient))
                return;

            ClientService.Current.AddClient(NewClient);
            AddedClient = $"{NewClient} has been added";
            NotifyPropertyChanged(nameof(AddedClient));

            NewClient = string.Empty;
            NotifyPropertyChanged(nameof(NewClient));
        }




        public void Delete()
        {
            if (SelectedClient == null)
                return;

            foreach (Project p in SelectedClient.Project)
                if (p.IsActive == true)
                    return;

            ClientService.Current.RemoveClient(SelectedClient);
            // Works now
            NotifyPropertyChanged("Clients");
        }

        public void Save()
        {
            SelectedClient.Name = ClientToUpdateName;
            UpdateTitle = $"Updating {SelectedClient.Name}";
            NotifyPropertyChanged("UpdateTitle");

            SelectedClient.OpenDate = ClientsOpenDate;
            if (IsEnabled)
                SelectedClient.CloseDate = ClientsCloseDate;

            if (SelectedProjects != null)
            {
                SelectedClient.Project = SelectedProjects.ToList();

                foreach (Project p in SelectedClient.Project)
                    p.Clients.Add(SelectedClient);
            }
            else
            {
                foreach (Project p in SelectedClient.Project)
                    p.Clients.Remove(SelectedClient);
                SelectedClient.Project.Clear();
            }

            if (Activity == "Active")
                SelectedClient.IsActive = true;
            else
                SelectedClient.IsActive = false;

            NotifyPropertyChanged(nameof(CheckProjects));

            SelectedClient.Notes = Notes;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
