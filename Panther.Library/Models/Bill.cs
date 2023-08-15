using PracticePanther.Models;

namespace Panther.Library.Models
{
    public class Bill
    {
        // Assigns needed values
        public Bill(Project p, DateTime d, double ao)
        {
            ProjectAssociated = p;
            DueDate = d;
            AmountOwed = ao;
            ID = ++BillsCreated;
        }

        private int BillsCreated = 0;
        public int ID { get; set; }
        public Project ProjectAssociated { get; set; }
        public DateTime DueDate { get; set; }
        public string DueDateString { get {  return DueDate.ToString("MM/dd/yyyy"); } }
        public double AmountOwed { get; set; }
        public double AmountOwedRounded { get { return double.Round(AmountOwed, 2); } }
        public override string ToString ()
        { return $"{ProjectAssociated} owes ${AmountOwedRounded} due on {DueDateString}"; }
    }
}
