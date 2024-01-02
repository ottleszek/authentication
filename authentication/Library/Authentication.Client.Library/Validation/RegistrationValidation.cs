using Authentication.Client.Library.ViewModels.Accounts;
using FluentValidation;
using System.Text.RegularExpressions;

namespace Authentication.Client.Library.Validation
{
    public class RegistrationValidation : AbstractValidator<RegistrationViewModel>
    {
        public RegistrationValidation(IHttpClientFactory httpClientFactory)
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
                .MustAsync(async (value, CancellationToken) => await UniqueEmailExtension.UniqueEmail(value, httpClient))
                .When(_ => !string.IsNullOrEmpty(_.Email) && Regex.IsMatch(_.Email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase), ApplyConditionTo.CurrentValidator)
                .WithMessage("Evvel az emailcímmel már regisztrált felhasználó");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("A jelszó nem lehet üres!")
                .MinimumLength(6).WithMessage("A jelszó nem lehet rövidebb 6 karakternél!")
                .MaximumLength(16).WithMessage("A jelszó nem lehet hosszabb 16 karakternél!")
                .Matches(@"[A-Z]+").WithMessage("A jelszóban nagybetünek lenni kell!")
                .Matches(@"[a-z]+").WithMessage("A jelszóban kisbetünk lenni kell!")
                .Matches(@"[0-9]+").WithMessage("A jelszóban számnak lenni kell!")
                .Matches(@"[\@\!\?\*\.\+\-\:]+").WithMessage("A jelszóban lenni kell legalább egynek a következők közül: @!?.*+-:");
            RuleFor(x => x.ConfirmPassword).Equal(_ => _.Password).WithMessage("A két jelszó meg kell egyezzen!");
        }

        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<RegistrationViewModel>.CreateWithOptions((RegistrationViewModel)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return result.Errors.Select(e => e.ErrorMessage);
        };
    }
}
