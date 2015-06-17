/***************************************************************************
 *                (c) "Essence UDK (Ultima Developper Kit)"
 *               http://dev.uoquint.ru/projects/essence-udk
 *               
 ***************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EssenceUDK.Controls.Common
{
    public class FileBrowser : Control, INotifyPropertyChanged
    {
        private TextBox _TextBox;
        private ListBox _ListBox;

        public event PropertyChangedEventHandler PropertyChanged;
        public readonly static DependencyProperty FolderProperty;

        #region DpAccessior

        public int Folder
        {
            get { return (int)GetValue(FolderProperty); }
            set {
                SetCurrentValue(FolderProperty, value);
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Folder"));
            }
        }
        
        #endregion

    }
}
