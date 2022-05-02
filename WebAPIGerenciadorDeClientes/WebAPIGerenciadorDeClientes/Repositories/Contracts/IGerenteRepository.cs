using WebAPIGerenciadorDeClientes.Models;
namespace WebAPIGerenciadorDeClientes.Repositories
{
    public interface IGerenteRepository : IRepository<Gerente>
    {
        public Task<Gerente> GetByEmail(string email);
    }
}
