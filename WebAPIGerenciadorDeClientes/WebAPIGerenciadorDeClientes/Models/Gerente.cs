using System.ComponentModel.DataAnnotations;
namespace WebAPIGerenciadorDeClientes.Models
{
    public class Gerente
    {
        public long Id { get; set; }
        [StringLength(200)]
        [Required]
        public string Nome { get; set; }
        [StringLength(50)]
        [Required]
        public string Email { get; set; }
        [StringLength(200)]
        [Required]
        public string Senha { get; set; }
        [Required]
        public Level Level { get; set; }
    }
    public enum Level
    {
        Um = 1,
        Dois = 2
    }
}
