using PracticePanther.API.Database;
using PracticePanther.Models;

namespace PracticePanther.API.EC
{
    public class ClientEC
    {
        public Client Add (string name)
        { 
            FakeDatabase.clients.Add(new Client(name));
            return FakeDatabase.clients.Last();
        }

        public Client Update(Client client)
        {
            var ClientToUpdate = FakeDatabase.clients.Find(c => c.Id == client.Id);
            ClientToUpdate = client;
            return ClientToUpdate;
        }
    }
}
