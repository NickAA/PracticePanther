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
    public class BillViewModel : INotifyPropertyChanged
    {
        public BillViewModel(bool NewBills = false)
        {
            if (NewBills)
                NotifyPropertyChanged(nameof(Bills));
        }

        public ObservableCollection<Bill> Bills
        { get
            {
                if (string.IsNullOrEmpty(Query))
                    return new ObservableCollection<Bill>(BillService.Current.bills);

                return new ObservableCollection<Bill>(BillService.Current.Search(Query));
            }
        }
        public string Query { get; set; }

        public void Search()
        { NotifyPropertyChanged(nameof(Bills)); }

        public void Delete()
        {
            if (SelectedBill != null)
                BillService.Current.removeBill(SelectedBill);
        }
        public Bill SelectedBill { get; set; }






        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
