using Panther.Library.Models;
using Panther.Library.Services;
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
                //NotifyProp
            }
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

        public string Query { get; set; }



        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
