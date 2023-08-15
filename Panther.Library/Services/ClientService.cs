using Newtonsoft.Json;
using Panther.Library.DTO;
using PracticePanther.Utilities;

namespace Panther.Library.Services
{
    public class ClientService
    {
        //private List<Client> clients;
        public List<ClientDTO> Clients
        {
            get 
            { 
                var response = new WebRequestHandler()
                    .Get("/Clients")
                    .Result;
                var clients = JsonConvert
                    .DeserializeObject<List<ClientDTO>>(response);
                return clients ?? new List<ClientDTO>();
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

        public void UpdateClient(ClientDTO client)
        { var response = new WebRequestHandler().Post($"/Clients/Update", client).Result; }
        public ClientDTO LastClient()
        { return Clients.Last(); }

        public int AmountofClients()
        { return Clients.Count; }

        public void PrintClients()
        { Clients.ForEach(Console.WriteLine); }

        public bool ClientExist(int ID)
        { return FindClient(ID) == null ? false : true ; }
        public ClientDTO? FindClient(int ID)
        {
            var response = new WebRequestHandler()
                .Get($"/Clients/{ID}")
                .Result;
            return JsonConvert.DeserializeObject<ClientDTO>(response);
        }

        // Returns searched clients
        public List<ClientDTO> Search(string query)
        { return Clients.Where(c => c.Name.ToUpper().Contains(query.ToUpper())).ToList(); }


    }
}
