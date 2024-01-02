using System.Net.Http.Json;

namespace Authentication.Client.Library.Validation
{
    public static class UniqueEmailExtension
    {
        public static async Task<bool> UniqueEmail(string email, HttpClient httpClient)
        {
            try
            {
                string url = $"/api/Registration/check-unique-user-email?email={email}";
                bool result = await httpClient.GetFromJsonAsync<bool>(url);
                return result;
            }
            catch (Exception ex)
            {
                LibraryLogging.LoggingBroker.LogError(ex.Message);
                LibraryLogging.LoggingBroker.LogError("Egyedi email cím ellenőrzés nem működik!");
            }
            return true;
        }
    }
}
