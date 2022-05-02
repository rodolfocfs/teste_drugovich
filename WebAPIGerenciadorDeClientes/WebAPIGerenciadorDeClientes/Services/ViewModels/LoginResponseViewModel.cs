namespace WebAPIGerenciadorDeClientes.Services.ViewModels
{
    public class LoginResponseViewModel
    {
        public string Token { get; set; }
        public DateTime ExpiresIn { get; set; }
    }
}
