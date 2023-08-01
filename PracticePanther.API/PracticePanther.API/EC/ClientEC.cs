using Panther.Library.DTO;
using PracticePanther.API.Database;
using PracticePanther.Models;

namespace PracticePanther.API.EC
{
    public class ClientEC
    {
        public ClientDTO GetClient(int Id)
        {
            return new ClientDTO(FakeDatabase.clients.FirstOrDefault(c => c.Id == Id));
        }

        public IEnumerable<ClientDTO> GetClients()
        { return FakeDatabase.clients.Select(c => new ClientDTO(c)); }

        public ClientDTO Add (string name)
        { 
            FakeDatabase.clients.Add(new Client(name));
            return new ClientDTO(FakeDatabase.clients.Last());
        }

        public ClientDTO Update(ClientDTO clientDTO)
        {
            int index = FakeDatabase.clients.FindIndex(c => c.Id == clientDTO.Id);
            FakeDatabase.clients[index] = new Client(clientDTO);
            return clientDTO;
        }

        public ClientDTO? Delete(int clientId)
        {
            Client clientToDelete = FakeDatabase.clients.FirstOrDefault(c => c.Id == clientId);
            if (clientToDelete != null)
                FakeDatabase.clients.Remove(clientToDelete);
            return clientToDelete != null ? new ClientDTO(clientToDelete) : null;
        }
    }
}
