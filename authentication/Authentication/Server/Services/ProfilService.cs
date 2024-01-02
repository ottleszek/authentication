using Authentication.Server.Repos;
using Authentication.Shared.Dtos;
using Authentication.Shared.Models;
using LibraryDatabase.Model;
using LibraryLogging;

namespace Authentication.Server.Services
{
    public class ProfilService : IProfilService
    {
        private readonly IProfilRepo? _profilRepo;

        public ProfilService(IProfilRepo profilRepo)
        {
            _profilRepo = profilRepo;
        }

        public async Task<ProfilDto> GetUserBy(string email)
        {
            User? user=null;
            if (_profilRepo is not null)
                 user=await _profilRepo.GetUserBy(email);
            if (user is not null)
            {
                return ProfilDto.ConvertToDto(user);
            }
            return new ProfilDto();
            
        }

        public async Task<Guid> GetUserIdBy(string email)
        {
            Guid? result = Guid.Empty;
            if (_profilRepo is not null)
                result = await _profilRepo.GetIdBy(email);
            if (result == null)
            {
                return Guid.Empty;
            }
            else
            {
                return (Guid) result;
            }

        }

        public async Task<ServiceResponse> UpdateProfil(ProfilDto profilDto)
        {
            ServiceResponse response = new ServiceResponse();
            if (_profilRepo is null)
            {
                response.ClearAndAddError("A felhasználó profil frissítése nem lehetséges!");
                return response;
            }
            if (!profilDto.IsValidUser)
            {
                response.ClearAndAddError("A felhasználó profil adatai hibásak, a profil frissítés nem lehetséges!");
                return response;
            }
            else
            {
                User? user = await _profilRepo.GetUserBy(profilDto.Email);
                if (user is null)
                {
                    response.ClearAndAddError("A felhasználó a megadott email címmel nem található,a profil frissítés nem lehetséges!");
                    return response;
                }

                user.FirstName = profilDto.FirstName;
                user.LastName = profilDto.LastName;

                RepositoryResponse repoResponse = await _profilRepo.UpdateProfil(user);
                if (response.HasError)
                {
                    LoggingBroker.LogError(response.Error);
                    response.ClearAndAddError("A felhasználó profil frissítés nem lehetséges!");
                    return response;
                }
            }
            return response;
        }
    }
}
