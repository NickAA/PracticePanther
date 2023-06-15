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
        }

        public int Id { get; set; }
        static private int ClientsCreated = 0;

        public Project? Project { get; set; }

        public DateTime OpenDate { get; set; }
        public DateTime CloseDate { get; set; }

        public bool IsActive { get; set; }

        public string Name { get; set; }
        public string Notes { get; set; }

        public override string ToString()
        {
            string Active;
            if (IsActive)
                Active = "Active";
            else
                Active = "Inactive";

            string strFormat = String.Format("{0,-5} {1, -18} {2}", Id, Name, Active);
            return $"{strFormat}";
        }


    }
}
