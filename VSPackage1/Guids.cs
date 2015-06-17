// Guids.cs
// MUST match guids.h
using System;
using System.Security.Permissions;
using EnvDTE;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell.Design;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace CommandTest
	{
	
	}
namespace Company.VSPackage1
{
    static class GuidList
    {
        public const string guidVSPackage1PkgString = "1e519b4a-83c8-46f9-aeee-020400f14073";
        public const string guidVSPackage1CmdSetString = "506405a1-3d2c-45f0-8538-334053c0a5ea";

        public static readonly Guid guidVSPackage1CmdSet = new Guid(guidVSPackage1CmdSetString);
    };

}