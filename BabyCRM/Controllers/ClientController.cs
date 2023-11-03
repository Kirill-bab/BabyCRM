using BLL.Commands.Clients;
using BLL.Managers;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BabyCRM.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientController : Controller                  // TODO: rework to minimal API with Carter, add migrations
    {
        private readonly EntityManager<ClientDataModel, CreateClientCommand, UpdateClientCommand> _clientManager;
        public ClientController(EntityManager<ClientDataModel, CreateClientCommand, UpdateClientCommand> clientManager)
        {
            _clientManager = clientManager;
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
