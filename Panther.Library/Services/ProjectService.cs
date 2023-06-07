using PracticePanther.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panther.Library.Services
{
    public class ProjectService
    {
        public List<Project> projects = new List<Project>();
        private static ProjectService? Instance;
        private static object _lock = new object();

        public static ProjectService Current { get
            {
                lock (_lock)
                {
                    if (Instance == null)
                        Instance = new ProjectService();
                
                    return Instance;
                }
            } }


        private ProjectService() { }

    }
}
