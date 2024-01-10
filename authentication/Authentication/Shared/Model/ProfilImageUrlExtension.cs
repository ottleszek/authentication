namespace Authentication.Shared.Model
{
    public static class ProfilImageUrlExtension
    {
        public static string GetProfilImageUrlName(this ProfilImageUrl profilImageUrl)
        {
            string email = profilImageUrl.Email.Replace("@", ".");
            return $"{email}-{profilImageUrl.Id}";
        }
    }
}
