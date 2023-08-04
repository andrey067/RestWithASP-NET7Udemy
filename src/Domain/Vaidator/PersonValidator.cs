using Domain.Entities;
using FluentValidation;

namespace Domain.Vaidator
{
    public class PersonValidator: AbstractValidator<Person>
    {
        public PersonValidator()
        {
            RuleFor(person => person.FirstName).NotEmpty().WithMessage("O primeiro nome é obrigatório.");
            RuleFor(person => person.LastName).NotEmpty().WithMessage("O último nome é obrigatório.");
            RuleFor(person => person.Address).NotEmpty().WithMessage("O endereço é obrigatório.");
            RuleFor(person => person.Gender).NotEmpty().WithMessage("O gênero é obrigatório.");
        }
    }
}
