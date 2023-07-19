using Microsoft.AspNetCore.Mvc;
using PracticePanther.API.Database;
using PracticePanther.API.EC;
using PracticePanther.Models;

namespace PracticePanther.API.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class ClientsController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Client> Get()
        { return FakeDatabase.clients; }


        [HttpGet("{id}")]
        public Client GetId(int id)
        {
            return FakeDatabase.clients.FirstOrDefault(c => c.Id == id);
        }

        [HttpDelete("Delete/{id}")]
        public Client? Delete(int id)
        {
            Client clientToDelete = FakeDatabase.clients.FirstOrDefault(c => c.Id == id);
            if (clientToDelete != null)
                FakeDatabase.clients.Remove(clientToDelete);
            return clientToDelete;
        }

        [HttpPost("Add/{clientsName}")]
        public Client Add ([FromBody]string clientsName)
        {
            return new ClientEC().Add(clientsName);
        }



        private readonly ILogger<ClientsController> _logger;
        public ClientsController(ILogger<ClientsController> logger)
        {
            _logger = logger;
        }
    }
}
