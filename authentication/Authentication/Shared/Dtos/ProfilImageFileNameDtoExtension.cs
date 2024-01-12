using Authentication.Shared.Model;

namespace Authentication.Shared.Dtos
{
    public static class ProfilImageFileNameDtoExtension
    {
        public static ProfilImageFileName ToProfilImageFileName(this ProfilImageFileNameDto profilImageFileNameDto)
        {
            return new ProfilImageFileName
            {
                Email = profilImageFileNameDto.Email,
                Id = profilImageFileNameDto.Id
            };
        }
    }
}
