using WebAPIGerenciadorDeClientes.Models;
namespace WebAPIGerenciadorDeClientes.Services.Contracts
{
    public interface ITokenService
    {
        TokenResponse GenerateToken(Gerente gerente);
    }
}
