using Authentication.Client.Library.Services.Profil;
using Authentication.Shared.Dtos;
using CommunityToolkit.Mvvm.ComponentModel;
using LibraryCore.Errors;
using LibraryCore.Responses;
using LibraryBlazorMvvm.ViewModels;

namespace Authentication.Client.Library.ViewModels.User
{
    public partial class ProfilViewModel : MvvmViewModelBase
    {
        private IProfilService? _profilService;        

        public ProfilViewModel(IProfilService profilService)
        {
            _profilService = profilService;
        }

        [ObservableProperty]
        public string _firstName=string.Empty;
        [ObservableProperty]
        public string _lastName = string.Empty;
        [ObservableProperty]
        public string _email = string.Empty;

        public bool IsValidUser => !string.IsNullOrEmpty(Email);
        public bool IsReadOnly { get; set; } = true;

        private ProfilDto _tempProfil = new ();


        public async Task<ErrorStore> UpdateProfil()
        {
            if (_profilService is null)
            {
                ErrorStore errorStore = new ErrorStore();
                errorStore.ClearAndAddError("A profil frissítése nem lehetséges!");
            }
            else
            {
                ProfilDto profilDto = new ProfilDto
                {
                    FirstName = FirstName,
                    LastName = LastName,
                    Email = Email,
                };
                ControllerResponse response = await _profilService.UpdateProfil(profilDto);
                if (response.IsSuccess)
                {
                    await GetProfil();
                    ChangeToReadOnly();
                }
                return (ErrorStore) response;
            }
            return new ErrorStore();
        }

        public void ChangeToModify()
        {
            IsReadOnly = false;
            _tempProfil = new ProfilDto
            {
                FirstName = FirstName,
                LastName = LastName,
                Email = Email
            };
        }

        public void ChangeToReadOnly()
        {
            IsReadOnly = true;
            Email = _tempProfil.Email;
            LastName=_tempProfil.LastName;
            Email = _tempProfil.Email;

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
                ProfilDto result = await _profilService.GetProfilBy(Email);
                FirstName = result.FirstName;
                LastName = result.LastName;
                Email = result.Email;
            }
        }   
    }
}
