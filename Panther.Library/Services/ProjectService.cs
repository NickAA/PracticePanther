﻿using PracticePanther.Models;

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
            if (!string.IsNullOrEmpty(Name))
            {
                projects.Add(new Project(Name));
                return true;
            }
            return false;
        }

        public bool Exists(int Id)
        { return projects.Exists(c => c.Id == Id); }

        public Project FindProject(int Id)
        { return projects.FirstOrDefault(c => c.Id == Id); }

        public void RemoveProject(Project DeleteProject)
        { projects.Remove(DeleteProject); }

        // Links Clients with project
        public void LinkClient(Project project, int Id)
        {
            var myClientService = ClientService.Current;
            var Client = myClientService.FindClient(Id);
            project.ClientIds.Add(Client.Id);
        }

        public void RemoveClient(Client client)
        {
            var ProjectwithClient = projects.FirstOrDefault(c => c.ClientIds.Contains(client.Id));
            ProjectwithClient.ClientIds.Remove(client.Id);
        }

        // Adds Projects
        private ProjectService() 
        {
            AddProject("Azzi's Case");
            AddProject("John's Cases");
        }

        public List<Project> Search(string query)
        { return projects.Where(c => c.Name.ToUpper().Contains(query.ToUpper())).ToList(); }


        // Functions below used for assignment 1
        public Project LastProject()
        { return projects.Last(); }

        public int Count()
        { return projects.Count(); }

        public void PrintProjects()
        { projects.ForEach(Console.WriteLine); }

        public Project FindProject(string Name)
        { return projects.FirstOrDefault(c => c.Name == Name); }

        public int AmountofProjects()
        { return projects.Count(); }


    }
}
