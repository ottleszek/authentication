using Authentication.Shared.Models;
using FluentValidation;

namespace Authentication.Client.Library.Validation
{
    public class UserRoleValidation : AbstractValidator<UserRole>
    {
        public UserRoleValidation()
        {
            RuleFor(x => x.EnglishName)
                .NotEmpty().WithMessage("A felhasználkói szerep angol neve nem lehet üres!")
                .Matches(@"^[a-z]+$")
                .WithMessage("A felhasználói szerep angol neve az angol abc betűiből állhat!");
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("A felhasználói szerep neve nem lehet üres!")
                .Matches(@"^[a-zA-ZöüóőúéáűíÍÖÜÓŐÚÉÁŰ]+$")
                .WithMessage("A fehleasználói szerep neve a magyar ábécé betűiből állhat!");
        }

        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<UserRole>.CreateWithOptions((UserRole)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return result.Errors.Select(e => e.ErrorMessage);
        };
    }
}
