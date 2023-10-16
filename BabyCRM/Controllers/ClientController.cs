using DAL.DbManagers;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BabyCRM.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientController : Controller
    {
        private readonly ClientDbManager _clientManager;
        public ClientController(DbManager<Client> clientManager)
        {
            _clientManager = (ClientDbManager)clientManager;
        }

        [HttpGet]
        public async Task<IEnumerable<Client>> Get()
        {
            return await _clientManager.GetAll();
        }
    }
}
