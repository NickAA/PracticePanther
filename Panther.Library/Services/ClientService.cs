using Newtonsoft.Json;
using PracticePanther.Models;
using PracticePanther.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Panther.Library.Services
{
    public class ClientService
    {
        //private List<Client> clients;
        public List<Client> Clients
        {
            get 
            { 
                var response = new WebRequestHandler()
                    .Get("/Clients")
                    .Result;
                var clients = JsonConvert
                    .DeserializeObject<List<Client>>(response);
                return clients ?? new List<Client>();
            }
        }

        private ClientService()
        {

        }

        private static ClientService? instance;
        private static object _lock = new object();

        public static ClientService Current
        { get
            {
                lock (_lock)
                {
                    if (instance == null)
                        instance = new ClientService();
                }

                return instance;
            }
        }

        public bool AddClient(string Name)
        {
            if (Name != string.Empty)
            {
                var response = new WebRequestHandler().Post($"/Clients/Add/{Name}", Name).Result;
                return true;
            }

            return false;
        }

        public void RemoveClient(int ClientToDelete)
        { var response = new WebRequestHandler().Delete($"/Clients/Delete/{ClientToDelete}").Result; }

        public void UpdateClient(Client client)
        { var response = new WebRequestHandler().Post($"/Clients/Update", client).Result; }
        public Client LastClient()
        { return Clients.Last(); }

        public int AmountofClients()
        { return Clients.Count; }

        public void PrintClients()
        { Clients.ForEach(Console.WriteLine); }

        public bool ClientExist(int ID)
        { return FindClient(ID) == null ? false : true ; }
        public Client? FindClient(int ID)
        {
            var response = new WebRequestHandler()
                .Get($"/Clients/{ID}")
                .Result;
            return JsonConvert.DeserializeObject<Client>(response);
        }

        // Adds Clients
        //private ClientService()
        //{
        //    //clients = new List<Client>();
        //    //AddClient("Nick");
        //    //AddClient("Andrew");
        //    //AddClient("Penelope");
        //}

        // Returns searched clients
        public List<Client> Search(string query)
        { return Clients.Where(c => c.Name.ToUpper().Contains(query.ToUpper())).ToList(); }


    }
}
