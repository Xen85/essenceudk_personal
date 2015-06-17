using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EssenceUDK.MapMaker.Elements
{
    public class ProgressEventArgs : EventArgs
    {
        public string PayLoad { get; set; }
        public int Progress { get; set; }
    }
}
