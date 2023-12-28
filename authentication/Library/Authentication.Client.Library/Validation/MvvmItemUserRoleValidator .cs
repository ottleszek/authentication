using Authentication.Shared.Models;
using FluentValidation;
using LibraryBlazorMvvm.ViewModels;

namespace Authentication.Client.Library.Validation
{
    public class MvvmItemUserRoleValidator : AbstractValidator<MvvmItemViewModelBase<UserRole>>
    {
        public MvvmItemUserRoleValidator()
        {
            RuleFor(x => x.SelectedItem).SetValidator(new UserRoleValidation());
        }

        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<MvvmItemViewModelBase<UserRole>>.CreateWithOptions((MvvmItemViewModelBase<UserRole>)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return result.Errors.Select(e => e.ErrorMessage);
        };
    }
}
