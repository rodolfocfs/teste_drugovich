using Microsoft.AspNetCore.Mvc;
using WebAPIGerenciadorDeClientes.Services.Contracts;
using WebAPIGerenciadorDeClientes.Services.ViewModels;

namespace WebAPIGerenciadorDeClientes.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GerenteController : ControllerBase
    {
        private readonly IGerenteService _gerenteService;
        public GerenteController(IGerenteService gerenteService)
        {
            _gerenteService = gerenteService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] GerenteViewModel request)
        {
            await _gerenteService.Create(request);
            return Ok();
        }

    }
}
