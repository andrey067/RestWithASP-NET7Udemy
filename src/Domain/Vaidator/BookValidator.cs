using Domain.Entities;
using FluentValidation;

namespace Domain.Vaidator
{
    public class BookValidator: AbstractValidator<Book>
    {
        public BookValidator()
        {
            RuleFor(book => book.Title).NotEmpty().WithMessage("O título do livro é obrigatório.");
            RuleFor(book => book.Author).NotEmpty().WithMessage("O autor do livro é obrigatório.");
            RuleFor(book => book.Price).GreaterThan(0).WithMessage("O preço do livro deve ser maior que zero.");
            RuleFor(book => book.LaunchDate).NotEmpty().WithMessage("A data de lançamento do livro é obrigatória.");
            RuleFor(book => book.LaunchDate).LessThanOrEqualTo(DateTime.Now).WithMessage("A data de lançamento não pode ser futura.");
        }
    }
}
