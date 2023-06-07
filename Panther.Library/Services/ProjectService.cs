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

        public static ProjectService Current
        { get
            {
                lock (_lock)
                {
                    if (Instance == null)
                        Instance = new ProjectService();

                    return Instance;
                }
            }
        }

        public bool AddProject(string Name)
        {
            if (Name != string.Empty)
            {
                projects.Add(new Project());
                projects.Last().Name = Name;
                return true;
            }
            return false;
        }

        public bool Exists(int Id)
        { return projects.Exists(c => c.Id == Id); }

        public Project LastProject()
        { return projects.Last(); }

        public int Count()
        { return projects.Count(); }

        public void PrintProjects()
        { projects.ForEach(Console.WriteLine); }



        private ProjectService() { }

    }
}
