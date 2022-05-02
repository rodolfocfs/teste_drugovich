using WebAPIGerenciadorDeClientes.Models;
using WebAPIGerenciadorDeClientes.Services.Contracts;
using WebAPIGerenciadorDeClientes.Services.ViewModels;
using WebAPIGerenciadorDeClientes.Repositories;
using AutoMapper;
using WebAPIGerenciadorDeClientes.Common.Exceptions;

namespace WebAPIGerenciadorDeClientes.Services.Concrets
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IMapper _mapper;
        private readonly IGrupoRepository _grupoRepository;
        public ClienteService(IClienteRepository clienteRepository, IMapper mapper, IGrupoRepository grupoRepository)
        {
            _clienteRepository = clienteRepository;
            _mapper = mapper;
            _grupoRepository = grupoRepository;
        }
        public async Task Create(ClienteViewModel request)
        {
            var existentePorCnpj = await _clienteRepository.GetByCNPJ(request.CNPJ);
            if(existentePorCnpj is not null)
                throw new InvalidInputException("Já existe um cliente com o mesmo CNPJ que pertence a outro grupo.");

            var grupoExistente = _grupoRepository.GetById(request.GrupoId);
            if(grupoExistente is null)
                throw new InvalidInputException("Grupo não encontrado.");

            Cliente cliente = _mapper.Map<Cliente>(request);
            await _clienteRepository.AddAndSaveAsync(cliente);
        }

        public async Task Update(UpdateClienteViewModel request)
        {
            var existente = _clienteRepository.GetById(request.Id);
            if (existente is null)
                throw new InvalidInputException("Cliente não encontrado!");

            var updated = _mapper.Map(request, existente);

            await _clienteRepository.UpdateAndSaveAsync(updated);
        }

        public void Delete(long id)
        {
            var existente = _clienteRepository.GetById(id);
            if (existente is null)
                throw new InvalidInputException("Cliente não encontrado!");

            _clienteRepository.RemoveAndSave(existente);
        }

        public ClienteViewModel GetById(long id)
        {
            Cliente cliente = _clienteRepository.GetById(id);
            return  _mapper.Map<ClienteViewModel>(cliente);
        }

        public List<ClienteViewModel> GetAll()
        {
            var clientes = _clienteRepository.GetAll();
            return _mapper.Map<List<Cliente>, List<ClienteViewModel>>(clientes);
        }

        public List<ClienteViewModel> GetByGroup(long id)
        {
            var clientes = _clienteRepository.GetByGroup(id);
            return _mapper.Map<List<Cliente>, List<ClienteViewModel>>(clientes);
        }

    }
}
