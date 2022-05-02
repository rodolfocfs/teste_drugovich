using Microsoft.EntityFrameworkCore;
using WebAPIGerenciadorDeClientes.Models;

namespace WebAPIGerenciadorDeClientes.Repositories
{
    public class GrupoRepository : EntityRepository<Grupo>, IGrupoRepository
    {
        private readonly ModelContext _context;
        public GrupoRepository(ModelContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Grupo> GetByIdAsync(long id)
            => await _context.Grupos
                .Include(g => g.Clientes).FirstOrDefaultAsync(x => x.Id == id);

        public List<Grupo> GetAll()
            => _context.Grupos.ToList();
    }
}
