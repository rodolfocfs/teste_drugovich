using WebAPIGerenciadorDeClientes.Models;
using WebAPIGerenciadorDeClientes.Services.Contracts;
using WebAPIGerenciadorDeClientes.Services.ViewModels;
using WebAPIGerenciadorDeClientes.Repositories;
using WebAPIGerenciadorDeClientes.Common.Extensions;
namespace WebAPIGerenciadorDeClientes.Services.Concrets
{
    public class LoginService : ILoginService
    {
        private readonly ITokenService _tokenService;
        private readonly IGerenteRepository _gerenteRepository;
        public LoginService(ITokenService tokenservice, IGerenteRepository gerenteRepository)
        {
            _tokenService = tokenservice;
            _gerenteRepository = gerenteRepository;
        }
        public async Task<LoginResponseViewModel> Login(LoginRequestViewModel request)
        {

            Gerente gerente = await _gerenteRepository.GetByEmail(request.Email);

            if (gerente is null)
                throw new UnauthorizedAccessException();

            if(!HasValidCredentials(request, gerente))
            {
                throw new UnauthorizedAccessException();
            }

            var tokenResponse = _tokenService.GenerateToken(gerente);

            return new LoginResponseViewModel
            {
                Token = tokenResponse.Token,
                ExpiresIn = tokenResponse.Expiration
            };
        }

        private static bool HasValidCredentials(LoginRequestViewModel request, Gerente gerente) => gerente?.Senha.Verify(request.Senha) == true;

    }
}
