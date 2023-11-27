using FluentValidation;

namespace Authentication.Client.Library.ViewModels.Login
{
    public class LoginValidation : AbstractValidator<LoginViewModel>
    {
        public LoginValidation() 
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Az emailcím nem lehet üres!")
                .EmailAddress()
                .WithMessage("Helytelen email cím!");
            /*
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("A jelszó nem lehet üres!")
                .MinimumLength(6).WithMessage("A jelszó nem lehet rövidebb 6 karakternél!")
                .MaximumLength(16).WithMessage("A jelszó nem lehet hosszabb 16 karakternél!")
                .Matches(@"[A-Z]+").WithMessage("A jelszóban nagybetünek lenni kell!")
                .Matches(@"[a-z]+").WithMessage("A jelszóban kisbetünk lenni kell!")
                .Matches(@"[0-9]+").WithMessage("A jelszóban számnak lenni kell!")
                .Matches(@"[\@\!\?\*\.\+\-\:]+").WithMessage("A jelszóban lenni kell legalább egynek a következők közül: @!?.*+-:");
            */
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
