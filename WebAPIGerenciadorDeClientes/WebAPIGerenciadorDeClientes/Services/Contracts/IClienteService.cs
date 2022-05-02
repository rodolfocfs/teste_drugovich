using WebAPIGerenciadorDeClientes.Services.ViewModels;

namespace WebAPIGerenciadorDeClientes.Services.Contracts
{
    public interface IClienteService
    {
        Task Create(ClienteViewModel request);
        Task Update(UpdateClienteViewModel request);
        void Delete(long id);
        ClienteViewModel GetById(long id);
        List<ClienteViewModel> GetAll();
        List<ClienteViewModel> GetByGroup(long grupoId);
    }
}
