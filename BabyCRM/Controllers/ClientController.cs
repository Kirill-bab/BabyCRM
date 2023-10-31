using BLL.Managers;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BabyCRM.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientController : Controller
    {
        private readonly EntityManager<Client> _clientManager;
        public ClientController(EntityManager<Client> clientManager)
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
        public async Task<IActionResult> AddClient([FromBody] Client client)
        { 
            await _clientManager.Add(client);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateClient([FromBody] Client client)
        {
            await _clientManager.Update(client);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteClient([FromRoute] int id)
        {
            await _clientManager.Delete(id);
            return Ok();
        }
    }
}
