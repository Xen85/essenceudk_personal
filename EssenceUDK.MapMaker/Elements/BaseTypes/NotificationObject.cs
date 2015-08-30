using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.Windows.Media;

namespace EssenceUDK.MapMaker.Elements.BaseTypes
{
    [Serializable]
    public class NotificationObject : INotifyPropertyChanged, IDataErrorInfo, ISerializable
    {
        public const string serialization = "Serialization.";

        #region INotifyPropertyChanged

        protected void RaisePropertyChanged<T>(Expression<Func<T>> action)
        {
            var propertyName = GetPropertyName(action);
            this.RaisePropertyChanged(propertyName);
        }

        private static string GetPropertyName<T>(Expression<Func<T>> action)
        {
            var expression = (MemberExpression)action.Body;
            var propertyName = expression.Member.Name;
            return propertyName;
        }

        protected void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void EventUpdateHandler(object sender, EventArgs args)
        {
        }

        #endregion INotifyPropertyChanged

        #region IDataErrorInfo

        public virtual string this[string columnName]
        {
            get { return null; }
        }

        public virtual string Error { get; private set; }

        #endregion IDataErrorInfo

        #region ISerializable

        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
        }

        protected void Serialize<T>(Expression<Func<T>> action, SerializationInfo info)
        {
            var propertyName = GetPropertyName(action);
            var value = GetType().GetProperty(propertyName).GetValue(this, null);
            info.AddValue(propertyName, value);
        }

        protected void Serialize(Expression<Func<Color>> action, SerializationInfo info)
        {
            string name = GetPropertyName(action);
            var value = (Color)GetType().GetProperty(name).GetValue(this, null);
            info.AddValue(name + "A", value.A);
            info.AddValue(name + "B", value.B);
            info.AddValue(name + "R", value.R);
            info.AddValue(name + "G", value.G);
        }

        protected void Serialize<T>(Expression<Func<ObservableCollection<T>>> action, SerializationInfo info)
        {
            var propertyName = GetPropertyName(action);
            var value = (ObservableCollection<T>)GetType().GetProperty(propertyName).GetValue(this, null);
            ListSerialization(value, info, propertyName);
        }

        private void ListSerialization<T>(IList<T> list, SerializationInfo info, string name)
        {
            int count = 0;
            if (list != null)
                count = list.Count;

            info.AddValue(name + "Lenght", count);

            for (int i = 0; i < count; i++)
            {
                if (list != null) info.AddValue(name + "_" + i, list[i]);
            }
        }

        protected Color Deserialize(Expression<Func<Color>> action, SerializationInfo info)
        {
            string name = GetPropertyName(action);
            var a = info.GetByte(name + "A");
            var b = info.GetByte(name + "B");
            var r = info.GetByte(name + "R");
            var g = info.GetByte(name + "G");
            var color = new Color() { A = a, B = b, R = r, G = g };
            return color;
        }

        protected T Deserialize<T>(Expression<Func<T>> action, SerializationInfo info)
        {
            Type t = action.ReturnType;
            var propertyName = GetPropertyName(action);
            var k = (T)info.GetValue(propertyName, t);
            return k;
        }

        protected IList<T> Deserialize<T>(Expression<Func<ObservableCollection<T>>> action, SerializationInfo info)
        {
            var propertyName = GetPropertyName(action);
            var type = typeof(IList<T>);
            return ListDeserialization<T>(info, propertyName);
        }

        private IList<T> ListDeserialization<T>(SerializationInfo info, string name)
        {
            var lenght = info.GetUInt32(name + "Lenght");
            if (lenght == 0) return new List<T>();
            var list = new List<T>();
            Type t = typeof(T);
            for (int i = 0; i < lenght; i++)
            {
                var c = (T)info.GetValue(name + "_" + i, t);
                list.Add(c);
            }
            return list;
        }

        #endregion ISerializable
    }
}