namespace Authentication.Shared.Model
{
    public static class ProfilImageUrlExtension
    {
        public static string GetProfilImageUrlName(this ProfilImageFileName profilImageUrl)
        {
            string email = profilImageUrl.Email.Replace("@", ".");
            return $"{email}-{profilImageUrl.Id}";
        }
    }
}
