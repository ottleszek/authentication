
using System.Reflection.Metadata.Ecma335;

namespace Authentication.Shared.Model
{
    public class ProfilImageFileName
    {
        private bool _isEmailValid => Email != string.Empty;
        private bool _isIdValid =>Id != Guid.Empty;

        public string Email { get; set; } = string.Empty;
        public Guid Id { get; set; }
        
        public string ProfilImageTimeStamp = string.Empty;

        public string FileName => this.GetProfilImageFilelName();
        public string FileNameWithoutExtension =>this.GetProfilImageFilelNameWithoutExtension();
        public bool IsValid => _isEmailValid && _isIdValid;
    }
}
