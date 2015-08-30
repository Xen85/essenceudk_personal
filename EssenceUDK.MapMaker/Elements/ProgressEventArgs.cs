using System;

namespace EssenceUDK.MapMaker.Elements
{
    public class ProgressEventArgs : EventArgs
    {
        public string PayLoad { get; set; }
        public int Progress { get; set; }
    }
}