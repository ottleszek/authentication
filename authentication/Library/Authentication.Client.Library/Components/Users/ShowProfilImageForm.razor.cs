using Authentication.Client.Library.ViewModels.User;
using Microsoft.AspNetCore.Components;

namespace Authentication.Client.Library.Components
{
    public partial class ShowProfilImageForm
    {
        private string _profilFolderName => ViewModel is not null ? ViewModel.ProfilImageFoleder : string.Empty;
        private bool _canUploadProfilImage => ViewModel is not null ? _profilFolderName != string.Empty && ViewModel.IsProfilImageFileNameValidName : false;
        [Inject] public ProfilViewModel? ViewModel { get; set; }
        [Parameter] public string? UserEmail { get; set; }

        protected async override Task OnParametersSetAsync()
        {
            // Profil kép megjelenítése ha létezik
            if (!string.IsNullOrEmpty(UserEmail) && ViewModel is not null)
            {
                // Kezdőérték
                ViewModel.Email = UserEmail;
                // Adatok betöltése
                await ViewModel.Loading();
                // Profilkép létezésének ellenőrzése
                await ViewModel.CheckIsProfileImageExist();
            }
            await base.OnParametersSetAsync();
        }

    }
}
