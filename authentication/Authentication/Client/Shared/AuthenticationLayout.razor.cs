using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Authentication.Client.Shared
{
    public partial class AuthenticationLayout
    {
        private bool _drawerOpen = true;
        private MudTheme? _darkMudTheme=null;
        private MudTheme? _lightMudTheme = null;
        private MudTheme? _currentTheme = null;
        private bool _isLightTheme = true;


        [Inject] public ILocalStorageService? LocalStorage { get; set; }

        protected override async Task OnInitializedAsync()
        {
            string themeName = "light";
            if ((LocalStorage is not null) && (await LocalStorage.ContainKeyAsync("theme")))
            {
                themeName = await LocalStorage.GetItemAsStringAsync("theme");
            }
            _isLightTheme = themeName == "light" ? true : false;

            SetTheme();
            await base.OnInitializedAsync();
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

        private void SetLightTheme()
        {
            _lightMudTheme = new MudTheme()
            {
                Palette = new PaletteLight
                {
                    AppbarBackground = "#cdcec6",
                    AppbarText = "#f8fffe",
                    Primary = "#e0e6e1",
                    Secondary = "#b0b0b5",
                    TextPrimary = "#101011",
                    TextSecondary = "#112113",
                    DrawerText = "#feffd9",
                    Background = "#2a5289",
                    BackgroundGrey = "#68686a",
                    DrawerBackground = "#656c45",
                    Surface = "#156ca6",
                    ActionDefault = "#FFFFFF",
                    ActionDisabled = "#000000",
                    TextDisabled = "#FFFFFF"
                }
            };
        }

        private async Task ChangeThemeAsync()
        {
            _isLightTheme = !_isLightTheme;
            SetTheme();

            if (LocalStorage is not null)
            {
                await LocalStorage.SetItemAsStringAsync("theme", GetThemeName());
            }
        }

        private void SetTheme()
        {
            if (_lightMudTheme is null) 
            {
                SetLightTheme();
            }
            if (_darkMudTheme is null)
            {
                SetDarkTheme();
            }
            
            if (_isLightTheme)
            {
                _currentTheme = _lightMudTheme;
            }
            else
            {
                _currentTheme = _darkMudTheme;
            }
        }

        private string GetThemeName()
        {
            if (_isLightTheme)
                return "light";
            else
                return "dark";
        }
    }
}
