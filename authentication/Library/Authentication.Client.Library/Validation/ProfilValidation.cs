using Authentication.Client.Library.ViewModels.User;
using FluentValidation;

namespace Authentication.Client.Library.Validation
{
    public class ProfilValidation : AbstractValidator<ProfilViewModel>
    {
        public ProfilValidation()
        { 
            RuleFor(x => x.LastName)
              .NotEmpty().WithMessage("A vezetéknév nem lehet üres!")
              .Matches(@"^[A-ZÍÖÜÓŐÚÉÁŰ][a-zA-ZöüóőúéáűíÍÖÜÓŐÚÉÁŰ]{1,}( {1,2}[A-ZÍÖÜÓŐÚÉÁŰ][a-zA-ZöüóőúéáűíÍÖÜÓŐÚÉÁŰ]{1,}){0,}$")
              .WithMessage("Csak szabályos vezetéknév fogadható el!");
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("A keresztnév nem lehet üres!")
                .Matches(@"^[A-ZÍÖÜÓŐÚÉÁŰ][a-zA-ZöüóőúéáűíÍÖÜÓŐÚÉÁŰ]{1,}( {1,2}[A-ZÍÖÜÓŐÚÉÁŰ][a-zA-ZöüóőúéáűíÍÖÜÓŐÚÉÁŰ]{1,}){0,}$")
                .WithMessage("Csak szabályos név fogadható el!");
        }

        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<ProfilViewModel>.CreateWithOptions((ProfilViewModel)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return result.Errors.Select(e => e.ErrorMessage);
        };
    }
}
