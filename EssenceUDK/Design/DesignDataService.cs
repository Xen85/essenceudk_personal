using System;
using EssenceUDK.Model;
using EssenceUDK.Platform;
using EssenceUDK.Platform.UtilHelpers;

namespace EssenceUDK.Design
{
    public class DesignDataService : IDataService
    {
        public void GetData(Action<DataItem, Exception> callback)
        {
            // Use this to create design time data

            var item = new DataItem("Welcome to MVVM Light [design]");
            callback(item, null);
        }

        public void GetData(Action<UserProfile, Exception> callback)
        {
            var userProfile = new UserProfile
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
            callback(userProfile, null);
        }

    }
}