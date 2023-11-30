using Authentication.Client.Library.Services.Profil;
using Authentication.Shared.Dtos;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LibraryCore.Responses;
using LibraryMvvm.Base;

namespace Authentication.Client.Library.ViewModels.User
{
    public partial class ProfilViewModel : ViewModelBase, IProfilViewModel
    {
        private IProfilService? _profilService;
        private ProfilDto tempProfil = new();

        public ProfilViewModel(IProfilService profilService)
        {
            _profilService = profilService;
        }

        public string Email { get; set; } = string.Empty;
        public bool IsReadOnly { get; set; } = true;

        [ObservableProperty]
        private ProfilDto _profilDto = new();
       
        [RelayCommand]
        public async Task UpdateProfil()
        {
            if (ProfilDto.IsValidUser && _profilService is not null)
            {
                ControllerResponse response = await _profilService.UpdateProfil(ProfilDto);
                if (response.HasError) 
                {
                }
            }
        }

        public void ChangeToModify()
        {
            IsReadOnly = false;
        }

        public void ChangeToReadOnly()
        {
            IsReadOnly = true;
            ProfilDto.Set(tempProfil);

        }

        public async override Task Loading()
        {
            if (!string.IsNullOrEmpty(Email))
            {
                await GetProfil();
                await base.Loading();
            }
        }

        public async Task GetProfil()
        {
            if (_profilService is not null)
            {
                ProfilDto = await _profilService.GetProfilBy(Email);
                tempProfil.Set(ProfilDto);
            }
        }

        
    }
}
