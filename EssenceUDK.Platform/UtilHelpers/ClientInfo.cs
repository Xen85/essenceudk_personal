using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using EssenceUDK.Resources.ResourceLib;

namespace EssenceUDK.Platform.UtilHelpers
{
    public sealed class ClientInfo : IEquatable<ClientInfo> 
    {
        public string  CompanyName      { get; private set; }
        public string  ProductName      { get; private set; }
        public Version ProductVersion   { get; private set; }
        public string  FileDescription  { get; private set; }
        public Version FileVersion      { get; private set; }
        public string  ExecutablPath    { get; private set; }
        public string  DirectoryPath    { get; private set; }

        private ClientInfo(string filepath)
        {
            ExecutablPath = filepath;
            DirectoryPath = Path.GetDirectoryName(filepath);

            var verResource = new VersionResource();
            verResource.LoadFrom(filepath);

            var strResource = verResource["StringFileInfo"].ToString();
            var strVal = strResource.Split(new[]{'\r','\n'}, StringSplitOptions.RemoveEmptyEntries).Where(t => t.TrimStart(' ').StartsWith("VALUE")).ToArray();
            foreach (var values in strVal.Select(t=>t.Split(new[]{'\"'}, StringSplitOptions.None))) {
                if (values.Length < 5) continue;
                if (values[1] == "FileDescription")  FileDescription = values[3];
                else if (values[1] == "CompanyName") CompanyName = values[3]; 
                else if (values[1] == "ProductName") ProductName = values[3];
            }

            var bitver = new byte[4];
            var strver = verResource.ProductVersion.Split('.');
            for (int i = 0; i < 4; ++i)
                bitver[i] = byte.Parse(strver[i]);
            ProductVersion = new Version(bitver[0], bitver[1], bitver[2], bitver[3]);

            strver = verResource.FileVersion.Split('.');
            for (int i = 0; i < 4; ++i)
                bitver[i] = byte.Parse(strver[i]);
            FileVersion = new Version(bitver[0], bitver[1], bitver[2], bitver[3]);
        }

        private ClientInfo()
        {
            throw new AccessViolationException();
        }

        public bool Equals(ClientInfo other)
        {
            if (Object.ReferenceEquals(other, null)) return false;
            if (Object.ReferenceEquals(this, other)) return true;
            return ExecutablPath.Equals(other.ExecutablPath);
        }

        // If Equals() returns true for a pair of objects 
        // then GetHashCode() must return the same value for these objects.

        public override int GetHashCode()
        {
            int hashExecutablPath = ExecutablPath == null ? 0 : ExecutablPath.GetHashCode();
            //int hashProductVersion = ProductVersion.GetHashCode();
            return hashExecutablPath;// ^ hashProductVersion;
        }

        public static ClientInfo Get(string path)
        {
            ClientInfo inf = null;
            if (!String.IsNullOrEmpty(path) && File.Exists(path) && Path.GetExtension(path) == ".exe") try {
                inf = new ClientInfo(path);
                if (inf.ProductName.ToLower().IndexOf("ultima") < 0) 
                    inf = null;
            } catch { inf = null; }
            return inf;
        }

        [Flags]
        public enum LookFor : ushort
        {
            None        = 0x0000,
            UOQuint     = 0x0001,
            //CentrEd     = 0x0002, //TODO: add clients from centred profiles   
            //Razor       = 0x0100, //TODO: add clients from razor profiles 
            Classic     = 0x0200,
            Evrywere    = 0xFFFF
        }

        public static string[] GetDataPath(LookFor look = LookFor.Evrywere)
        {
            var cinf = GetInSystem(look);
            return cinf.Select(i => i.DirectoryPath).Distinct().ToArray();
        }

        public static ClientInfo[] GetInFolder(string path)
        {
            var inf = new List<ClientInfo>(8);
            if (Directory.Exists(path))
                foreach (var file in Directory.GetFiles(path, "*.exe"))
                    inf.Add(Get(file));
            return inf.ToArray();
        }

        public static ClientInfo[] GetInSystem(LookFor look = LookFor.Evrywere)
        {
            var inf = new Hashtable(32);
            string dir;

            if (look.HasFlag(LookFor.UOQuint)) { // look for client for uoQuint server
                dir = Utils.GetRegistryKey<string>(@"Quintessence\Ultima Online", "InstallPath");
                if (Directory.Exists(dir))
                    foreach (var file in Directory.GetFiles(dir, "*.exe"))
                        inf[file] = inf[file] ?? Get(file);
            }
            
            if (look.HasFlag(LookFor.Classic)) { // look for osi classic clients
                foreach (var key in _KnownRegkeys)
                    foreach (var name in _KnownRegNames) {
                        dir = Utils.GetRegistryKey<string>(key, name);
                        
                        if (File.Exists(dir) && Path.GetExtension(dir) == ".exe")
                            dir = Path.GetDirectoryName(dir);

                        if (Directory.Exists(dir))
                            foreach (var file in Directory.GetFiles(dir, "*.exe"))
                                inf[file] = inf[file] ?? Get(file);
                    }
            }

            return inf.Values.OfType<ClientInfo>().Where(t => t != null).Distinct().ToArray();
        }  

