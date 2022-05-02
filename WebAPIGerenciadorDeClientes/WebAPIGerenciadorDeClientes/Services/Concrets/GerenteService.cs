using WebAPIGerenciadorDeClientes.Models;
using WebAPIGerenciadorDeClientes.Services.Contracts;
using WebAPIGerenciadorDeClientes.Services.ViewModels;
using WebAPIGerenciadorDeClientes.Repositories;
using AutoMapper;
using WebAPIGerenciadorDeClientes.Common.Exceptions;

namespace WebAPIGerenciadorDeClientes.Services.Concrets
{
    public class GerenteService : IGerenteService
    {
        private readonly IGerenteRepository _gerenteRepository;
        private readonly IMapper _mapper;
        public GerenteService(IGerenteRepository gerenteRepository, IMapper mapper)
        {
            _gerenteRepository = gerenteRepository;
            _mapper = mapper;
        }
        public async Task Create(GerenteViewModel request)
        {
            var existente = await _gerenteRepository.GetByEmail(request.Email);
            if (existente is not null)
                throw new InvalidInputException("Já existe um gerente com esse email no sistema.");

            Gerente gerente = _mapper.Map<Gerente>(request);
            await _gerenteRepository.AddAndSaveAsync(gerente);
        }

    }
}
