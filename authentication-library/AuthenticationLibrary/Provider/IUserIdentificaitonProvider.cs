using AuthenticationLibrary.Provider.UserIdentification;

namespace AuthenticationLibrary.Provider
{
    public interface IUserIdentificaitonProvider
    {
        public bool IsLoaded { get; }
        public UserIdentificationData UserIdentificationData { get; set; }
        public Task<UserIdentificationData?> GetUserIdentificationData();
    }
}
