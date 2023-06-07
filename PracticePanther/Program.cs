using Panther.Library.Services;
using PracticePanther.Models;
using System;
using System.ComponentModel.Design;

namespace PracticePanther
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Welcome Message
            Console.WriteLine("Welcome to the practice management Console App!");
            Console.WriteLine("Below a Menu will show with options to choose from.\n");

            // List of clients and projects
            var myProjectService = ProjectService.Current;
            var myClientServices = ClientService.Current;

            string Selection;
            do
            {

                // Menu
                Console.WriteLine("Main Menu\n");
                Console.WriteLine("C. Client Menu\nP. Project Menu\nQ. Quit Program");

                Selection = Console.ReadLine() ?? string.Empty;
                while (!Selection.Equals("C", StringComparison.CurrentCultureIgnoreCase) && !Selection.Equals("P", StringComparison.CurrentCultureIgnoreCase) &&
                       !Selection.Equals("Q", StringComparison.CurrentCultureIgnoreCase))
                {
                    Console.WriteLine("Option does not exist please try again.\n\nMain Menu\n\nC. Client Menu\nP. Project Menu\nQ. Quit Program");
                    Selection = Console.ReadLine() ?? string.Empty;
                }

                if (Selection.Equals("C", StringComparison.CurrentCultureIgnoreCase))
                    ClientMenu();
                else if (Selection.Equals("P", StringComparison.CurrentCultureIgnoreCase))
                    ProjectMenu();



            } while (!Selection.Equals("Q", StringComparison.CurrentCultureIgnoreCase));

        }



        static void ClientMenu()
        {
            var myClientServices = ClientService.Current;

            string Selection;
            do
            {
                Console.Clear();
                // Menu
                Console.WriteLine("Client Menu\n");
                Console.WriteLine("C. Create a new Client");
                Console.WriteLine("S. Show Clients");
                Console.WriteLine("U. Update Client");
                Console.WriteLine("D. Delete Client");
                Console.WriteLine("Q. Return to Main menu");


                Selection = Console.ReadLine() ?? string.Empty;

                switch (Selection)
                {
                    case "C":
                    case "c":
                        // Creates new client
                        
                        Console.Write("Please enter name of Client: ");

                        while (!myClientServices.AddClient(Console.ReadLine() ?? string.Empty))
                            Console.Write("Please enter a valid name: ");

                        Console.WriteLine($"Succesfully entered {myClientServices.LastClient().Name} into the system.");
                        break;

                    case "S":
                    case "s":
                        // Prints out clients in directory
                        if (myClientServices.AmountofClients() == 0)
                        {
                            Console.WriteLine("\nNo storred clients ");
                        }
                        else
                        {
                            string strFormat = String.Format("{0,-5} {1, -18} {2}", "ID", "Client", "Activity");
                            Console.WriteLine($"\n{strFormat}");
                            myClientServices.PrintClients();
                        }
                        break;

                    case "U":
                    case "u":
                        // To update Client
                        Console.WriteLine("\nEnter Client Id to update a Client.\n");
                        string NewFormat = String.Format("{0,-5} {1, -18} {2}", "ID", "Client", "Activity");
                        Console.WriteLine($"\n{NewFormat}");
                        myClientServices.PrintClients();
                        int IdToUpdate = int.Parse(Console.ReadLine() ?? "-1");
                        while (IdToUpdate == -1 || !myClientServices.ClientExist(IdToUpdate))
                        {
                            Console.WriteLine("Please enter a valid Id associated with a Client.");
                            IdToUpdate = int.Parse(Console.ReadLine() ?? "-1");
                        }

                        var ClientToUpdate = myClientServices.FindClient(IdToUpdate);

                        Console.WriteLine("Changing Name?");        // Update Name
                        var Input = Console.ReadLine();
                        if (Input.Equals("Y", StringComparison.CurrentCultureIgnoreCase) || Input.Equals("Yes", StringComparison.CurrentCultureIgnoreCase))
                        {
                            Console.Write("Enter new name: ");
                            ClientToUpdate.Name = Console.ReadLine() ?? ClientToUpdate.Name;
                        }

                        Console.WriteLine("Entering Notes?");       // Entering Notes
                        Input = Console.ReadLine();
                        if (Input.Equals("Y", StringComparison.CurrentCultureIgnoreCase) || Input.Equals("Yes", StringComparison.CurrentCultureIgnoreCase))
                        {
                            Console.WriteLine("Enter Notes below.");
                            ClientToUpdate.Notes = Console.ReadLine() ?? ClientToUpdate.Notes;
                        }

                        Console.WriteLine("Entering Close date?");  // Entering Close date
                        Input = Console.ReadLine();
                        if (Input.Equals("Y", StringComparison.CurrentCultureIgnoreCase) || Input.Equals("Yes", StringComparison.CurrentCultureIgnoreCase))
                        {
                            Console.WriteLine("Enter Close date in format Month/Day/Year: ");
                            DateTime Dt;
                            var ValidDate = DateTime.TryParse(Console.ReadLine(), out Dt);
                            while (!ValidDate)
                            {
                                Console.WriteLine("Please enter a valid close date: ");
                                ValidDate = DateTime.TryParse(Console.ReadLine(), out Dt);
                            }

                            ClientToUpdate.CloseDate = Dt;


                            if (DateTime.Today >= Dt)
                                ClientToUpdate.IsActive = false;
                        }
                        break;

                    case "D":
                    case "d":
                        // Delete client
                        if (myClientServices.AmountofClients() > 0)
                        {
                            Console.WriteLine("Enter client Id that you want to remove.");
                            myClientServices.PrintClients();
                            int IdToDelete = int.Parse(Console.ReadLine() ?? "-1");
                            while (!myClientServices.ClientExist(IdToDelete))
                            {
                                Console.WriteLine("Please enter a valid Id associated with a Client.");
                                IdToDelete = int.Parse(Console.ReadLine() ?? "-1");
                            }

                            var ClientToDelete = myClientServices.FindClient(IdToDelete);
                            /*
                            foreach (Client Current in clients)
                            {
                                if (Current.Id > )
                                Current.Id--;
                            }
                            If I feel like making the id's not skip any numbers*/

                            // Work on removing client and if client is in a project remove it from project singleton
                            if (projects.Exists(c => c.Clients.Exists(s => s.Id == ClientToDelete.Id)))
                            {
                                // returns project and uses that to get the client within project MUST FIX WITH SINGLETON
                                var RemoveClint = projects.FirstOrDefault(c => c.Clients.FirstOrDefault(s => s.Id == ClientToDelete.Id) == ClientToDelete);
                                RemoveClint.Clients.Remove(ClientToDelete);
                            }

                            myClientServices.RemoveClient(ClientToDelete);
                            Console.WriteLine("Successfully deleted client.");
                        }
                        else
                            Console.WriteLine("No Clients to delete from.");

                        break;
                    case "Q":
                    case "q":
                        // Quit Menu
                        break;

                    default:
                        Console.WriteLine("Invalid Choice");
                        break;
                }

                Console.WriteLine("\nPress any key to continue.");
                Console.ReadKey();

            } while (!Selection.Equals("Q", StringComparison.CurrentCultureIgnoreCase));
            Console.Clear();
        }

        static void ProjectMenu()
        {
            var myProjectServices = ProjectService.Current;
            var myClientServices = ClientService.Current;

            string Selection;
            do
            {
                Console.Clear();
                // Menu
                Console.WriteLine("Project Menu\n");
                Console.WriteLine("C. Create a new Project");
                Console.WriteLine("S. Show Projects");
                Console.WriteLine("U. Update Project");
                Console.WriteLine("D. Delete Project");
                Console.WriteLine("Q. Return to Main menu");


                Selection = Console.ReadLine() ?? string.Empty;

                switch (Selection)
                {
                    case "C":
                    case "c":
                        // Add new Project
                        Console.Write("Please enter name of Project: ");
                        while (!myProjectServices.AddProject(Console.ReadLine() ?? string.Empty))
                            Console.Write("Please enter a valid project name: ");

                        Console.WriteLine($"Succesfully entered {myProjectServices.LastProject().Name} project into the system.");
                        break;

                    case "S":
                    case "s":
                        // Prints out projects in directory
                        if (myProjectServices.Count() == 0)
                        {
                            Console.WriteLine("\nNo storred projects ");
                        }
                        else
                        {
                            string strFormat = String.Format("{0,-5} {1, -18} {2, -18} {3}", "ID", "Project", "Client Ids", "Activity");
                            Console.WriteLine($"\n{strFormat}");
                            myProjectServices.PrintProjects();
                        }
                        break;

                    case "U":
                    case "u":
                        // To update project (FIXED)
                        Console.WriteLine("Enter project Id that you want to update.");
                        myProjectServices.PrintProjects();
                        int IdToUpdate = int.Parse(Console.ReadLine() ?? "-1");
                        while (IdToUpdate == -1 || !myProjectServices.Exists(IdToUpdate))
                        {
                            Console.WriteLine("Please enter a valid Id associated with a Project.");
                            IdToUpdate = int.Parse(Console.ReadLine() ?? "-1");
                        }

                        var ProjectToUpdate = projects.FirstOrDefault(c => c.Id == IdToUpdate);

                        Console.WriteLine("Changing Name?");                // Update project name
                        var Input = Console.ReadLine() ?? "n";
                        if (Input.Equals("Y", StringComparison.CurrentCultureIgnoreCase) || Input.Equals("Yes", StringComparison.CurrentCultureIgnoreCase))
                        {
                            Console.Write("Enter new name: ");
                            ProjectToUpdate.Name = Console.ReadLine() ?? ProjectToUpdate.Name;
                        }

                        Console.WriteLine("Input clients to project?");     // Input clients
                        Input = Console.ReadLine() ?? "n";
                        if (Input.Equals("Y", StringComparison.CurrentCultureIgnoreCase) || Input.Equals("Yes", StringComparison.CurrentCultureIgnoreCase))
                        {
                            Console.WriteLine(String.Format("\n{0,-5} {1, -18} {2}", "ID", "Client", "Activity"));
                            myClientServices.PrintClients();
                            Console.Write("Enter client Id to link with project: ");
                            int InputtedID = int.Parse(Console.ReadLine() ?? "-1");
                            
                            while (!myClientServices.ClientExist(InputtedID))
                            {
                                Console.Write("Please enter a valid client Id: ");
                                InputtedID = int.Parse(Console.ReadLine() ?? "-1");
                            }

                            ProjectToUpdate.Clients.Add(clients.Find(c => c.Id == InputtedID));
                        }

                        Console.WriteLine("Entering Notes?");               // Entering Notes
                        Input = Console.ReadLine() ?? "n";
                        if (Input.Equals("Y", StringComparison.CurrentCultureIgnoreCase) || Input.Equals("Yes", StringComparison.CurrentCultureIgnoreCase))
                        {
                            Console.WriteLine("Enter Notes below.");
                            ProjectToUpdate.Notes = Console.ReadLine() ?? ProjectToUpdate.Notes;
                        }

                        Console.WriteLine("Entering Close date?");          // Entering Close date, IsActive is now false
                        Input = Console.ReadLine() ?? "n";
                        if (Input.Equals("Y", StringComparison.CurrentCultureIgnoreCase) || Input.Equals("Yes", StringComparison.CurrentCultureIgnoreCase))
                        {
                            Console.WriteLine("Enter Close date in format Month/Day/Year: ");
                            DateTime Dt;
                            var ValidDate = DateTime.TryParse(Console.ReadLine(), out Dt);
                            while (!ValidDate)
                            {
                                Console.WriteLine("Please enter a valid close date: ");
                                ValidDate = DateTime.TryParse(Console.ReadLine(), out Dt);
                            }

                            ProjectToUpdate.CloseDate = Dt;


                            if (DateTime.Today >= Dt)
                                ProjectToUpdate.IsActive = false;
                        }
                        break;

                    case "D":
                    case "d":
                        // Delete Project

                        if (projects.Count > 0)
                        {
                            Console.WriteLine(String.Format("{0,-5} {1, -18} {2, -18} {3}", "ID", "Project", "Client Ids", "Activity"));
                            Console.WriteLine("Enter project Id that you want to delete.");
                            projects.ForEach(Console.WriteLine);
                            int IdToDelete = int.Parse(Console.ReadLine() ?? "-1");
                            while (IdToDelete == -1 || !projects.Exists(s => s.Id == IdToDelete))
                            {
                                Console.WriteLine("Please enter a valid Id associated with a Project.");
                                IdToDelete = int.Parse(Console.ReadLine() ?? "-1");
                            }

                            var ProjectToDelete = projects.FirstOrDefault(c => c.Id == IdToDelete);
                            Console.WriteLine($"Successfully deleted {ProjectToDelete.Name}.");
                            projects.Remove(ProjectToDelete);
                        }
                        else
                            Console.WriteLine("No projects to delete from.");
                        break;
                    case "Q":
                    case "q":
                        break;

                    default:
                        Console.WriteLine("Invalid Choice");
                        break;
                }

                Console.WriteLine("\nPress any key to continue.");
                Console.ReadKey();

            } while (!Selection.Equals("Q", StringComparison.CurrentCultureIgnoreCase));
            Console.Clear();
        }


    }
}