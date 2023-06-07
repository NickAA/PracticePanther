using PracticePanther.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panther.Library.Services
{
    public class ClientService
    {
        public List<Client> clients = new List<Client>();
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
                clients.Add(new Client());
                clients.Last().Name = Name;
                return true;
            }

            return false;
        }

        public void RemoveClient(Client ClientToDelete)
        {
            clients.Remove(ClientToDelete);
        }

        public Client LastClient()
        {
            return clients.Last();
        }

        public int AmountofClients()
        { return clients.Count; }

        public void PrintClients()
        {
            clients.ForEach(Console.WriteLine);
        }

        public bool ClientExist(int ID)
        {
            return clients.Exists(c => c.Id == ID);
        }
        public Client? FindClient(int ID)
        {
            return clients.FirstOrDefault(c => c.Id == ID);
        }

        private ClientService()
        {
            clients = new List<Client>();
        }

        
    }
}
