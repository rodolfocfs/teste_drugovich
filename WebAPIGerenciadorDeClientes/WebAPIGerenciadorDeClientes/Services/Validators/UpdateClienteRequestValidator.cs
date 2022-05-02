using FluentValidation;
using FluentValidation.Validators;
using WebAPIGerenciadorDeClientes.Services.ViewModels;
using System.Text.RegularExpressions;

namespace WebAPIGerenciadorDeClientes.Services.Validators
{
    public class UpdateClienteRequestValidator : AbstractValidator<ClienteViewModel>
    {
        public UpdateClienteRequestValidator()
        {
            RuleFor(x => x.Nome).NotEmpty();
            RuleFor(x => x.DataFuncacao).NotEmpty();
            RuleFor(x => x.CNPJ).NotEmpty();
            RuleFor(x => x.GrupoId).NotEmpty();
        }
    }
}