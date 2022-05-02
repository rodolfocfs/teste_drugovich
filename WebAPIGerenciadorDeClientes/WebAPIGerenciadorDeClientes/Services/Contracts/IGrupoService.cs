using WebAPIGerenciadorDeClientes.Services.ViewModels;

namespace WebAPIGerenciadorDeClientes.Services.Contracts
{
    public interface IGrupoService
    {
        Task Create(GrupoViewModel request, string email);
        Task Update(GrupoViewModel request, string email);
        Task Delete(long id, string email);
        GrupoViewModel GetById(long id);
        List<GrupoViewModel> GetAll();
    }
}
