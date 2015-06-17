using System.Windows;
using GalaSoft.MvvmLight.Threading;
using MapMakerApplication.Messages;

namespace MapMakerApplication
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static ApplicationController ApplicationController { get; set; }
        public App()
        {
            DispatcherHelper.Initialize();

            ApplicationController = new ApplicationController();
        }

        private void ApplicationDispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            if (e.Exception.InnerException != null && e.Exception.InnerException.InnerException!=null)
            AppMessages.DialogRequest.Send(new MessageDialogRequest(e.Exception.Message + '\n'+ e.Exception.InnerException.Message + '\n' + e.Exception.InnerException.InnerException.Message));
            else
            {
                AppMessages.DialogRequest.Send(e.Exception.InnerException != null
                                                   ? new MessageDialogRequest(e.Exception.Message + '\n' +
                                                                              e.Exception.InnerException.Message)
                                                   : new MessageDialogRequest(e.Exception.Message));
            }
            e.Handled = true;
        }
    }
}
