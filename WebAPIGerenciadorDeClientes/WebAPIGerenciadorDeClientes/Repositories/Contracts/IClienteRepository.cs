using WebAPIGerenciadorDeClientes.Models;
namespace WebAPIGerenciadorDeClientes.Repositories
{
    public interface IClienteRepository : IRepository<Cliente>
    {
        public Task<Cliente> GetByCNPJ(string cnpj);
        public List<Cliente> GetAll();
        public List<Cliente> GetByGroup(long groupId);
    }
}
