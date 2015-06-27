using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EssenceUDK.Platform;
using EssenceUDK.Platform.UtilHelpers;

namespace EssenceUDKMVVM.Models.Model.Option
{
    public class UltimaDataPathOptionTree : OptionTreeMenu
    {

        #region Fields
        private string _path;
        private ClassicClientVersion _dataType;
        private Language _language;
        private bool _realTime;
        #endregion 


        #region ctor

        public UltimaDataPathOptionTree()
        {
            Name = "Ultima Data Options";
        }

        #endregion


        #region props

        public String Path { get { return _path; } set { _path = value; RaisePropertyChanged(() => (Path)); } }

        public ClassicClientVersion DataType { get { return _dataType; } set { _dataType = value; RaisePropertyChanged(() => (DataType)); } }

        public Language Language { get { return _language; } set { _language = value; RaisePropertyChanged(() => (Language)); } }

        public Boolean RealTime { get { return _realTime; } set { _realTime = value; RaisePropertyChanged(() => (RealTime)); } }

        #endregion




    }
}
