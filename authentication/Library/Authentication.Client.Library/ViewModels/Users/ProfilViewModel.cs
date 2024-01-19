using Authentication.Client.Library.Services.Profil;
using Authentication.Shared.Dtos;
using CommunityToolkit.Mvvm.ComponentModel;
using LibraryCore.Errors;
using LibraryCore.Responses;
using LibraryBlazorMvvm.ViewModels;
using Authentication.Shared.Model;
using System.Reflection.Emit;

namespace Authentication.Client.Library.ViewModels.User
{
    public partial class ProfilViewModel : MvvmViewModelBase
    {
        private IProfilService? _profilService;
        private Guid? _userId = null;
        private ProfilImageFileName _profilImageFileData
        {
            get
            {
                if (_userId is null)
                {
                    return new ProfilImageFileName();
                }
                else
                {
                    ProfilImageFileName profilImageFileData = new ProfilImageFileName
                    {
                        Email = Email,
                        Id = _userId.Value
                    };
                    return profilImageFileData;
                }
            }
        }

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
        [ObservableProperty]
        private ErrorStore _errorString = new();
        [ObservableProperty]
        private bool _isBusy = false;
        [ObservableProperty]
        public bool _isProfilImageExsist = false;

        public string ProfileImageUploadButtonText => IsProfilImageExsist ? "Profil kép módosítása" : "Profil kép feltöltése";
        public string ProfilImageFoleder => $"profil";

        public string ProfilImageFileName => _profilImageFileData.FileNameWithoutExtension;
        
        public string UrlToShowingProfilImage
        {
            get
            {
                string url = string.Empty;
                if (ProfilImageFileName != string.Empty)
                {
                    url = Path.Combine("StaticFiles", ProfilImageFoleder, $"{_profilImageFileData.FileName}");
                }
                return url;
            }
        }
        public bool IsProfilImageFileNameValidName => ProfilImageFileName is not null;

        public bool IsValidUser => !string.IsNullOrEmpty(Email);
        public bool IsReadOnly { get; set; } = true;

        private ProfilDto _tempProfil = new ();

        public async Task<ErrorStore> UpdateProfilAsync()
        {
            IsBusy = true;
            ErrorStore errorStore = new ErrorStore();
            if (_profilService is null)
            {                
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
                    // profil mentése, hogy változtatás után vissza lehessen állítani
                    SaveProfilToTempData();
                    ChangeToReadOnly();
                }
                errorStore = (ErrorStore) response;
            }
            ErrorString = errorStore;
            IsBusy = false;
            return errorStore;
        }

        public void ChangeToModify()
        {
            IsReadOnly = false;
            SaveProfilToTempData();
        }

        public void ChangeToReadOnly()
        {
            IsReadOnly = true;
            RestoreProfilFromTempData();
        }

        public async override Task Loading()
        {
            if (!string.IsNullOrEmpty(Email))
            {                
                await GetProfil();
                // a profil kép neve userid-ből készül, kell a userid
                await GetUserId();
                await base.Loading();
            }
        }

        public async Task<bool> CheckIsProfileImageExist()
        {
            bool isExsist = false;
            if (_profilService is not null && _profilImageFileData.IsValid)
            {
                isExsist = await _profilService.IsProfileImageExist(_profilImageFileData);
            }
            IsProfilImageExsist = isExsist;
            return isExsist;
        }

        public async Task DeleteProfilImage()
        {
            if (IsProfilImageExsist && _profilService is not null)
            {
                ControllerResponse response = await _profilService.DeleteProfilImage(_profilImageFileData);
                if (response.IsSuccess)
                {
                    IsProfilImageExsist= false;
                }
            }
        }

        public void OnProfilImageUploadSucceeded(string url)
        {
            IsProfilImageExsist = true;
        }

        private async Task GetProfil()
        {
            if (_profilService is not null)
            {
                ProfilDto result = await _profilService.GetProfilBy(Email);
                FirstName = result.FirstName;
                LastName = result.LastName;
                Email = result.Email;
            }
        }


        private async Task GetUserId()
        {
            if (_profilService is not null)
            {
                 _userId = await _profilService.GetUserIdBy(Email);
            }
        }

        private void RestoreProfilFromTempData()
        {
            FirstName = _tempProfil.FirstName;
            LastName = _tempProfil.LastName;
            Email = _tempProfil.Email;
        }

        private void SaveProfilToTempData()
        {
            _tempProfil = new ProfilDto
            {
                FirstName = FirstName,
                LastName = LastName,
                Email = Email
            };
        }
    }
}
