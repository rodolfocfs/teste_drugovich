namespace WebAPIGerenciadorDeClientes.Services.ViewModels
{
    public class ClienteViewModel
    {
        public long Id { get; set; }
        public string CNPJ { get; set; }
        public string Nome { get; set; }
        public DateTime DataFuncacao { get; set; }
        public int GrupoId { get; set; }
    }
}
