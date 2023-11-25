using Authentication.Server.Repos;
using Authentication.Shared.Models;
using LibraryDatabase.Model;

namespace Authentication.Server.Services
{
    public class ProfilService : IProfilService
    {
        private readonly IProfilRepo? _profilRepo;

        public ProfilService(IProfilRepo profilRepo)
        {
            _profilRepo = profilRepo;
        }

        public User GetUserBy(string email)
        {
            User? result=null;
            if (_profilRepo is not null)
                 result=_profilRepo.GetUserBy(email);
            if (result is not null)
                return result;
            return new User();
            
        }

        public async Task<ServiceResponse> UpdateProfil(User user)
        {
            ServiceResponse response = new ServiceResponse();
            if (_profilRepo is null)
            {
                response.ClearAndAddError("A profil frissítése nem lehetséges");
                return response;
            }
            if (!user.IsValidUser)
            {
                response.ClearAndAddError("A felhasználó profil adatai hibásak, a felhasználó nem methető!");
                return response;
            }
            else
            {
                RepositoryResponse repoResponse = await _profilRepo.UpdateProfil(user);
                if (response.HasError)
                {
                    // hiba loggolása

                }

            }
            return response;

        }
    }
}
