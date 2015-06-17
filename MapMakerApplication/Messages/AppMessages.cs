using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight.Messaging;

namespace MapMakerApplication.Messages
{
    /// <summary>
    /// class that defines all messages used in this application
    /// </summary>
    public static class AppMessages
    {

        public static class PleaseConfirmMessage
        {
            public static void Send(DialogMessage dialogMessage)
            {
                Messenger.Default.Send(dialogMessage);
            }

            public static void Register(object recipient, Action<DialogMessage> action)
            {
                Messenger.Default.Register(recipient,action);
            }
        }

        public static class DialogRequest
        {
            public static void Send(MessageDialogRequest dialogMessage)
            {
                Messenger.Default.Send(dialogMessage);
            }

            public static void Register(object recipient, Action<MessageDialogRequest> action)
            {
                Messenger.Default.Register(recipient, action);
            }
        }

        public static class DialogAnwer
        {
            public static void Send(MessageDialogResult dialogMessage)
            {
                Messenger.Default.Send(dialogMessage);
            }

            public static void Register(object recipient, Action<MessageDialogResult> action)
            {
                Messenger.Default.Register(recipient, action);
            }
        }


        public static class OptionAnswer
        {
            public static void Send(OptionMessage dialogMessage)
            {
                Messenger.Default.Send(dialogMessage);
            }

            public static void Register(object recipient, Action<OptionMessage> action)
            {
                Messenger.Default.Register(recipient, action);
            }
        }

        public static class MapGeneratorMessage
        {
            public static void Send(MapMakeMessage dialogMessage)
            {
                Messenger.Default.Send(dialogMessage);
            }

            public static void Register(object recipient, Action<MapMakeMessage> action)
            {
                Messenger.Default.Register(recipient, action);
            }
        }

		public static class MapAltitudeExtractor
			{
			public static void Send( MapAltitudeExport dialogMessage )
				{
				Messenger.Default.Send(dialogMessage);
				}

			public static void Register( object recipient, Action<MapAltitudeExport> action )
				{
				Messenger.Default.Register(recipient, action);
				}
			}

    }

}
