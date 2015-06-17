using System;
using EssenceUDK.Platform;
using EssenceUDK.Platform.UtilHelpers;

namespace EssenceUDK.Model
{
    public class DataService : IDataService
    {
        public void GetData(Action<DataItem, Exception> callback)
        {
            // Use this to connect to the actual data service

            var item = new DataItem("Welcome to MVVM Light");
            

            callback(item, null);
        }

        public void GetData(Action<UserProfile, Exception> callback)
        {
            var UserProfile = new UserProfile
            {
                ClientData = 0,
                ClientFolder = "",
                ClientLanguage = Language.English,
                ClientVersion = UODataTypeVersion.LegacyClassic,
                InterfaceLanguage = Language.English,
                ServerAddress = "",
                ServerPassword = "",
                ServerPort = 0,
                ServerUsername = "",
                UserProfileName = ""
            };
            callback(UserProfile, null);

        }
    }
}