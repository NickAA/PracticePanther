using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticePanther.Models
{
    public class Client
    {
        // Assigns needed values
        public Client (string InName)
        {
            Name = InName;
            OpenDate = DateTime.Today;
            IsActive = true;
            Id = ++ClientsCreated;
            Project = new List<Project> ();

            Notes = string.Empty;
            CloseDate = DateTime.Today;
        }

        public int Id { get; set; }
        public string IdDisplay
        { get { return Id.ToString(); } }
        static private int ClientsCreated = 0;

        public List<Project> Project { get; set; }

        public DateTime OpenDate { get; set; }
        public string OpeningDate { get { return OpenDate.ToString("MM/dd/yyyy"); } }
        public DateTime CloseDate { get; set; }

        public bool IsActive { get; set; }
        public string ActiveDisplay
        { get { return IsActive ? "Active" : "Inactive"; } }

        public string Name { get; set; }
        public string Notes { get; set; }

        // When Client contains and project
        public string ProjectName
        { get { return Project?.Count == null || Project?.Count == 0 ? "Not Assigned" : "Contains Projects"; } }

        public override string ToString()
        { return $"{Name}"; }


    }
}
