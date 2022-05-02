using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebAPIGerenciadorDeClientes.Controllers
{
    public class BaseController : ControllerBase
    {
        public BaseController()
        {
        }

        protected string GetEmailGerenteLogado()
        {
            return User.FindFirst(ClaimTypes.Email)?.Value;
        }
    }
}
