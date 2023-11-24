using AuthenticationLibrary.Provider.UserIdentification;
using AuthenticationLibrary.Provider;
using Microsoft.AspNetCore.Components;

namespace Authentication.Client.Library.Components
{
    public partial class UserDataComponent : IDisposable
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
            await InvokeAsync(async () =>
            {
                await Refresh();
                StateHasChanged();
            });
        }

        private async Task Refresh()
        {
            if (UserIdentificaitonProvider is not null)
            {
                UserIdentificationData = await UserIdentificaitonProvider.GetUserIdentificationData();
            }
        }
    }
}
