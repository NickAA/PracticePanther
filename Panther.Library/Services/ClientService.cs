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

        public List<Client> clients;

        private ClientService()
        {
            clients = new List<Client>();
        }

        
    }
}
