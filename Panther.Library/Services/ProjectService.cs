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

        public Project FindProject(int Id)
        { return projects.FirstOrDefault(c => c.Id == Id); }

        public Project FindProject(string Name)
        { return projects.FirstOrDefault(c => c.Name == Name); }

        public int AmountofProjects()
        { return projects.Count(); }

        public void RemoveProject(Project DeleteProject)
        { projects.Remove(DeleteProject); }

        public void LinkClient(Project project, int Id)
        {
            var myClientService = ClientService.Current;
            var Client = myClientService.FindClient(Id);
            project.Clients.Add(Client);
        }

        public void RemoveClient(Client client)
        {
            var ProjectwithClient = projects.FirstOrDefault(c => c.Clients.FirstOrDefault(s => s.Id == client.Id) == client);
            ProjectwithClient.Clients.Remove(client);
        }

        private ProjectService() 
        {
            AddProject("Azzi's Case");
            AddProject("John's Cases");
        }

        public List<Project> Search(string query)
        { return projects.Where(c => c.Name.ToUpper().Contains(query.ToUpper())).ToList(); }

    }
}
