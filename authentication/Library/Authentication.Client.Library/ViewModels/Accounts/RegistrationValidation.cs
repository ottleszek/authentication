using FluentValidation;
using System.Net.Http.Json;
using System.Text.RegularExpressions;

namespace Authentication.Client.Library.ViewModels.Accounts
{
    public class RegistrationValidation : AbstractValidator<RegistrationViewModel>
    {
        private readonly HttpClient _httpClient;

        public RegistrationValidation(HttpClient httpClient)
        {
            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("A vezetéknév nem lehet üres!")
                .Matches(@"^[A-ZÍÖÜÓŐÚÉÁŰ][a-zA-ZöüóőúéáűíÍÖÜÓŐÚÉÁŰ]{1,}( {1,2}[A-ZÍÖÜÓŐÚÉÁŰ][a-zA-ZöüóőúéáűíÍÖÜÓŐÚÉÁŰ]{1,}){0,}$")
                .WithMessage("Csak szabályos vezetéknév fogadható el!");           
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("A keresztnév nem lehet üres!")
                .Matches(@"^[A-ZÍÖÜÓŐÚÉÁŰ][a-zA-ZöüóőúéáűíÍÖÜÓŐÚÉÁŰ]{1,}( {1,2}[A-ZÍÖÜÓŐÚÉÁŰ][a-zA-ZöüóőúéáűíÍÖÜÓŐÚÉÁŰ]{1,}){0,}$")
                .WithMessage("Csak szabályos név fogadható el!"); 
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Az email nem lehet üres!")
                .MustAsync(async (value, CancellationToken) => await UniqueEmail(value))
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
            
            _httpClient = httpClient;
        }

        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<RegistrationViewModel>.CreateWithOptions((RegistrationViewModel)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return result.Errors.Select(e => e.ErrorMessage);
        };

        private async Task<bool> UniqueEmail(string email)
        {
            try
            {
                string url = $"/api/Account/check-unique-user-email?email={email}";
                bool result = await _httpClient.GetFromJsonAsync<bool>(url);
                return result;
            }
            catch(Exception)
            {
            }
            return true;
        }
    }
}
