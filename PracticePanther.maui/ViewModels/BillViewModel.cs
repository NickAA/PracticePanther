﻿using Panther.Library.Models;
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

            AvaliableProject = ProjectService.Current.projects;
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
            if (SelectedProject != null && SelectedDate != null && ActualAmount != null)
            {
                BillService.Current.addBill(SelectedProject, SelectedDate.Value, ActualAmount.Value);
                AddedBill = $"{SelectedProject} has been billed ${ActualAmount} due on {SelectedDate.Value.ToString("MM/dd/yyyy")}";
                NotifyPropertyChanged(nameof(AddedBill));
            }
        }

        public Bill SelectedBill { get; set; }
        public Project SelectedProject { get; set; }
        public List<Project> AvaliableProject { get; set; }
        private double? ActualAmount { get; set; }
        public double AmountInputted { set {  ActualAmount = double.Round(value, 2); } }
        public DateTime? SelectedDate { get; set; }



        public string AddedBill { set; get; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
