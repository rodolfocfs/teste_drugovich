using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPIGerenciadorDeClientes.Services.Contracts;
using WebAPIGerenciadorDeClientes.Services.ViewModels;

namespace WebAPIGerenciadorDeClientes.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _clienteService;
        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpPost("create")]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] ClienteViewModel request)
        {
            await _clienteService.Create(request);
            return Ok();
        }

        [HttpPut("update")]
        [Authorize]
        public async Task<IActionResult> Update([FromBody] UpdateClienteViewModel request)
        {
            await _clienteService.Update(request);
            return Ok();
        }

        [HttpDelete("delete/{id:long}")]
        [Authorize]
        public async Task<IActionResult> Delete([FromRoute] long id)
        {
            _clienteService.Delete(id);
            return Ok();
        }

        [HttpGet("{id:long}")]
        [Authorize]
        public async Task<IActionResult> Get([FromRoute] long id)
        {
            return Ok(_clienteService.GetById(id));
        }

        [HttpGet("all")]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            return Ok(_clienteService.GetAll());
        }

        [HttpGet("getbygroup/{id:long}")]
        [Authorize]
        public async Task<IActionResult> GetByGroup([FromRoute] long id)
        {
            return Ok(_clienteService.GetByGroup(id));
        }

    }
}
