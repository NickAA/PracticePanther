﻿using Panther.Library.Services;
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
    public class ClientViewModel : INotifyPropertyChanged
    {

        public ClientViewModel(bool NewClients = false) 
        {
            if (NewClients)
                NotifyPropertyChanged("Clients");
        }

        public ClientViewModel(int ClientsId)
        {
            SelectedClient = ClientService.Current.FindClient(ClientsId);
            AvaliableProjects = ProjectService.Current.projects;

            ClientToUpdateName = SelectedClient.Name;

            if (SelectedClient.IsActive == true)
                Activity = "Active";
            else
                Activity = "Inactive";

            AssociatedProject = SelectedClient.Project?.Name;

            UpdateTitle = $"Updating {SelectedClient.Name}";
            Notes = SelectedClient.Notes;


            NotifyPropertyChanged("Options");
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

        public string AssociatedProject { get; set; }

        public List<Project> AvaliableProjects { get; set; }

        public string Activity { get; set; }

        public string Query { get; set; }

        public string Notes { get; set; }

        public string NewClient { get; set; }

        public string AddedClient { get; set; }

        public string UpdateTitle { get; set; }

        public string ClientToUpdateName { get; set; }

        public Client SelectedClient { get; set; }

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

            ClientService.Current.RemoveClient(SelectedClient);
            // Notify Client not working find out tomorrow
            NotifyPropertyChanged("Clients");
        }

        public void Save()
        {
            SelectedClient.Name = ClientToUpdateName; 

            if (Activity == "Active")
                SelectedClient.IsActive = true;
            else
                SelectedClient.IsActive = false;

            SelectedClient.Project = ProjectService.Current.FindProject(AssociatedProject);

            SelectedClient.Notes = Notes;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
