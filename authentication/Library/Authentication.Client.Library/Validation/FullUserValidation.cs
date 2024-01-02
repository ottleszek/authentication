using Authentication.Shared.Models;
using FluentValidation;
using FluentValidation.Validators;
using System.Text.RegularExpressions;

namespace Authentication.Client.Library.Validation
{
    public class FullUserValidation : AbstractValidator<User>
    {
        public FullUserValidation(IHttpClientFactory httpClientFactory)
        {
            HttpClient httpClient = httpClientFactory.CreateClient("AuthenticationApi");
            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("A vezetéknév nem lehet üres!")
                .Matches(@"^[A-ZÍÖÜÓŐÚÉÁŰ][a-zöüóőúéáűí]{1,}( {1,2}[A-ZÍÖÜÓŐÚÉÁŰ][a-zöüóőúéáűí]{1,}){0,}$")
                .WithMessage("Csak szabályos vezetéknév fogadható el!");
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("A keresztnév nem lehet üres!")
                .Matches(@"^[A-ZÍÖÜÓŐÚÉÁŰ][a-zöüóőúéáűí]{1,}( {1,2}[A-ZÍÖÜÓŐÚÉÁŰ][a-zöüóőúéáűí]{1,}){0,}$")
                .WithMessage("Csak szabályos név fogadható el!");
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Az email nem lehet üres!")
                .EmailAddress().WithMessage("Helytelen email cím!")
                .Matches("^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$").WithMessage("Az email cím helytelen");
            RuleFor(x => x.Email)
                .MustAsync(async (value, CancellationToken) => await UniqueEmailExtension.UniqueEmail(value, httpClient))
                .When(_ => !string.IsNullOrEmpty(_.Email) && Regex.IsMatch(_.Email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase), ApplyConditionTo.CurrentValidator).WithMessage("Az email cím már létezik!");
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
