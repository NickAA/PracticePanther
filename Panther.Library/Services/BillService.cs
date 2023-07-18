using Panther.Library.Models;
using PracticePanther.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panther.Library.Services
{
    public class BillService
    {
        public List<Bill> bills = new List<Bill>();
        private static BillService? Instance;
        private static object _lock = new object();

        public static BillService Current
        {
            get
            {
                lock (_lock)
                {
                    if (Instance == null)
                        Instance = new BillService();

                    return Instance;
                }
            }
        }

        // Finds bill w/ id
        public Bill Find(int id)
        { return bills.Find(b => b.ID == id); }

        public bool addBill(Project p, DateTime d)
        {
            double a = 0;
            foreach (Time time in TimeService.Current.times)
                if (p == time.project)
                    a += time.employee.Rate * time.Hours;

            if (a > 0)
            {
                bills.Add(new Bill(p, d, a));
                // Assigned recently created bill to project
                ProjectService.Current.FindProject(p.Id).Bills.Add(bills.Last());
                return true;
            }
            return false;
        }

        public void saveBill(Project p, DateTime d, double a, Bill BillChanging)
        {
            // Removes all project associated w bill
            ProjectService.Current.FindProject(BillChanging.ProjectAssociated.Id).Bills.Remove(BillChanging);

            BillChanging.AmountOwed = a;
            BillChanging.ProjectAssociated = p;
            BillChanging.DueDate = d;

            // Adds bills to projects
            ProjectService.Current.FindProject(BillChanging.ProjectAssociated.Id).Bills.Add(BillChanging);
        }

        public void removeBill(Bill b)
        {
            ProjectService.Current.FindProject(b.ProjectAssociated.Id).Bills.Remove(b);
            bills.Remove(b);
        }

        // returns search bills w/ project name
        public List<Bill> Search (string Query)
        { return bills.Where(b => b.ProjectAssociated.Name.ToUpper().Contains(Query.ToUpper())).ToList(); }

    }
}
