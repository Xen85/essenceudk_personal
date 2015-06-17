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

namespace CommandTest
{
    public class C //: VisualCommanderExt.ICommand
    {
        public void Run(EnvDTE80.DTE2 DTE, Microsoft.VisualStudio.Shell.Package package)
        {

            EnvDTE.TextSelection ts = DTE.ActiveWindow.Selection as EnvDTE.TextSelection;
            if (ts == null)
                return;
            var codeClass = ts.ActivePoint.CodeElement[vsCMElement.vsCMElementClass]
                as EnvDTE.CodeClass;
            if (codeClass == null)
                return;
            EnvDTE.Project project = DTE.ActiveWindow.Project;
            var resolutor = GetResolutionService(project, Microsoft.VisualStudio.Shell.ServiceProvider.GlobalProvider);
            var type = resolutor.GetType(codeClass.FullName, true);
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
                var tmp = type_base.GetProperties();
                foreach (var fieldInfo in tmp)
                {
                    fields_list.Add(fieldInfo);
                }
            }
            var text = "//numero di fields = ' " + fields_list.Count + " '" + System.Environment.NewLine;
            foreach (var fieldInfo in fields_list)
            {
                if (text.Contains("public " + fieldInfo.PropertyType + " " + fieldInfo.Name + ";"))
                    continue;
                text += "private " + fieldInfo.PropertyType + " _" + fieldInfo.Name.ToLower() + ";" + System.Environment.NewLine + System.Environment.NewLine;
                text += "public " + fieldInfo.PropertyType + " " + fieldInfo.Name + "{get{return _"+fieldInfo.Name.ToLower()+"; }"+ "set{ _"+fieldInfo.Name.ToLower()+"=value ;}}" + System.Environment.NewLine + System.Environment.NewLine;
            }
            text = text.Replace("+", ".");
            System.Windows.Clipboard.SetText(text);
        }

        private ITypeResolutionService GetResolutionService(EnvDTE.Project project, ServiceProvider provider)
        {

            var typeService = (DynamicTypeService)provider.GetService(typeof(DynamicTypeService));

            Debug.Assert(typeService != null, "No dynamic type service registered.");


            IVsSolution sln = provider.GetService(typeof(IVsSolution)) as IVsSolution;
            IVsHierarchy hier;
            sln.GetProjectOfUniqueName(project.UniqueName, out hier);

            return typeService.GetTypeResolutionService(hier);
        }
    }
}