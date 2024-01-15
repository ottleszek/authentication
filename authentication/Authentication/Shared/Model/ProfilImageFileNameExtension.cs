using Authentication.Shared.Dtos;

namespace Authentication.Shared.Model
{
    public static class ProfilImageFileNameExtension
    {
        public static ProfilImageFileNameDto ToDto(this  ProfilImageFileName file) 
        {
            return new ProfilImageFileNameDto
            {
                Id = file.Id,
                Email = file.Email,
            };
        }

        public static string GetProfilImageFilelName(this ProfilImageFileName profilImageFileName)
        {
            if (profilImageFileName.Id == Guid.Empty || !profilImageFileName.IsValid)
                return string.Empty;
            string email = profilImageFileName.Email.Replace("@", ".");
            return $"{email}-{profilImageFileName.Id}.jpg";
        }
    }
}
