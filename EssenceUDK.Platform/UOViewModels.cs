﻿using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Windows.Input;
using EssenceUDK.Platform.DataTypes;

namespace EssenceUDK.Platform
{
    public sealed class ModelItemData : UOBaseViewModel, IItemTile, IItemData
    {
        #region Declarations
        private readonly IItemTile _data;
        #endregion

        #region Properties

        public override uint EntryId { get { return _data == null ? 0 : _data.EntryId; } set { _data.EntryId = value; RaisePropertyChanged(() => EntryId); } }

        public bool IsValid { get { return _data != null && _data.IsValid; } }

        public ISurface Surface { get { return _data==null? null:_data.Surface; } set { _data.Surface = value; RaisePropertyChanged(() => Surface); } }

        public string Name { get { return _data == null ? null : _data.Name; } set { _data.Name = value; RaisePropertyChanged(() => Name); } }

        public TileFlag Flags { get { return _data == null ? (TileFlag) 0 : _data.Flags; } set { _data.Flags = value; RaisePropertyChanged(() => Flags); } }

        public byte Height { get { return _data == null ? (byte)0 : _data.Height; } set { _data.Height = value; RaisePropertyChanged(() => Height); } }

        public byte Quality { get { return _data == null ? (byte)0 : _data.Quality; } set { _data.Quality = value; RaisePropertyChanged(() => Quality); } }

        public byte Quantity { get { return _data == null ? (byte)0 : _data.Quantity; } set { _data.Quantity = value; RaisePropertyChanged(() => Quantity); } }

        public ushort Animation { get { return _data == null ? (byte)0 : _data.Animation; } set { _data.Animation = value; RaisePropertyChanged(() => Animation); } }

        public byte StackingOff { get { return _data == null ? (byte)0 : _data.StackingOff; } set { _data.StackingOff = value; RaisePropertyChanged(() => StackingOff); } }
        
        #endregion

        #region Ctor
        public ModelItemData(IItemTile data)
        {
            _data = data;
        }
        #endregion
    }

    public sealed class ModelLandData : UOBaseViewModel, ILandTile, ILandData
    {
        #region Declarations
        private readonly ILandTile _data;
        #endregion

        #region Properties

        public override uint EntryId { get { return _data.EntryId; } set { _data.EntryId = value; RaisePropertyChanged(() => EntryId); } }

        public bool IsValid { get { return _data.IsValid; } }

        public ISurface Surface { get { return _data.Surface; } set { _data.Surface = value; RaisePropertyChanged(() => Surface); } }

        public ISurface Texture { get { return _data.Texture; } set { _data.Texture = value; RaisePropertyChanged(() => Texture); } }

        public string Name { get { return _data.Name; } set { _data.Name = value; RaisePropertyChanged(() => Name); } }

        public TileFlag Flags { get { return _data.Flags; } set { _data.Flags = value; RaisePropertyChanged(() => Flags); } }

        public ushort TexID { get { return _data.TexID; } set { _data.TexID = value; RaisePropertyChanged(() => TexID); } }
        
        #endregion

        #region Ctor
        public ModelLandData(ILandTile data)
        {
            _data = data;
        }
        #endregion
    }

    public sealed class ModelGumpSurf : UOBaseViewModel, IGumpEntry
    {
        #region Declarations
        private readonly IGumpEntry _data;
        #endregion

        #region Properties

        public override uint EntryId { get { return _data.EntryId; } set { _data.EntryId = value; RaisePropertyChanged(() => EntryId); } }

        public bool IsValid { get { return _data.IsValid; } }

        public ISurface Surface { get { return _data.Surface; } set { _data.Surface = value; RaisePropertyChanged(() => Surface); } }
        
        #endregion

        #region Ctor
        public ModelGumpSurf(IGumpEntry data)
        {
            _data = data;
        }
        #endregion
    }

    #region Model Helpers
    
    public abstract class UOBaseViewModel : NotificationUOObject
    {
        public virtual uint EntryId { get; set; }
    }

    public abstract class DelegateUOCommand : ICommand
    {
        #region Declarations
        private readonly Action<object> _command;
        private readonly Func<object, bool> _canExecute;
        private InputBindingCollection _inputBinding;
        #endregion

        #region Ctor
        /// <summary>
        /// Constructor. Initializes delegate command with Execute delegate and CanExecute delegate
        /// </summary>
        /// <param name="command">Delegate to execute when Execute is called on the command.  This can be null to just hook up a CanExecute delegate.</param>
        /// <param name="canExecute">Delegate to execute when CanExecute is called on the command.  This can be null.</param>
        public DelegateUOCommand(Action<object> command, Func<object, bool> canExecute = null)
        {
            if (command == null)
                throw new ArgumentNullException();
            _canExecute = canExecute;
            _command = command;
        }
        #endregion

        #region Props
        /// <summary>
        /// Command's associated input bindings
        /// </summary>
        public InputBindingCollection InputBindings
        {
            get
            {
                return _inputBinding;
            }
        }
        #endregion 


        #region Methods
        /// <summary>
        /// Adds a new gesture to associate inputbindings
        /// </summary>
        public void AddGesture(InputGesture gesture)
        {
            if (_inputBinding == null) _inputBinding = new InputBindingCollection();
            _inputBinding.Add(new InputBinding(this, gesture));
        }

        ///<summary>
        ///Defines the method to be called when the command is invoked.
        ///</summary>
        ///<param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
        public void Execute(object parameter)
        {
            if (_command == null) return;
            _command(parameter);
        }

        ///<summary>
        ///Defines the method that determines whether the command can execute in its current state.
        ///</summary>
        ///<param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
        ///<returns>
        ///true if this command can be executed; otherwise, false.
        ///</returns>
        public bool CanExecute(object parameter)
        {
            if (_canExecute == null) return true;
            return _canExecute(parameter);
        }

        /// <summary>
        /// Raises <see cref="CanExecuteChanged"/> so every command invoker can requery to check if the command can execute.
        /// <remarks>Note that this will trigger the execution of <see cref="CanExecute"/> once for each invoker.</remarks>
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested += value;
            }
        }
        #endregion
    }

    public abstract class NotificationUOObject : INotifyPropertyChanged
    {
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

        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }

    #endregion
}