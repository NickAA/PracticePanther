using PracticePanther.API.Database;
using PracticePanther.Models;

namespace PracticePanther.API.EC
{
    public class ClientEC
    {
        public Client GetClient(int Id)
        {
            return FakeDatabase.clients.FirstOrDefault(c => c.Id == Id);
        }

        public IEnumerable<Client> GetClients()
        { return FakeDatabase.clients; }

        public Client Add (string name)
        { 
            FakeDatabase.clients.Add(new Client(name));
            return FakeDatabase.clients.Last();
        }

        public Client Update(Client client)
        {
            int index = FakeDatabase.clients.FindIndex(c => c.Id == client.Id);
            FakeDatabase.clients[index] = client;
            return client;
        }

        public Client Delete(int clientId)
        {
            Client clientToDelete = FakeDatabase.clients.FirstOrDefault(c => c.Id == clientId);
            if (clientToDelete != null)
                FakeDatabase.clients.Remove(clientToDelete);
            return clientToDelete ?? new Client("ERROR");
        }
    }
}
