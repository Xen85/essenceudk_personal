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
using Microsoft.VisualStudio.Shell;

namespace Company.VisualStudioCommandMaker
{
  

    namespace CommandTest
    {
        public class CloneProperties //: VisualCommanderExt.ICommand
        {
            public void Run(EnvDTE80.DTE2 DTE, Microsoft.VisualStudio.Shell.Package package)
            {
                EnvDTE.TextSelection ts = DTE.ActiveWindow.Selection as EnvDTE.TextSelection;
                if (ts == null)
                    return;
                var codeClass = ts.ActivePoint.CodeElement[vsCMElement.vsCMElementClass] as CodeClass;
                if (codeClass == null)
                    return;
                EnvDTE.Project project = DTE.ActiveWindow.Project;
                var resolutor = GetTypeByName(DTE, package, codeClass.FullName);
                var type = Type.GetType(codeClass.FullName, true);
                var bases = new List<Type>();
                var baseType = type.BaseType;
                while (baseType != typeof(object) && baseType != null)
                {
                    bases.Add(baseType);
                    baseType = baseType.BaseType;
                }
                bases.Insert(0, type);

                var fields_list = new List<PropertyInfo>();
                foreach (var type_base in bases)
                {
                    var tmp = type_base.GetProperties(BindingFlags.Public | BindingFlags.IgnoreCase | BindingFlags.Default | BindingFlags.Instance);
                    foreach (var fieldInfo in tmp)
                    {

                        fields_list.Add(fieldInfo);
                    }
                }
                var text = "//numero di fields = ' " + fields_list.Count + " '" + System.Environment.NewLine;
                foreach (var fieldInfo in fields_list)
                {
                    if (text.Contains("private " + fieldInfo.PropertyType + " _" + fieldInfo.Name.ToLower() + ";"))
                        continue;
                    text += "private " + fieldInfo.PropertyType + " _" + fieldInfo.Name.ToLower() + ";" + System.Environment.NewLine + System.Environment.NewLine;
                    text += "public " + fieldInfo.PropertyType + " " + fieldInfo.Name + "{get{return _" + fieldInfo.Name.ToLower() + "; }" + "set{ _" + fieldInfo.Name.ToLower() + " =value ; RaisePropertyChanged(()=>("+fieldInfo.Name+  "));}}" + System.Environment.NewLine + System.Environment.NewLine;
                }
                text = text.Replace("+", ".");
                System.Windows.Clipboard.SetText(text);
            }

            private System.Type GetTypeByName(EnvDTE80.DTE2 DTE, Microsoft.VisualStudio.Shell.Package package, string name)
            {
                System.IServiceProvider serviceProvider = package as System.IServiceProvider;
                var typeService =
                    serviceProvider.GetService(typeof(Microsoft.VisualStudio.Shell.Design.DynamicTypeService)) as
                    Microsoft.VisualStudio.Shell.Design.DynamicTypeService;

                Microsoft.VisualStudio.Shell.Interop.IVsSolution sln =
                    serviceProvider.GetService(typeof(Microsoft.VisualStudio.Shell.Interop.IVsSolution)) as
                    Microsoft.VisualStudio.Shell.Interop.IVsSolution;

                Microsoft.VisualStudio.Shell.Interop.IVsHierarchy hier;
                sln.GetProjectOfUniqueName(DTE.ActiveDocument.ProjectItem.ContainingProject.UniqueName, out hier);

                return typeService.GetTypeResolutionService(hier).GetType(name, true);
            }
        }
    }
}
