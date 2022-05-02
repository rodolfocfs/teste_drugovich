using WebAPIGerenciadorDeClientes.Models;
using WebAPIGerenciadorDeClientes.Services.Contracts;
using WebAPIGerenciadorDeClientes.Services.ViewModels;
using WebAPIGerenciadorDeClientes.Repositories;
using AutoMapper;
using WebAPIGerenciadorDeClientes.Common.Exceptions;
using System.Security.Claims;

namespace WebAPIGerenciadorDeClientes.Services.Concrets
{
    public class GrupoService : IGrupoService
    {
        private readonly IGrupoRepository _grupoRepository;
        private readonly IMapper _mapper;
        private readonly IGerenteRepository _gerenteRepository;
        public GrupoService(IGrupoRepository grupoRepository, IMapper mapper, IGerenteRepository gerenteRepository)
        {
            _grupoRepository = grupoRepository;
            _mapper = mapper;
            _gerenteRepository = gerenteRepository;
        }
        public async Task Create(GrupoViewModel request, string email)
        {
            await VerificaGerenteNivelDois(email);
            Grupo grupo = _mapper.Map<Grupo>(request);
            await _grupoRepository.AddAndSaveAsync(grupo);
        }

        public async Task Update(GrupoViewModel request, string email)
        {
            await VerificaGerenteNivelDois(email);
            var existente = _grupoRepository.GetById(request.Id);
            if (existente is null)
                throw new InvalidInputException("Grupo não encontrado!");

            var updated = _mapper.Map(request, existente);

            await _grupoRepository.UpdateAndSaveAsync(updated);
        }

        public async Task Delete(long id, string email)
        {
            await VerificaGerenteNivelDois(email);
            var existente = await _grupoRepository.GetByIdAsync(id);
            if (existente is null)
                throw new InvalidInputException("Grupo não encontrado!");

            _grupoRepository.RemoveAndSave(existente);
        }

        public GrupoViewModel GetById(long id)
        {
            Grupo grupo = _grupoRepository.GetById(id);
            return  _mapper.Map<GrupoViewModel>(grupo);
        }

        public List<GrupoViewModel> GetAll()
        {
            var grupos = _grupoRepository.GetAll();
            return _mapper.Map<List<Grupo>, List<GrupoViewModel>>(grupos);
        }

        public async Task VerificaGerenteNivelDois(string email)
        {
            var gerente = await _gerenteRepository.GetByEmail(email);
            if (gerente.Level != Level.Dois)
                throw new InvalidInputException("Apenas gerentes de nível dois podem executar esta ação.");
        }

    }
}
