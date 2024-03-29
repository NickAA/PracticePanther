﻿using Panther.Library.DTO;

namespace PracticePanther.Models
{
    public class Client
    {
        // Assigns needed values
        public Client ()
        {
            Name = string.Empty;
            Notes = string.Empty;
            Project = new List<Project> ();
        }

        public Client (ClientDTO c)
        {
            Id = c.Id;
            Name = c.Name;
            Notes = c.Notes;
            OpenDate = c.OpenDate;
            CloseDate = c.CloseDate;
            IsActive = c.IsActive;
            Project = new List<Project>(c.Project);
        }

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
