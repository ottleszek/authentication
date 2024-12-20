﻿using Authentication.Shared.Models;
using FluentValidation;
using LibraryBlazorMvvm.ViewModels;

namespace Authentication.Client.Library.Validation
{
    public class MvvmItemUserValidation : AbstractValidator<MvvmItemViewModelBase<User>>
    {
        public MvvmItemUserValidation(UserValidation userValidation)
        {
            RuleFor(x => x.SelectedItem).SetValidator(userValidation);
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
