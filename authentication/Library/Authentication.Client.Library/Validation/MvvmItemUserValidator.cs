using Authentication.Shared.Models;
using FluentValidation;
using LibraryBlazorMvvm.ViewModels;

namespace Authentication.Client.Library.Validation
{
    public class MvvmItemUserValidator : AbstractValidator<MvvmItemViewModelBase<User>>
    {
        public MvvmItemUserValidator()
        {
            RuleFor(x => x.SelectedItem).SetValidator(new UserValidation());
        }

        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<MvvmItemViewModelBase<User>>.CreateWithOptions((MvvmItemViewModelBase<User>)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return result.Errors.Select(e => e.ErrorMessage);
        };
    }
}
