namespace Authentication.Shared.Model
{
    public static class ProfilImageFileNameExtension
    {
        public static string GetProfilImageFilelName(this ProfilImageFileName profilImageUrl)
        {
            string email = profilImageUrl.Email.Replace("@", ".");
            return $"{email}-{profilImageUrl.Id}";
        }
    }
}
