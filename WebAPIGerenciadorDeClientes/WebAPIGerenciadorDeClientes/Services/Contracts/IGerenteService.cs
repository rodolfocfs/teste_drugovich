using WebAPIGerenciadorDeClientes.Services.ViewModels;

namespace WebAPIGerenciadorDeClientes.Services.Contracts
{
    public interface IGerenteService
    {
        Task Create(GerenteViewModel request);
    }
}
