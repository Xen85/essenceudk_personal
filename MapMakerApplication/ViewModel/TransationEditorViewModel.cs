using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using EssenceUDK.MapMaker.Elements.Interfaces;
using EssenceUDK.MapMaker.Elements.Items.ItemsTransition;
using EssenceUDK.MapMaker.Elements.Textures.TextureTransition;
using EssenceUDK.Platform.DataTypes;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Linq;

namespace MapMakerApplication.ViewModel
{
    public class TransationEditorViewModel : ViewModelBase
    {
        #region Declarations

        private int _comboboxTransitionLineTypeSelectedIndex;
        private int _comboboxTransitionDirectionSelectedIndex;
        private ITransition _selectedTransition;
        private int _selectedKindOfTransition;
        private IEntryTile _selectedTile;
        private int _selectedTileInt;
        private string _stringTextureInt;

        #endregion //Declarations

        #region Properties

        public string TextureIntString { get { return _stringTextureInt; } set { _stringTextureInt = value; RaisePropertyChanged(()=>TextureIntString);} }

        public int ComboBoxLineTypeSelectedIndex
        {
            get { return _comboboxTransitionLineTypeSelectedIndex; }
            set
            {
                _comboboxTransitionLineTypeSelectedIndex = value;
                RaisePropertyChanged(() => ComboBoxLineTypeSelectedIndex);
                RaisePropertyChanged(() => SelectedLineTransition);
            }
        }

        public ObservableCollection<int> SelectedLineTransition
        {
            get
            {
                if (SelectedTransition == null) return null;
                if (ComboboxDirectionSelectedIndex < 0) return null;
                if (ComboBoxLineTypeSelectedIndex < 0) return null;
                return SelectedTransition.Lines[ComboBoxLineTypeSelectedIndex].List[ComboboxDirectionSelectedIndex].List;
            }
        }

        public ITransition SelectedTransition
        {
            get { return _selectedTransition; }
            set
            {
                _selectedTransition = value;
                RaisePropertyChanged(null);
            }
        }

        public int ComboboxDirectionSelectedIndex
        {
            get
            { return _comboboxTransitionDirectionSelectedIndex; }
            set
            {
                _comboboxTransitionDirectionSelectedIndex = value;
                RaisePropertyChanged(() => ComboboxDirectionSelectedIndex);
                RaisePropertyChanged(() => SelectedLineTransition);
            }
        }

        public int SelectedKindOfTransition
        {
            get { return _selectedKindOfTransition; }
            set
            {
                _selectedKindOfTransition = value;
                RaisePropertyChanged(() => SelectedKindOfTransition);
            }
        }

        public object SelectedTile
        {
            get { return _selectedTile; }
            set
            {
                _selectedTile = (IEntryTile)value;
                RaisePropertyChanged(() => SelectedTile);
            }
        }

        public object SelectedTileInt
        {
            get { return _selectedTileInt; }
            set
            {
                if (value == null)
                    _selectedTileInt = -1;
                else
                {
                    _selectedTileInt = (int)value;
                }
                RaisePropertyChanged(() => SelectedTileInt);
            }
        }

        public int IndexTextureTo
        {
            get
            {
                var transition = _selectedTransition as AreaTransitionTexture;
                if (transition != null)
                    return transition.TextureIdTo;
                var areaTransitionItem = _selectedTransition as AreaTransitionItem;
                if (areaTransitionItem != null)
                    return areaTransitionItem.TextureIdTo;
                return -1;
            }

            set
            {
                var transition = _selectedTransition as AreaTransitionTexture;
                var trainsitionItem = _selectedTransition as AreaTransitionItem;
                if (transition != null)
                    transition.TextureIdTo = value;
                else if (trainsitionItem!=null)
                ((AreaTransitionItem) _selectedTransition).TextureIdTo = value;

                RaisePropertyChanged(() => IndexTextureTo);
                RaisePropertyChanged(() => SelectedTextureName);
            }
        }

