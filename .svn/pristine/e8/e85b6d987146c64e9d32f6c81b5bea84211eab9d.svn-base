using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Media;

namespace EssenceUDK.MapMaker.TextFileReading
{
    public class Factory
    {
        #region fields
        protected readonly List<string> Strings;
        #endregion

        public static char[] separator = { '\t', ' ' };

        public Color ReadColorFromInt(int number)
        {
            var bytes = BitConverter.GetBytes(number);
            Array.Reverse(bytes);
            return Color.FromArgb(byte.MaxValue, bytes[1], bytes[2], bytes[3]);
        }
        public Color ReadColorFromInt(String number)
        {
            var oldcolor =  System.Drawing.ColorTranslator.FromHtml(number.Replace("0x", "#"));
            return new Color(){A=oldcolor.A,B =oldcolor.B, R=oldcolor.R,G=oldcolor.G};
        }
        
        public Factory(string location)
        {
            Strings = File.ReadAllLines(location).ToList();
        }

        public virtual void Read()
        {
            
        }
    }
}
