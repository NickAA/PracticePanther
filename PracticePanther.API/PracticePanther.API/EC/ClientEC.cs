using Panther.Library.DTO;
using PracticePanther.API.Database;
using PracticePanther.Models;

namespace PracticePanther.API.EC
{
    public class ClientEC
    {
        public ClientDTO GetClient(int Id)
        {
            return new ClientDTO(Filebase.Current.Clients.FirstOrDefault(c => c.Id == Id));
        }

        public IEnumerable<ClientDTO> GetClients()
        { return Filebase.Current.Clients.Select(c => new ClientDTO(c)); }

        public ClientDTO Add (string name)
        { 
            Filebase.Current.Add(new Client(name));
            return new ClientDTO(Filebase.Current.Clients.Last());
        }

        public ClientDTO Update(ClientDTO clientDTO)
        {
            return new ClientDTO(Filebase.Current.Update(new Client(clientDTO)));
        }

        public ClientDTO? Delete(int clientId)
        {
            Client clientToDelete = Filebase.Current.Clients.FirstOrDefault(c => c.Id == clientId);
            if (clientToDelete != null)
                Filebase.Current.Delete(clientId);
            return clientToDelete != null ? new ClientDTO(clientToDelete) : null;
        }
    }
}
