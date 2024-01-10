using Authentication.Client.Library.ViewModels.User;
using Microsoft.AspNetCore.Components;

namespace Authentication.Client.Library.Components
{
    public partial class ShowProfilImageForm
    {
        private string _profilFolderName => ViewModel is not null ? ViewModel.ProfilImageFoleder : string.Empty;
        private bool _canUploadProfilImage => ViewModel is not null ? _profilFolderName != string.Empty && ViewModel.IsProfilImageFileNameValidName : false;
        [CascadingParameter] public ProfilViewModel? ViewModel { get; set; }


    }
}
