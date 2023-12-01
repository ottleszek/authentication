using Authentication.Shared.Dtos;
using FluentValidation;

namespace Authentication.Client.Library.Validation
{
    public class EmailValidation : AbstractValidator<UserLoginDto>
    {
        public EmailValidation()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Az emailcím nem lehet üres!")
                .EmailAddress().WithMessage("Helytelen email cím!");
        }

        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<UserLoginDto>.CreateWithOptions((UserLoginDto)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return result.Errors.Select(e => e.ErrorMessage);
        };
    }
}
