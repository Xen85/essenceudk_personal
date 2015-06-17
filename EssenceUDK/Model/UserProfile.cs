using System;
using EssenceUDK.Platform.UtilHelpers;

namespace EssenceUDK.Model
{
    [Serializable]
    public class UserProfile
    {
        private string _theme = "ExpressionDark";

        public string ClientInfo { get; set; }

        public String UserProfileName { get; set; }

        public string ClientFolder { get; set; }

        public string ServerAddress { get; set; }

        public int ServerPort { get; set; }

        public string ServerUsername { get; set; }

        public string ServerPassword { get; set; }

        public Platform.UODataTypeOptions ClientData { get; set; }

        public Platform.UODataTypeVersion ClientVersion { get; set; }

        public Language InterfaceLanguage { get; set; }

        public string InterfaceTheme { get { return _theme; } set { _theme = value; } }

        public Language ClientLanguage { get; set; }
    }
}
