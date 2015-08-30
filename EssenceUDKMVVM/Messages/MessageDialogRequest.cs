using GalaSoft.MvvmLight.Messaging;

namespace MapMakerApplication.Messages
{
    public class MessageDialogRequest : MessageBase
    {
        public string Content { get; set; }
        public object sender { get; set; }

        public MessageDialogRequest()
            : base()
        {
        }

        public MessageDialogRequest(string content)
            : base()
        {
            Content = content;
        }
    }

    public class MessageDialogResult : MessageBase
    {
        public string Content { get; set; }
        public object sender { get; set; }
        public DialogType Type { get; set; }

        public MessageDialogResult()
            : base()
        {
        }

        public MessageDialogResult(string content)
            : base()
        {
            Content = content;
        }
    }

    public enum DialogType
    {
        OpenFolder,
        SaveAco,
        SaveFile,
        OpenFile,
        SelectBitmapMap,
        SelectBitmapZ,
        OpenOptionFolder,
        OpenOptionOutputFolder,
        SaveBrushFile
    }
}