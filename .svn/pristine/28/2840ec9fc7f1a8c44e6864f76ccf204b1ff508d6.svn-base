using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using EssenceUDK.Platform;

namespace EssenceUDK
{
	/// <summary>
	/// Interaction logic for PreferencesNew.xaml
	/// </summary>
	public partial class PreferencesNew : UserControl
	{
		public PreferencesNew()
		{
			this.InitializeComponent();

            var clientinfo = Platform.UtilHelpers.ClientInfo.Get("C:\\UltimaOnline\\__client\\client.exe");
            if (clientinfo != null)
            {
                var ClientVersion = clientinfo.DetectDataVersion();
                ClientVersion &= UODataTypeVersion._DataTypeMask;
                var ClientData = clientinfo.DetectDataFeautures();
                ClientData &= UODataTypeOptions._DataTypeMask;
                var a = 10;
                a += 1;
            }
		}
	}
}