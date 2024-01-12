﻿using Authentication.Client.Library.Services.Profil;
using Authentication.Shared.Dtos;
using CommunityToolkit.Mvvm.ComponentModel;
using LibraryCore.Errors;
using LibraryCore.Responses;
using LibraryBlazorMvvm.ViewModels;
using Authentication.Shared.Model;
using Microsoft.AspNetCore.Components.Web;

namespace Authentication.Client.Library.ViewModels.User
{
    public partial class ProfilViewModel : MvvmViewModelBase
    {
        private IProfilService? _profilService;
        private Guid? _userId = null;

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
        public string ProfilImageFileName
        {
            get
            {
                if (_userId is null || _userId==Guid.Empty)
                    return string.Empty;
                else
                {
                    
                    ProfilImageFileName profilImageUrl = new ProfilImageFileName
                    {
                        Email = Email,
                        Id = _userId.Value
                    };
                    return profilImageUrl.GetProfilImageFilelName();
                }
            }
        }

        
        public string ProfilImageUrl
        {
            get
            {
                string url = Path.Combine("StaticFiles", ProfilImageFoleder, $"{ProfilImageFileName}.jpg");
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
                //userId a profil kép mappa nevéhez
                await GetUserId();
                await base.Loading();
            }
        }

        public async Task<bool> CheckIsProfileImageExist(string url)
        {
            bool isExsist = false;
            if (_profilService is not null)
                isExsist= await _profilService.IsProfileImageExist(url);
            IsProfilImageExsist = isExsist;
            return IsProfilImageExsist;
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
