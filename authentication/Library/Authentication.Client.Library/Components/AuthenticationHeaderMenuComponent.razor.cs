﻿using AuthenticationLibrary.Provider.UserIdentification;
using AuthenticationLibrary.Provider;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace Authentication.Client.Library.Components
{
    public partial class AuthenticationHeaderMenuComponent : IDisposable
    {
        private UserIdentificationData? UserIdentificationData;

        [Inject] IUserIdentificaitonProvider? UserIdentificaitonProvider { get; set; }

        public void Dispose()
        {

        }

        protected override async Task OnInitializedAsync()
        {
            if (UserIdentificaitonProvider is not null)
            {
                UserIdentificationData = await UserIdentificaitonProvider.GetUserIdentificationData();
                UserIdentificaitonProvider.UserIdentificationDataChanged += OnUserIdentificationDataChanged;
            }


        }

        private async void OnUserIdentificationDataChanged(object? sender, EventArgs e)
        {
            if (UserIdentificaitonProvider is not null)
            {
                UserIdentificationData = await UserIdentificaitonProvider.GetUserIdentificationData();
                Refresh();
            }
        }

        private void Refresh()
        {
            StateHasChanged();
        }
    }
}
