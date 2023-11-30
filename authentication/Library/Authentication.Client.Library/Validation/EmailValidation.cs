using Authentication.Client.Library.ViewModels.Login;
using FluentValidation;

namespace Authentication.Client.Library.Validation
{
    public class EmailValidation : AbstractValidator<LoginViewModel>
    {
        public EmailValidation()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Az emailcím nem lehet üres!")
                .EmailAddress()
                .WithMessage("Helytelen email cím!");
        }

        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<LoginViewModel>.CreateWithOptions((LoginViewModel)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return result.Errors.Select(e => e.ErrorMessage);
        };
    }
}
