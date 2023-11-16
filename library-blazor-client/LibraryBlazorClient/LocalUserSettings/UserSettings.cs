using System.ComponentModel;
using System.Runtime.CompilerServices;

// https://www.meziantou.net/storing-user-settings-in-a-blazor-webassembly-application.htm

namespace LibraryBlazorClient.LocalUserSettings
{
    public class UserSettings : INotifyPropertyChanged
    {
        private string username = string.Empty;
        public string Username
        {
            get => username; 
            set
            {
                username = value;
                RaisePropertyChanged();
            }
        }

        private string userRole = string.Empty;
        public string UserRole
        {
            get => userRole;
            set
            {
                userRole = value;
                RaisePropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
