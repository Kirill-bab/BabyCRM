using BLL.Commands.Filials;
using BLL.Managers;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace BabyCRM.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilialController : Controller                  // TODO: rework to minimal API with Carter, add migrations
    {
        private readonly EntityManager<FilialDataModel, CreateFilialCommand, UpdateFilialCommand> _filialManager;
        public FilialController(EntityManager<FilialDataModel, CreateFilialCommand, UpdateFilialCommand> filialManager)
        {
            _filialManager = filialManager;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var results = await _filialManager.GetAll();
            if (results is null || !results.Any()) return NoContent();
            return Ok(results);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var result = await _filialManager.Get(id);
            return result is null ? NotFound() : Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddFilial([FromBody] CreateFilialCommand command)
        {
            command.CreatedBy = User.Identity.Name ?? "Admin-Default";
            await _filialManager.Add(command);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFilial([FromBody] UpdateFilialCommand command)
        {
            await _filialManager.Update(command);
            return Ok();
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteFilial([FromRoute] int id)
        {
            await _filialManager.Delete(id);
            return Ok();
        }
    }
}
