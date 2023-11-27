using Authentication.Client.Library.Services.Profil;
using Authentication.Shared.Dtos;
using LibraryCore.Responses;

namespace Authentication.Client.Library.ViewModels.User
{
    public class ProfilViewModel : IProfilViewModel
    {
        private IProfilService? _profilService;

        public ProfilViewModel(IProfilService profilService)
        {
            _profilService = profilService;
        }

        public ProfilDto ProfilDto { get; set; } = new ProfilDto();

        public async Task GetProfilBy(string email)
        {
            if (_profilService is not null)
                ProfilDto = await _profilService.GetProfilBy(email);
        }

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
    }
}
