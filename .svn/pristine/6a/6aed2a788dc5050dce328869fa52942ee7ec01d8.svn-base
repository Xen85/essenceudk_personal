using System.Windows;
using GalaSoft.MvvmLight.Messaging;
using MapMakerApplication.Messages;

namespace MapMakerApplication.Views
{
    /// <summary>
    /// Interaction logic for WindowTest.xaml
    /// </summary>
    public partial class WindowTest : Window
    {
        public WindowTest()
        {
            InitializeComponent();
           AppMessages.DialogRequest.Register(this, DialogManager);

        }

        private void DialogManager(MessageDialogRequest obj)
        {
            if (obj == null) return;
            var dialog = new Microsoft.Win32.SaveFileDialog();
            var opendialog = new Microsoft.Win32.OpenFileDialog();
            var folderdialog = new System.Windows.Forms.FolderBrowserDialog();
            switch ((obj.Content))
            {
                case "ACO":
                    {
                        dialog.DefaultExt = ".ACO";
                        dialog.Filter = "Photoshop Palette (.ACO)|*.ACO";

                        var result  = dialog.ShowDialog();
                        if(result==true)
                        {
                            AppMessages.DialogAnwer.Send(new MessageDialogResult(dialog.FileName){Type = DialogType.SaveAco});
                        }
                        
                    }
                    break;
                case "FOLDER":
                    {
                        var result = folderdialog.ShowDialog();
                        if(result== System.Windows.Forms.DialogResult.OK)
                        {
                            AppMessages.DialogAnwer.Send(new MessageDialogResult(folderdialog.SelectedPath){Type = DialogType.OpenFolder});
                        }
                    }
                    break;

                case "SAVE":
                    {
                        dialog.DefaultExt = ".xml";
                        dialog.Filter = "Save file (.xml)|*.xml";
                        var result = dialog.ShowDialog();
                        if (result == true)
                        {
                            AppMessages.DialogAnwer.Send(new MessageDialogResult(dialog.FileName) { Type = DialogType.SaveFile });
                        }
                    }
                    break;

                case "LOAD":
                    {
                        opendialog.DefaultExt = ".xml";
                        opendialog.Filter = "Save file (.xml)|*.xml";
                        var result = opendialog.ShowDialog();
                        if (result == true)
                        {
                            AppMessages.DialogAnwer.Send(new MessageDialogResult(opendialog.FileName) { Type = DialogType.OpenFile });
                        }
                    }
                    break;
                case "SelectBitmapMap":
                    {
                        opendialog.DefaultExt = ".bmp";
                        opendialog.Filter = "Bitmap file (.bmp)|*.bmp";
                        var result = opendialog.ShowDialog();
                        if (result == true)
                        {
                            AppMessages.DialogAnwer.Send(new MessageDialogResult(opendialog.FileName) { Type = DialogType.SelectBitmapMap });
                        }
                    }
                    break;
                case "SelectBitmapMapZ":
                    {
                        opendialog.DefaultExt = ".bmp";
                        opendialog.Filter = "Bitmap file (.bmp)|*.bmp";
                        var result = opendialog.ShowDialog();
                        if (result == true)
                        {
                            AppMessages.DialogAnwer.Send(new MessageDialogResult(opendialog.FileName) { Type = DialogType.SelectBitmapZ });
                        }
                    }
                    break;
                case "OpenOptionWindow":
                    {
                        new OptionWindow().Show();
                    }
                    break;
                case "OpenFolderOutput":
                    {
                        var result = folderdialog.ShowDialog();
                        if (result == System.Windows.Forms.DialogResult.OK)
                        {
                            AppMessages.DialogAnwer.Send(new MessageDialogResult(folderdialog.SelectedPath) { Type = DialogType.OpenOptionOutputFolder });
                        }
                    }
                    break;
                case "OpenFileXmlExport":
                    {

                        var result = folderdialog.ShowDialog();
                        if (result == System.Windows.Forms.DialogResult.OK)
                        {
                            AppMessages.DialogAnwer.Send(new MessageDialogResult(folderdialog.SelectedPath) { Type = DialogType.SaveBrushFile });
                        }
                        break;
                    }

                default:
                    {
                        MessageBox.Show(this, obj.Content, "Error", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
                    }
                    break;
            }
        }


    }
}
