﻿using System;
using System.IO;
using System.Threading;
using System.Reflection;

namespace EssenceUDK.Platform.UtilHelpers
{
	public static class DynamicExecutor 
    {
	    public static string ExecutablePath {
            get { return _ExecutablePath ?? (_ExecutablePath = Assembly.GetExecutingAssembly().Location); }
	    }
        private static string _ExecutablePath = null;

        public static string ApplicationDir {
            get { return _ApplicationDir ?? (_ApplicationDir = Path.GetDirectoryName(ExecutablePath)); }
        }
        private static string _ApplicationDir = null;

        public static T CreateInstance<T>()
        {
            return (T)Activator.CreateInstance(typeof(T));
        }

        public static object CreateInstance(Type type)
        {
            return Activator.CreateInstance(type);
        }

        public static void SetStaticProperty(Type obj_type, string property_name, object new_value)
        {
            if (obj_type == null)
                throw new ArgumentNullException("obj_type");
            PropertyInfo property = obj_type.GetProperty(property_name, BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
            if (property == null)
                throw new InvalidOperationException(string.Format("Property '{0}' not found in class '{1}.", property_name, obj_type));
            property.SetValue(null, new_value, null);
        }

        //public static void SetProperty(Control control, object obj, string property_name, object new_value)
        //{
        //    if (control.InvokeRequired)
        //    {
        //        control.Invoke((ThreadStart)delegate
        //        {
        //            SetProperty(obj, property_name, new_value);
        //        });
        //    }
        //    else
        //    {
        //        SetProperty(obj, property_name, new_value);
        //    }
        //}

        public static void SetProperty(object obj, string property_name, object new_value)
        {
            if (obj == null)
                throw new ArgumentNullException("obj");
            Type obj_type = obj.GetType();
            PropertyInfo property = obj_type.GetProperty(property_name, BindingFlags.Public | BindingFlags.Instance);
            if (property == null)
                throw new InvalidOperationException(string.Format("Property '{0}' not found in class '{1}.", property_name, obj_type));
            property.SetValue(obj, new_value, null);
        }

        public static object GetStaticProperty(Type obj_type, string property_name)
        {
            if (obj_type == null)
                throw new ArgumentNullException("obj_type");
            PropertyInfo property = obj_type.GetProperty(property_name, BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
            if (property == null)
                throw new InvalidOperationException(string.Format("Property '{0}' not found in class '{1}.", property_name, obj_type));
            return property.GetValue(null, null);
        }

        //public static object GetProperty(Control control, object obj, string property_name)
        //{
        //    if (control.InvokeRequired)
        //    {
        //        object result = null;
        //        control.Invoke((ThreadStart)delegate
        //        {
        //            result = GetProperty(obj, property_name);
        //        });
        //        return result;
        //    }
        //    else
        //    {
        //        return GetProperty(obj, property_name);
        //    }
        //}

        //public static T GetProperty<T>(Control control, object obj, string property_name)
        //{
        //    if (control.InvokeRequired)
        //    {
        //        T result = default(T);
        //        control.Invoke((ThreadStart)delegate
        //        {
        //            result = GetProperty<T>(obj, property_name);
        //        });
        //        return result;
        //    }
        //    else
        //    {
        //        return GetProperty<T>(obj, property_name);
        //    }
        //}

        public static object GetProperty(object obj, string property_name)
        {
            if (obj == null)
                throw new ArgumentNullException("obj");
            Type obj_type = obj.GetType();
            PropertyInfo property = obj_type.GetProperty(property_name, BindingFlags.Public | BindingFlags.Instance);
            if (property == null)
                throw new InvalidOperationException(string.Format("Property '{0}' not found in class '{1}.", property_name, obj_type));
            return property.GetValue(obj, null);
        }

        public static T GetProperty<T>(object obj, string property_name)
        {
            return (T)GetProperty(obj, property_name);
        }

        //public static void InvokeMethod(Control control, object obj, string method_name, params object[] args)
        //{
        //    if (control.InvokeRequired)
        //    {
        //        control.Invoke((ThreadStart)delegate
        //        {
        //            InvokeMethod(obj, method_name, args);
        //        });
        //    }
        //    else
        //    {
        //        InvokeMethod(obj, method_name, args);
        //    }
        //}

        //public static void InvokeMethod(object obj, string method_name, params object[] args)
        //{
        //    if (obj == null)
        //        throw new ArgumentNullException("obj");
        //    Type obj_type = obj.GetType();
        //    Type[] arg_types;
        //    if (args != null)
        //        arg_types = Array.ConvertAll<object, Type>(args, delegate(object arg)
        //        {
        //            return arg != null ? arg.GetType() : typeof(void);
        //        });
        //    else
        //        arg_types = Type.EmptyTypes;



        //    MethodInfo method = obj_type.GetMethod(method_name, BindingFlags.Public | BindingFlags.Instance, null, arg_types, null);
        //    if (method == null)
        //        throw new InvalidOperationException(string.Format("Method '{0}' not found in class '{1}.", method_name, obj_type));
        //    method.Invoke(obj, args);
        //}

        public static object InvokeMethod(object obj, string method_name, params object[] args)
        {
            return InvokeMethod(obj, null, method_name, args);
        }

        public static object InvokeMethod(object obj, Type result, string method_name, params object[] args)
        {
            return InvokeMethod(obj, null, result, method_name, args);
        }

        public static object InvokeMethod(object obj, Type type, Type result, string method_name, params object[] args)
        {
            if (obj == null)
                throw new ArgumentNullException("obj");
            Type obj_type = type ?? obj.GetType();

            object res = result != null ? CreateInstance(result) : new object();

            res = obj_type.InvokeMember(method_name, System.Reflection.BindingFlags.InvokeMethod, null, obj, args);

            //             if(result != null)
            //                 return (result)res;
            //             else
            return res;
        }

        public static T InvokeMethod<T>(object obj, Type type, string method_name, params object[] args)
        {
            return (T)InvokeMethod(obj, type, typeof(T), method_name, args);
        }

    }

}
