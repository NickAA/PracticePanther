using Microsoft.AspNetCore.Mvc;
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
        { return new ClientEC().GetClients(); }

        [HttpGet("{id}")]
        public Client GetId(int id)
        {
            return new ClientEC().GetClient(id);
        }

        [HttpDelete("Delete/{id}")]
        public Client? Delete(int id)
        {
            return new ClientEC().Delete(id);
        }

        [HttpPost("Add/{clientsName}")]
        public Client Add ([FromBody]string clientsName)
        {
            return new ClientEC().Add(clientsName);
        }

        [HttpPost("Update")]
        public Client Update([FromBody]Client client)
        {
            return new ClientEC().Update(client);
        }

        private readonly ILogger<ClientsController> _logger;
        public ClientsController(ILogger<ClientsController> logger)
        {
            _logger = logger;
        }
    }
}
