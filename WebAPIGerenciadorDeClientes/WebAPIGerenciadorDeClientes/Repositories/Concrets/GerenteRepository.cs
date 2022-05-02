using Microsoft.EntityFrameworkCore;
using WebAPIGerenciadorDeClientes.Models;

namespace WebAPIGerenciadorDeClientes.Repositories
{
    public class GerenteRepository : EntityRepository<Gerente>, IGerenteRepository
    {
        private readonly ModelContext _context;
        public GerenteRepository(ModelContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Gerente> GetByEmail(string email)
            => await _context.Gerentes.FirstOrDefaultAsync(g => g.Email == email);
    }
}
