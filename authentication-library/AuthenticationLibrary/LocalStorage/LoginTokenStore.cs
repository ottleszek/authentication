using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using LibraryCore.Errors;

namespace AuthenticationLibrary.LocalStorage
{ 
    public class LoginTokenStore : ILoginTokenStore
    {
    
        [Inject] ILocalStorageService? LocalStorageService { get; set; }

        public LoginTokenStore(ILocalStorageService localStorageService)
        {
            LocalStorageService = localStorageService;
        }
        public async Task<ErrorStore> SaveAccessTokenAndRefreshToken(string accessJwtToken, string refreshToken)
        {
            ErrorStore errorStore= new ();
            if (LocalStorageService is null) 
            { 
                errorStore.ClearAndAddError("Bejelentkezési adatok tárolása nem lehetséges!");
            }
            else
            {
                await LocalStorageService.SetItemAsync<string>("jwt-access-token", accessJwtToken);
                await LocalStorageService.SetItemAsync<string>("refresh-token",refreshToken);                
            }
            return errorStore;
        }

        public async Task<ErrorStore> DeleteAccessTokenAndRefreshToken()
        {
            ErrorStore errorStore = new ();
            if (LocalStorageService is null)
            {
                errorStore.ClearAndAddError("Bejelentkezési adatok törlése nem lehetséges!");
            }
            else
            {
                await LocalStorageService.RemoveItemAsync("jwt-access-token");
                await LocalStorageService.RemoveItemAsync("refresh-token");
            }
            return errorStore;
        }

    }
}
