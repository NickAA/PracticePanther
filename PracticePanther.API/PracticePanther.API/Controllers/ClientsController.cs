using Microsoft.AspNetCore.Mvc;
using Panther.Library.DTO;
using PracticePanther.API.EC;


namespace PracticePanther.API.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class ClientsController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<ClientDTO> Get()
        { return new ClientEC().GetClients(); }

        [HttpGet("{id}")]
        public ClientDTO GetId(int id)
        { return new ClientEC().GetClient(id); }

        [HttpDelete("Delete/{id}")]
        public ClientDTO? Delete(int id)
        { return new ClientEC().Delete(id); }

        [HttpPost("Add/{clientsName}")]
        public ClientDTO Add ([FromBody]string clientsName)
        { return new ClientEC().Add(clientsName); }

        [HttpPost("Update")]
        public ClientDTO Update([FromBody]ClientDTO clientDTO)
        { return new ClientEC().Update(clientDTO); }

        private readonly ILogger<ClientsController> _logger;
        public ClientsController(ILogger<ClientsController> logger)
        { _logger = logger; }
    }
}
