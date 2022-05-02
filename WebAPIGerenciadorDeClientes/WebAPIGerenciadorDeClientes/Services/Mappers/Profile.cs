using System;
using AutoMapper;
using WebAPIGerenciadorDeClientes.Services.ViewModels;
using WebAPIGerenciadorDeClientes.Models;
using WebAPIGerenciadorDeClientes.Common.Extensions;

namespace WebAPIGerenciadorDeClientes.Services.Mappers
{
    public class ApiProfile : Profile
    {
        public ApiProfile()
        {
            CreateMap<Gerente, GerenteViewModel>();
            CreateMap<GerenteViewModel, Gerente>()
            .ForMember(destination => destination.Senha,
                          config => config.MapFrom(request => request.Senha.ToHash()));

            CreateMap<Cliente, ClienteViewModel>();
            CreateMap<ClienteViewModel, Cliente>();

            CreateMap<Cliente, UpdateClienteViewModel>();
            CreateMap<UpdateClienteViewModel, Cliente>();

            CreateMap<Grupo, GrupoViewModel>();
            CreateMap<GrupoViewModel, Grupo>();
        }
    }
}