        static readonly string[] _KnownRegkeys = new [] {
            @"Origin Worlds Online\Ultima Online\1.0", 
            @"Origin Worlds Online\Ultima Online Third Dawn\1.0",
            @"EA GAMES\Ultima Online Samurai Empire", 
            @"EA GAMES\Ultima Online Samurai Empire\1.0", 
            @"EA GAMES\Ultima Online Samurai Empire\1.00.0000", 
            @"EA GAMES\Ultima Online: Samurai Empire\1.0", 
            @"EA GAMES\Ultima Online: Samurai Empire\1.00.0000", 
            @"EA Games\Ultima Online: Mondain's Legacy", 
            @"EA Games\Ultima Online: Mondain's Legacy\1.0", 
            @"EA Games\Ultima Online: Mondain's Legacy\1.00.0000", 
            @"Origin Worlds Online\Ultima Online Samurai Empire BETA\2d\1.0", 
            @"Origin Worlds Online\Ultima Online Samurai Empire BETA\3d\1.0", 
            @"Origin Worlds Online\Ultima Online Samurai Empire\2d\1.0", 
            @"Origin Worlds Online\Ultima Online Samurai Empire\3d\1.0",
            @"Electronic Arts\EA Games\Ultima Online Stygian Abyss Classic",
        };

        static readonly string[] _KnownRegNames = new [] { 
            "ExePath", 
            "InstallDir",
            "Install Dir"
        };

        /// <summary>
        /// Try to detect using data type.
        /// </summary>
        /// <returns>If failed return UODataType.Inavalide, otherwise returns UODataType value.</returns>
        public UODataType DetectDataType()
        {
            var ver = ProductVersion;
            if (ver != null && CompanyName == "Electronic Arts" && FileDescription == "Ultima Online Client") {
                if (ver >= new Version(7, 0, 23,  2))
                    return UODataType.ClassicAdventuresOnHighSeasUpdated;
                if (ver >= new Version(7, 0,  8, 44))
                    return UODataType.ClassicAdventuresOnHighSeas;
                if (ver >= new Version(6, 0, 14,  3))
                    return UODataType.ClassicStygianAbyss;
                if (ver >= new Version(6, 0,  0,  0))
                    return UODataType.ClassicMondainsLegacy;
            }      

            return UODataType.Inavalide;
        }

        /// <summary>
        /// Try to detect using data type version.
        /// </summary>
        public UODataTypeVersion DetectDataVersion()
        {
            return (UODataTypeVersion)(DetectDataType() & (UODataType)UODataTypeVersion._DataTypeMask);
        }

        /// <summary>
        /// Try to detect using data type feautures.
        /// </summary>
        public UODataTypeOptions DetectDataFeautures()
        {
            return (UODataTypeOptions)(DetectDataType() & (UODataType)UODataTypeOptions._DataTypeMask);
        }
    }


    public class Revision
    {
        private int m_DayRevision;
        private int m_SecRevision;

        public int DayRevision { get { return m_DayRevision; } set { m_DayRevision = value; } }
        public int SecRevision { get { return m_SecRevision; } set { m_SecRevision = value; } }

        public string StrRevision { get { return String.Format("{0:D4}-{1:D5}", m_DayRevision, m_SecRevision); } set { this.Parse(value); } }

        private void Parse(string revision)
        {
            if (revision.Length < 10)
                return;

            string[] str = revision.Split(new char[] { '-', '.', '/', '\\', ' ' });
            if (str.Length < 2)
                return;

            int dayRevision = 0;
            int secRevision = 0;
            for (int i = str.Length - 1; i >= 0; --i)
                if (str[i].Length == 4)
                    Int32.TryParse(str[i], out dayRevision);
                else if (str[i].Length == 5)
                    Int32.TryParse(str[i], out secRevision);
                else
                    continue;

            if (dayRevision != 0 && dayRevision != 0)
            {
                m_DayRevision = dayRevision;
                m_SecRevision = secRevision;
            }
        }

        public Revision(int dayRevision, int secRevision)
        {
            m_DayRevision = dayRevision;
            m_SecRevision = secRevision;
        }

        public Revision(string revision)
        {
            Parse(revision);
        }

        public Revision(DateTime dateTime)
        {
            m_DayRevision = (int)Math.Floor((dateTime - new DateTime(2000, 1, 1, 0, 0, 0)).TotalDays);
            m_SecRevision = (int)Math.Floor((dateTime - new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 0, 0, 0)).TotalSeconds / 2);
        }

        public static Revision Now { get { return new Revision(DateTime.Now); } }

        /// <summary>
        /// Compares an instance to the class with the object Version version
        /// </Summary>
        /// <param Name="version"> Version object which will be conducted comparing </param>
        /// <returns> Returns 1 if the older version, -1 in case the newer version, and 0 in the case when the same version; </returns>
        public int Compare(Revision version)
        {
            int dayRevDelta = m_DayRevision - version.DayRevision;
            int secRevDelta = m_SecRevision - version.SecRevision;

            if (dayRevDelta > 0)
                return 1;
            else if (dayRevDelta < 0)
                return -1;
            else if (secRevDelta > 0)
                return 1;
            else if (secRevDelta < 0)
                return -1;
            else
                return 0;
        }
    }

}
