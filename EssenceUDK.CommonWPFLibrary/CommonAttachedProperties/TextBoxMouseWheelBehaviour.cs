using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace EssenceUDK.CommonWPFLibrary.CommonAttachedProperties
{

    public static class TextBoxMouseWheelBehaviour
    {


        public static bool GetIsWheelEnabled(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsWheelEnabledProperty);
        }

        public static void SetIsWheelEnabled(DependencyObject obj, bool value)
        {
            obj.SetValue(IsWheelEnabledProperty, value);
        }

        // Using a DependencyProperty as the backing store for IsWheelEnabled.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsWheelEnabledProperty =
            DependencyProperty.RegisterAttached("IsWheelEnabled", typeof(bool), typeof(TextBoxMouseWheelBehaviour), new UIPropertyMetadata(false, OnIsWheelEnabled));



        private static void OnIsWheelEnabled(object sender, DependencyPropertyChangedEventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (tb == null) return;
            bool isEnabled = (bool)e.NewValue;
            if (isEnabled)
            {
                tb.MouseWheel += OnTextBoxOnTextChanged;
                //tb.GotFocus += OnInputTextBoxGotFocus;
                //tb.LostFocus += OnInputTextBoxLostFocus;
            }
            else
            {
                tb.MouseWheel -= OnTextBoxOnTextChanged;
                //tb.GotFocus -= OnInputTextBoxGotFocus;
                //tb.LostFocus -= OnInputTextBoxLostFocus;
            }
        }




        private static void OnTextBoxOnTextChanged(object sender, MouseWheelEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox == null) return;
            int value;
            bool res;
            if (e.Delta > 0)
            {
                res = int.TryParse(textBox.Text, out value);
                if (res)
                {
                    value++;
                }
            }
            else
            {
                res = int.TryParse(textBox.Text, out value);
                if (res)
                {
                    value--;
                }
            }
            if (res)
                textBox.Text = value.ToString();
        }
    }
}