using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Authentication.Client.Shared
{
    public partial class AuthenticationLayout
    {
        private bool _drawerOpen = true;
        private MudTheme? _mudTheme = null;
        private MudTheme? _darkMudTheme=null;
        private MudTheme? _currentTheme = null;


        [Inject] public ILocalStorageService? LocalStorage { get; set; }

        protected override Task OnInitializedAsync()
        {
            SetDarkTheme();
            _currentTheme = _darkMudTheme;
            return base.OnInitializedAsync();
        }

        private void ToggleDrawer()
        {
            _drawerOpen = !_drawerOpen;            
        }

        private void SetDarkTheme()
        {
            _darkMudTheme = new MudTheme()
            {
                Palette = new PaletteDark
                {
                    AppbarBackground = "#4d8e66",
                    AppbarText = "#e8fffe",
                    Primary = "#005631",
                    Secondary = "#010545",
                    TextPrimary = "#f0f0ff",
                    TextSecondary = "#ffeffc",
                    DrawerText = "#feffd9",
                    Background = "#2a5289",
                    BackgroundGrey = "#68686a",
                    DrawerBackground = "#656c45",
                    Surface = "#156ca6",
                    ActionDefault="#FFFFFF",
                    ActionDisabled="#000000",
                    TextDisabled="#FFFFFF"
                }
            };
        }
    }
}
