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
    public class BillViewModel : INotifyPropertyChanged
    {
        public BillViewModel(bool NewBills = false)
        {
            if (NewBills)
                NotifyPropertyChanged(nameof(Bills));

            SelectedDate = DateTime.Now;
            AvaliableProject = ProjectService.Current.projects;
        }

        public BillViewModel(int BillId)
        {
            AvaliableProject = ProjectService.Current.projects;
            SelectedBill = BillService.Current.Find(BillId);
            SelectedProject = SelectedBill.ProjectAssociated;
            AmountInputted = SelectedBill.AmountOwedRounded;
            SelectedDate = SelectedBill.DueDate;
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

            NotifyPropertyChanged(nameof(Bills));
        }

        public void Add()
        {
            if (SelectedProject != null && SelectedDate != null)
            {
                if (BillService.Current.addBill(SelectedProject, SelectedDate.Value))
                    AddedBill = $"{SelectedProject} has been billed ${BillService.Current.bills.Last().AmountOwedRounded} due on {SelectedDate.Value.ToString("MM/dd/yyyy")}";

                NotifyPropertyChanged(nameof(AddedBill));
                SelectedProject = null;
                SelectedDate = DateTime.Now;
                NotifyPropertyChanged(nameof(SelectedProject));
                NotifyPropertyChanged(nameof(SelectedDate));
            }
        }

        public void Save()
        {
            if (SelectedProject != null && SelectedDate != null && ActualAmount != 0 && ActualAmount != null)
            {
                BillService.Current.saveBill(SelectedProject, SelectedDate.Value, AmountInputted.Value, SelectedBill);

                AddedBill = $"{SelectedProject} is now billed ${ActualAmount} due on {SelectedDate.Value.ToString("MM/dd/yyyy")}";
                NotifyPropertyChanged(nameof(AddedBill));
            }
        }

        public Bill SelectedBill { get; set; }
        public Project SelectedProject { get; set; }
        public List<Project> AvaliableProject { get; set; }
        private double? ActualAmount { get; set; }
        public double? AmountInputted 
        { 
            get { return ActualAmount == null ? null : ActualAmount.Value; } 
            set {  
                if (value != null)
                    ActualAmount = double.Round((double)value, 2);
            }
        }
        public DateTime? SelectedDate { get; set; }



        public string AddedBill { set; get; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
