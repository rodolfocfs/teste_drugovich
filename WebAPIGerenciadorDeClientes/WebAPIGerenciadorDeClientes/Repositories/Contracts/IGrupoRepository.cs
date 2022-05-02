using WebAPIGerenciadorDeClientes.Models;
namespace WebAPIGerenciadorDeClientes.Repositories
{
    public interface IGrupoRepository : IRepository<Grupo>
    {
        List<Grupo> GetAll();
        Task<Grupo> GetByIdAsync(long id);
    }
}
