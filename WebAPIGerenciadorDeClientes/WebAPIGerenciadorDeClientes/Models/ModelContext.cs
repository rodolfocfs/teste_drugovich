using WebAPIGerenciadorDeClientes.Models.Extensions;
using Microsoft.EntityFrameworkCore;

namespace WebAPIGerenciadorDeClientes.Models
{
    public class ModelContext : DbContext
    {
        public ModelContext(DbContextOptions options) : base(options) { }

        public DbSet<Gerente> Gerentes { get; set; }
        public DbSet<Grupo> Grupos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.RemovePluralizingTableNameConvention();
        }


    }
}
