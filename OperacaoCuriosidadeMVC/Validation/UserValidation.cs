using FluentValidation;
using OperacaoCuriosidadeMVC.Models;
using OperacaoCuriosidadeMVC.Persistence;

namespace OperacaoCuriosidadeMVC.Validation
{
    public class UserValidation : AbstractValidator<UserModel>
    {
        private readonly UserDbContext _userDbContext;

        
        public UserValidation(UserDbContext context)
        {
            _userDbContext = context;

            RuleFor(user => user.Fatos)
                .NotEmpty().WithMessage("Os campos não podem estar vazios");
            RuleFor(user => user.Fatos.Nome)
                .MaximumLength(100).WithMessage("O nome deve conter no máximo 100 caracteres");
            RuleFor(user => user.Fatos.Email)
                .EmailAddress().WithMessage("O email inserido não é válido");
            RuleFor(user => user.Fatos.Idade)
                .InclusiveBetween(18, 100).WithMessage("A idade deve estar entre 18 e 65 anos.");

        }

    }
}