        public string SelectedTextureName
        {
            get
            {
                var areaTextures = ViewModelLocator._sdk.CollectionAreaTexture.List.FirstOrDefault(textures => textures.Index == IndexTextureTo);
                if (
                    areaTextures != null)
                    return
                        areaTextures.
                            Name;
                return "";
            }

            set
            {
                var name = value;
                var firstOrDefault = ViewModelLocator._sdk.CollectionAreaTexture.List.FirstOrDefault(o => o.Name == name);
                if (firstOrDefault != null)
                {
                    var texture = firstOrDefault.Index;
                    IndexTextureTo = texture;
                }
            }
        }

        #endregion

        #region Ctor
        public TransationEditorViewModel()
        {

            #region Commands
            TransitionAdd = new RelayCommand(TransitionAddExecuted, TransitionCanAdd);
            TransitionRemove = new RelayCommand(TransitionRemoveExecuted, TransitionCanExecuteRemove);

            TileTransitionAdd = new RelayCommand(TileAddExecuted, TileAddCanExecute);
            TextureTransitionTileRemove = new RelayCommand(TileRemoveExecuted, TileRemoveCanExecute);
            TileTransitionAddByString = new RelayCommand(()=>
                                                             {
                                                                 var value = SdkViewModel.ParseStringToInt(TextureIntString);
                                                                 SelectedLineTransition.Add(value);
                                                                 
                                                             },
                                            ()=>{
                                                if (string.IsNullOrWhiteSpace(TextureIntString))
                                                    return false;
                                                    var value = SdkViewModel.ParseStringToInt(TextureIntString);
                                                    if(value < 0)
                                                        return false;

                                                    if (SelectedKindOfTransition == 1 && ApplicationController.manager.GetItemTile().Count() < value)
                                                        return false;
                                                    if (SelectedKindOfTransition == 0 && ApplicationController.manager.GetLandTile().Count() < value)
                                                        return false;
                                                    return SelectedLineTransition != null && !SelectedLineTransition.Contains(value);
                                            });
            #endregion //Commands
        }
        #endregion

        #region Commands Properties

        public ICommand TransitionRemove { get; private set; }

        public ICommand TransitionAdd { get; private set; }

        public ICommand TextureTransitionTileRemove { get; private set; }

        public ICommand TileTransitionAdd { get; private set; }

        public ICommand TileTransitionAddByString { get; private set; }

        #endregion //Commands Properties

        #region Commands Methods

        #region Transition

        private void TransitionRemoveExecuted()
        {
            ViewModelLocator._sdk.SelectedTextures.AreaTransitionTexture.List.Remove(SelectedTransition as AreaTransitionTexture);
            ViewModelLocator._sdk.SelectedTextures.CollectionAreaItems.List.Remove(SelectedTransition as AreaTransitionItem);
        }

        private Boolean TransitionCanExecuteRemove()
        {
            return SelectedTransition != null;

        }

        private Boolean TransitionCanAdd()
        {
            return ViewModelLocator._sdk.SelectedTextures != null;
        }

        private void TransitionAddExecuted()
        {
            if (SelectedKindOfTransition == 0)
            {
                ViewModelLocator._sdk.SelectedTextures.AreaTransitionTexture.List.Add(new AreaTransitionTexture());
            }
            if (SelectedKindOfTransition == 1)
            {
                ViewModelLocator._sdk.SelectedTextures.CollectionAreaItems.List.Add(new AreaTransitionItem());
            }

        }

        #endregion //Transition

        #region Tiles

        private void TileAddExecuted()
        {
            SelectedLineTransition.Add((int)_selectedTile.EntryId);
        }
        private Boolean TileAddCanExecute()
        {
            return _selectedTile != null &&
                SelectedLineTransition != null &&
                !SelectedLineTransition.Contains((int)_selectedTile.EntryId);
        }

        private Boolean TileRemoveCanExecute()
        {
            return
                _selectedTileInt >= 0 &&
                SelectedLineTransition != null;
        }

        private void TileRemoveExecuted()
        {
            SelectedLineTransition.Remove(_selectedTileInt);
            ComboboxDirectionSelectedIndex = ComboboxDirectionSelectedIndex;
        }
        #endregion //Tiles

        #region Export Tiles

        

        #endregion

        #endregion // commands Methods


    }
}
