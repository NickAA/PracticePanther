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

        private BillService() 
        {
            addBill(ProjectService.Current.FindProject(1), DateTime.Today, 99.99);
        }

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


        public void addBill(Project p, DateTime d, double a)
        {
            bills.Add(new Bill(p, d, a));
            // Assigned recently created bill to project
            ProjectService.Current.FindProject(p.Id).Bills.Add(bills.Last());
        }

        public void removeBill(Bill b)
        {
            ProjectService.Current.FindProject(b.ProjectAssociated.Id).Bills.Remove(b);
            bills.Remove(b);
        }

        public List<Bill> Search (string Query)
        { return bills.Where(b => b.ProjectAssociated.Name.ToUpper().Contains(Query.ToUpper())).ToList(); }

    }
}
