using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticePanther.Models
{
    public class Project
    {
        public Project ()
        {
            OpenDate = DateTime.Today;
            IsActive = true;
            Id = ++ProjectsCreated;
            ClientId = -1;
        }

        public int Id { get; set; }
        static private int ProjectsCreated = 0;
        public int ClientId { get; set; }          // Used to Link Clients to a project

        public DateTime OpenDate { get; set; }
        public DateTime CloseDate { get; set; }

        public bool IsActive { get; set; }

        public string Name { get; set; }
        public string Notes { get; set; }

        public override string ToString()
        {
            string Active, Client;

            if (IsActive)
                Active = "Active";
            else
                Active = "Inactive";

            if (ClientId == -1)
                Client = "None";
            else
                Client = ClientId.ToString();

            string strFormat = String.Format("{0,-5} {1, -18} {2, -18} {3}", Id, Name, Client, Active);
            return $"{strFormat}";
        }
    }
}
