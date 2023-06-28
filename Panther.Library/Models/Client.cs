using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticePanther.Models
{
    public class Client
    {

        public Client ()
        {
            OpenDate = DateTime.Today;
            IsActive = true;
            Id = ++ClientsCreated;
            Project = new List<Project> ();
        }

        public int Id { get; set; }
        static private int ClientsCreated = 0;

        public List<Project>? Project { get; set; }

        public DateTime OpenDate { get; set; }
        public string OpeningDate { get { return OpenDate.ToString("MM/dd/yyyy"); } }
        public DateTime? CloseDate { get; set; }

        public bool IsActive { get; set; }

        public string Name { get; set; }
        public string Notes { get; set; }

        public string ProjectName
        { get { return Project?.Count == null || Project?.Count == 0 ? "Not Assigned" : "Contains Projects"; } }
        public string ActiveDisplay
        { get { return IsActive ? "Active" : "Inactive"; } }

        public string IdDisplay
        { get { return Id.ToString(); } }

        public override string ToString()
        { return $"{Name}"; }


    }
}
