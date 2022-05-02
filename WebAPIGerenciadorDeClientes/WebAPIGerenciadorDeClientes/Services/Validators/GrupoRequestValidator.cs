using FluentValidation;
using FluentValidation.Validators;
using WebAPIGerenciadorDeClientes.Services.ViewModels;
using System.Text.RegularExpressions;

namespace WebAPIGerenciadorDeClientes.Services.Validators
{
    public class GrupoRequestValidator : AbstractValidator<GrupoViewModel>
    {
        public GrupoRequestValidator()
        {
            RuleFor(x => x.Nome).NotEmpty();
        }
    }
}