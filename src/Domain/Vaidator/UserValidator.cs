using Domain.Entities;
using FluentValidation;

namespace Domain.Vaidator
{

    public class UserValidator: AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(user => user.UserName).NotEmpty().MaximumLength(50);
            RuleFor(user => user.FullName).NotEmpty().MaximumLength(100);
            RuleFor(user => user.Password).NotEmpty().MinimumLength(8);
            RuleFor(user => user.RefreshToken).NotEmpty().MaximumLength(200);
            RuleFor(user => user.RefreshTokenExpyTime).NotEmpty().GreaterThan(DateTime.UtcNow);
        }
    }
}
