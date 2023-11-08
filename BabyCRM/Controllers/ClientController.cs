using BLL.Commands.Clients;
using BLL.Managers;
using DAL.Models;
using DAL.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace BabyCRM.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientController : Controller                  // TODO: rework to minimal API with Carter, add migrations
    {
        private readonly EntityManager<ClientDataModel, CreateClientCommand, UpdateClientCommand> _clientManager;
        private readonly IConfiguration _configuration;
        public ClientController(EntityManager<ClientDataModel, CreateClientCommand, UpdateClientCommand> clientManager,
            IConfiguration config)
        {
            _clientManager = clientManager;
            _configuration = config;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var results = await _clientManager.GetAll();
            if (results is null || !results.Any()) return NoContent();
            return Ok(results);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var result = await _clientManager.Get(id);
            return result is null ? NotFound() : Ok(result);
        }

        [HttpGet]
        [Route("generate/{quantity:int}")]
        public async Task<IActionResult> GenerateSamples([FromRoute] int quantity = 20)
        {
            await (_clientManager as ClientManager).GenerateClients(quantity);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> AddClient([FromBody] CreateClientCommand command)
        {
            command.CreatedBy = User.Identity.Name ?? "Admin-Default";
            await _clientManager.Add(command);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateClient([FromBody] UpdateClientCommand command)
        {
            await _clientManager.Update(command);
            return Ok();
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteClient([FromRoute] int id)
        {
            await _clientManager.Delete(id);
            return Ok();
        }
    }
}
