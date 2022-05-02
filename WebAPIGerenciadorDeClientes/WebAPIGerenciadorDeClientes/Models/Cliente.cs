using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPIGerenciadorDeClientes.Models
{
    public class Cliente
    {
        public long Id { get; set; }
        [Required]
        [StringLength(14)]
        public string CNPJ { get; set; }
        [Required]
        [StringLength(200)]
        public string Nome { get; set; }
        [Required]
        public DateTime DataFuncacao { get; set; }
        [Required]
        [ForeignKey("Grupo")]
        public long GrupoId { get; set; }
        public virtual Grupo Grupo { get; set; }
    }
}
