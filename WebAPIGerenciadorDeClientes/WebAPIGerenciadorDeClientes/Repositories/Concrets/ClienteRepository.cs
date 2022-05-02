using Microsoft.EntityFrameworkCore;
using WebAPIGerenciadorDeClientes.Models;

namespace WebAPIGerenciadorDeClientes.Repositories
{
    public class ClienteRepository : EntityRepository<Cliente>, IClienteRepository
    {
        private readonly ModelContext _context;
        public ClienteRepository(ModelContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Cliente> GetByCNPJ(string cnpj)
            => await _context.Clientes.FirstOrDefaultAsync(g => g.CNPJ == cnpj);

        public List<Cliente> GetAll()
            =>  _context.Clientes.ToList();

        public List<Cliente> GetByGroup(long groupId)
            => _context.Clientes
            .Where(c => c.GrupoId == groupId)
            .ToList();
    }
}
