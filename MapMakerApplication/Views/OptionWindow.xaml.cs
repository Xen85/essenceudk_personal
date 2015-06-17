using System.Windows;
using MapMakerApplication.Messages;

namespace MapMakerApplication.Views
{
	/// <summary>
	/// Interaction logic for OptionWindow.xaml
	/// </summary>
	public partial class OptionWindow : Window
	{
		public OptionWindow()
		{
			this.InitializeComponent();
            AppMessages.DialogRequest.Register(this, MessageDialogRequestHandler);
            AppMessages.OptionAnswer.Register(this, MessageOptionHandler);
            
			// Insert code required on object creation below this point.
		}
        
        private void MessageOptionHandler(OptionMessage message)
        {
            Close();
        }


        private void MessageDialogRequestHandler(MessageDialogRequest request)
        {
            var folderdialog = new System.Windows.Forms.FolderBrowserDialog();
            switch (request.Content)
            {
                case "OpenOptionFolder":
                    {
                        var result = folderdialog.ShowDialog();
                        if (result == System.Windows.Forms.DialogResult.OK)
                        {
                            AppMessages.DialogAnwer.Send(new MessageDialogResult(folderdialog.SelectedPath) { Type = DialogType.OpenOptionFolder });
                        }
                    }
                    break;
               
            }
        }
	}
}