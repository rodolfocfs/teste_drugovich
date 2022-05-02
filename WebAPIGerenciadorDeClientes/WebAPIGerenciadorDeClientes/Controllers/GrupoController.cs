using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPIGerenciadorDeClientes.Services.Contracts;
using WebAPIGerenciadorDeClientes.Services.ViewModels;

namespace WebAPIGerenciadorDeClientes.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GrupoController : BaseController
    {
        private readonly IGrupoService _grupoService;
        public GrupoController(IGrupoService grupoService)
        {
            _grupoService = grupoService;
        }

        [HttpPost("create")]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] GrupoViewModel request)
        {
            await _grupoService.Create(request, GetEmailGerenteLogado());
            return Ok();
        }

        [HttpPut("update")]
        [Authorize]
        public async Task<IActionResult> Update([FromBody] GrupoViewModel request)
        {
            await _grupoService.Update(request, GetEmailGerenteLogado());
            return Ok();
        }

        [HttpDelete("delete/{id:long}")]
        [Authorize]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            await _grupoService.Delete(id, GetEmailGerenteLogado());
            return Ok();
        }

        [HttpGet("{id:long}")]
        [Authorize]
        public async Task<IActionResult> Get([FromRoute] long id)
        {
            return Ok(_grupoService.GetById(id));
        }

        [HttpGet("all")]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            return Ok(_grupoService.GetAll());
        }

    }
}
