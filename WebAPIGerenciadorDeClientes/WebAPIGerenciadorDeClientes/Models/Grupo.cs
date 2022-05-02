using System.ComponentModel.DataAnnotations;
namespace WebAPIGerenciadorDeClientes.Models
{
    public class Grupo
    {
        public long Id { get; set; }
        [StringLength(200)]
        [Required]
        public string Nome { get; set; }
        public virtual ICollection<Cliente> Clientes { get; set; }   
    }
}
