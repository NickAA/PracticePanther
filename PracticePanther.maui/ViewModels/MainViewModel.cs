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
    public class MainViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Client> Clients
        {
            get 
            { 
                if (string.IsNullOrEmpty(Query))
                    return new ObservableCollection<Client>(ClientService.Current.Clients);

                return new ObservableCollection<Client>(ClientService.Current.Search(Query));
            }
        }
        public string Query { get; set; }

        public void Search ()
        {
            NotifyPropertyChanged("Clients");
        }

        public Client SelectedClient { get; set; }



        public void Delete()
        {
            if (SelectedClient == null)
                return;

            ClientService.Current.RemoveClient(SelectedClient);
            NotifyPropertyChanged("Clients");
        }



        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
