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
        public Project (string InName)
        {
            Name = InName;
            OpenDate = DateTime.Today;
            IsActive = true;
            Id = ++ProjectsCreated;
            Clients = new List<Client>();
            Bills = new List<Bill>();
        }

        public int Id { get; set; }
        public string IdDisplay
        { get { return Id.ToString(); } }

        static private int ProjectsCreated = 0;

        // Associated Clients
        public List<Client> Clients { get; set; }
        public string ClientName
        { get { return Clients?.Count == 0 ? "Not Assigned" : "Contains Clients"; } }

        public DateTime OpenDate { get; set; }
        public string OpeningDate
        { get { return OpenDate.ToString("MM/dd/yyyy"); } }
        public DateTime? CloseDate { get; set; }

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
