/***************************************************************************
 *                                               Created by :        StaticZ
 *                   IniFile.cs                  UO Quintessense server team
 *              ____________________             url   :   http://uoquint.ru
 *              Version : 14/01/2011             email :    admin@uoquint.ru 
 *                                               ---------------------------
 *
 ***************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace EssenceUDK.Platform.DataTypes.FileFormat
{
    public sealed class IniFile
    {
        [DllImport("Kernel32", CharSet = CharSet.Unicode)]
        private static extern Int32 GetPrivateProfileInt(string appName, string keyName, Int32 valDefault, string filePath);

        [DllImport("Kernel32", CharSet = CharSet.Unicode)]
        private static extern UInt32 GetPrivateProfileString(string appName, string keyName, string valDefault, StringBuilder valReturn, UInt32 size, string filePath);

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool WritePrivateProfileString(string lpAppName, string lpKeyName, string lpString, string lpFileName);

        public string ConfigPath { get; private set; }

        public IniFile(string config)
        {
            ConfigPath = config;
        }

        public bool ReadBool(string appName, string keyName, bool valDefault = false, bool write = true)
        {
            int getint = GetPrivateProfileInt(appName, keyName, valDefault ? 1 : 0, ConfigPath);
            bool result = !(getint <= 0);
            
            if (write && result == valDefault)
                WriteBool(appName, keyName, valDefault);
            
            return result;
        }

        public int ReadInt(string appName, string keyName, int valDefault=0, bool write = true)
        {
            int result = GetPrivateProfileInt(appName, keyName, valDefault, ConfigPath);
            
            if (write && result == valDefault)
                WriteInt(appName, keyName, valDefault);
            
            return result;
        }

        public string ReadString(string appName, string keyName, string valDefault = "", bool write = true)
        {
            StringBuilder getstr = new StringBuilder(0x4000);
            GetPrivateProfileString(appName, keyName, valDefault, getstr, 0x4000, ConfigPath);
            string result = getstr.ToString();
            if (String.IsNullOrEmpty(result))
                result = valDefault;

            if (write && result == valDefault)
                WriteString(appName, keyName, valDefault);

            return result;
        }

        public bool WriteBool(string appName, string keyName, bool val)
        {
            bool result = WritePrivateProfileString(appName, keyName, val ? "1" : "0", ConfigPath);
            return result;
        }

        public bool WriteInt(string appName, string keyName, int val)
        {
            bool result = WritePrivateProfileString(appName, keyName, String.Format("{0}", val), ConfigPath);
            return result;
        }

        public bool WriteString(string appName, string keyName, string val)
        {
            bool result = WritePrivateProfileString(appName, keyName, String.Format("\"{0}\"", val), ConfigPath);
            return result;
        }   
    }
}   
