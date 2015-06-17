using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight.Messaging;

namespace MapMakerApplication.Messages
{
    public class OptionMessage : MessageBase
    {
        public bool Success { get; set; }
    }


}
