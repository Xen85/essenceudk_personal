using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using EssenceUDK.Resources;
using WinForms = System.Windows.Forms;

namespace EssenceUDK
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        
        //private ApplicationController _applicationController;
        public App()
        {
            AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionTrapper;
        }

        static void UnhandledExceptionTrapper(object sender, UnhandledExceptionEventArgs e)
        {
            var exc = e.ExceptionObject as Exception;
            var msg = String.Format("{0}\n\nCall Stack:\n{1}", exc.Message, exc.StackTrace);

            // WPF doesn't work correct at exception, so let's use WinForms
            //System.Windows.MessageBox.Show(null, msg, "Exception", MessageBoxButton.OKCancel, MessageBoxImage.Error, MessageBoxResult.Cancel);
            if (WinForms.MessageBox.Show(null, msg, "Essence UDK Exception", WinForms.MessageBoxButtons.RetryCancel, WinForms.MessageBoxIcon.Error, 
                WinForms.MessageBoxDefaultButton.Button2) == WinForms.DialogResult.Cancel) Environment.Exit(1);
        }
        
    }
}
