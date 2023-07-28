using Panther.Library.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticePanther.Models
{
    public class Project
    {
        public Project ()
        {
            Name = string.Empty;
            Notes = string.Empty;
            ClientIds = new List<int>();
            Bills = new List<Bill>();
        }

        public Project (string InName)
        {
            Name = InName;
            OpenDate = DateTime.Today;
            IsActive = true;
            Id = ++ProjectsCreated;
            ClientIds = new List<int>();
            Bills = new List<Bill>();

            CloseDate = DateTime.Today;
            Notes = string.Empty;
        }

        public int Id { get; set; }
        public string IdDisplay
        { get { return Id.ToString(); } }

        static private int ProjectsCreated = 0;

        // Associated Clients
        public List<int> ClientIds { get; set; }
        public string ClientName
        { get { return ClientIds?.Count == 0 ? "Not Assigned" : "Contains Clients"; } }

        public DateTime OpenDate { get; set; }
        public string OpeningDate
        { get { return OpenDate.ToString("MM/dd/yyyy"); } }
        public DateTime CloseDate { get; set; }

        public bool IsActive { get; set; }
        public string ActiveDisplay
        { get { return IsActive ? "Active" : "Inactive"; } }

        public string Name { get; set; }
        public string Notes { get; set; }

        public override string ToString()
        { return $"{Name}"; }

        // Bills for project
        public List<Bill> Bills { get; set; }
    }
}
