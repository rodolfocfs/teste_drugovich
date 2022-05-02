using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPIGerenciadorDeClientes.Services.Contracts;
using WebAPIGerenciadorDeClientes.Services.ViewModels;

namespace WebAPIGerenciadorDeClientes.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;
        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost("signin")]
        public async Task<IActionResult> Login([FromBody] LoginRequestViewModel request)
        {
            var token = await _loginService.Login(request);
            return Ok(token);
        }

        [HttpGet("teste")]
        [Authorize]
        public async Task<IActionResult> Teste()
        {
            return Ok("ok");
        }
    }
}
