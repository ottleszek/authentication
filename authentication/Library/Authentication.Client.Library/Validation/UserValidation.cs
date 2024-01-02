using Authentication.Shared.Models;
using FluentValidation;

namespace Authentication.Client.Library.Validation
{
    public class UserValidation : AbstractValidator<User>
    {
        public UserValidation()
        {
            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("A vezetéknév nem lehet üres!")
                .Matches(@"^[A-ZÍÖÜÓŐÚÉÁŰ][a-zöüóőúéáűí]{1,}( {1,2}[A-ZÍÖÜÓŐÚÉÁŰ][a-zöüóőúéáűí]{1,}){0,}$")
                .WithMessage("Csak szabályos vezetéknév fogadható el!");
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("A keresztnév nem lehet üres!")
                .Matches(@"^[A-ZÍÖÜÓŐÚÉÁŰ][a-zöüóőúéáűí]{1,}( {1,2}[A-ZÍÖÜÓŐÚÉÁŰ][a-zöüóőúéáűí]{1,}){0,}$")
                .WithMessage("Csak szabályos név fogadható el!");
        }

        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<User>.CreateWithOptions((User)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return result.Errors.Select(e => e.ErrorMessage);
        };
    }
}
