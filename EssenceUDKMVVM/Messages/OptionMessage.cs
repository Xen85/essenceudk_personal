using GalaSoft.MvvmLight.Messaging;

namespace MapMakerApplication.Messages
{
    public class OptionMessage : MessageBase
    {
        public bool Success { get; set; }
    }
}