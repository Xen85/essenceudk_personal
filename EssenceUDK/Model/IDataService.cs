using System;

namespace EssenceUDK.Model
{
    public interface IDataService
    {
        void GetData(Action<DataItem, Exception> callback);
        void GetData(Action<UserProfile, Exception> callback);
    }
}
