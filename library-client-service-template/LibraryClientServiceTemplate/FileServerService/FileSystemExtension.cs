using System.Net;

namespace LibraryClientServiceTemplate.FileServerService
{
    public static class FileSystemExtension
    {
        public static bool IsUrlExist(string url)
        {
            if (File.Exists(url))
                return true;
            else
                return false;
        }


        /*public static bool IsUrlExist(string url, int timeOutMs = 1000)
        {
            bool exists = false;
            HttpWebResponse response = null;
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "HEAD";
            request.Timeout = timeOutMs; // milliseconds
            request.AllowAutoRedirect = false;

            try
            {
                response = (HttpWebResponse)request.GetResponse();
                exists = response.StatusCode == HttpStatusCode.OK;
            }
            catch
            {
                exists = false;
            }
            finally
            {
                // close your response.
                if (response != null)
                    response.Close();
            }
            return exists;
        }*/
        /*
        private readonly HttpClient? _httpClient
        public FileSystemExtension(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("AuthenticationApi");
        }

        private async Task<bool> isFileExist(string url)
        {
            if (_httpClient is null)
                return false;
            else
            {
                var restponse = await _httpClient.GetAsync(url);
                return restponse.StatusCode == System.Net.HttpStatusCode.OK;
            }            
        }*/
    }
}
