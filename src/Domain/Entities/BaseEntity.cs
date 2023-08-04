using Domain.Shared;
using FluentValidation;
using FluentValidation.Results;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public abstract class BaseEntity
    {
        public long Id { get; private set; }
        public DateTime? CreateAt { get; private set; } = DateTime.UtcNow;
        public DateTime? UpdateAt { get; private set; }
        public void UpdateUpdateAt() => UpdateAt = DateTime.UtcNow;

        [NotMapped]
        internal List<Error> _erros = new List<Error>();

        [NotMapped]
        public IReadOnlyCollection<Error> Errors => _erros;

        [NotMapped]
        public bool IsValid => _erros.Count == 0;

        private void AddErrorList(IList<ValidationFailure> errors)
        {
            foreach (var error in errors)
                _erros.Add(new Error(error.ErrorCode.ToString(), error.ErrorMessage));
        }

        protected bool Validate<T, J>(T validator, J obj) where T : AbstractValidator<J>
        {
            var validation = validator.Validate(obj);
            if (validation.Errors.Count() > 0)
                AddErrorList(validation.Errors);

            return IsValid;
        }
    }
}
