using FluentValidation;
using FluentValidation.Validators;
using WebAPIGerenciadorDeClientes.Services.ViewModels;
using System.Text.RegularExpressions;

namespace WebAPIGerenciadorDeClientes.Services.Validators
{
    public class GerenteCreateRequestValidator : AbstractValidator<GerenteViewModel>
    {
        public GerenteCreateRequestValidator()
        {
            RuleFor(x => x.Nome).NotEmpty();
            RuleFor(x => x.Email).NotEmpty().EmailAddress(EmailValidationMode.Net4xRegex);
            RuleFor(x => x.Senha).NotEmpty();
            RuleFor(x => x.Level).NotEmpty();
        }
    }
}