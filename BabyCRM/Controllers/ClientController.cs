using BLL.Managers;
using DAL.DbManagers;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BabyCRM.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientController : Controller
    {
        private readonly IEntityManager<Client> _clientManager;
        public ClientController(IEntityManager<Client> clientManager)
        {
            _clientManager = clientManager;
        }

        [HttpGet]
        public async Task<IEnumerable<Client>> Get()
        {
            return await _clientManager.GetAll();
        }

        [HttpPost]
        public async Task<IActionResult> AddClient([FromBody] Client client)
        { 
            await _clientManager.Add(client);
            return Ok();
        }
    }
}
