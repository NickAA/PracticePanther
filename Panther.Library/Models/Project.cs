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
            ClientId = new List<int>();
            Clients = new List<Client>();
        }

        public int Id { get; set; }
        static private int ProjectsCreated = 0;

        public List<int> ClientId { get; set; }          // Used to Link Clients to a project

        public List<Client> Clients { get; set; }

        public DateTime OpenDate { get; set; }
        public DateTime CloseDate { get; set; }

        public bool IsActive { get; set; }

        public string Name { get; set; }
        public string Notes { get; set; }

        public override string ToString()
        {
            string Active = "Inactive", Client = "None";

            if (IsActive)
                Active = "Active";

            if (Clients.Count != 0)
                for (int i = 0; i < Clients.Count; i++)
                {
                    if (i == 0)
                        Client = Clients[0].Id.ToString();
                    else
                        Client += $" {Clients[i].Id.ToString()}";
                }


            string strFormat = String.Format("{0,-5} {1, -18} {2, -18} {3}", Id, Name, Client, Active);
            return $"{strFormat}";
        }
    }
}
