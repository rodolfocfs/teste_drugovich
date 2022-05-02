using WebAPIGerenciadorDeClientes.Services.ViewModels;

namespace WebAPIGerenciadorDeClientes.Services.Contracts
{
    public interface ILoginService
    {
        Task<LoginResponseViewModel> Login(LoginRequestViewModel request);
    }
}
