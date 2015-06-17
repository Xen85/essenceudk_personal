using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using EssenceUDK.Platform.MiscHelper.Components.Base;
using EssenceUDK.Platform.MiscHelper.Components.Enums;
using EssenceUDK.Platform.MiscHelper.Components.Interface;

namespace EssenceUDK.Platform.MiscHelper.Components
{
    [Serializable()]
    public class TileCategory : NotificationObject, IComponent
    {

        #region Fields

        private int _index;

        private string _name;

        private ObservableCollection<TileStyle> _list;

        private TypeTile _typetile;

        #endregion  //Fields

        #region Ctor
        public TileCategory()
        {
            _name = "";
            _index = -1;
            _list = new ObservableCollection<TileStyle>();
        }

        public TileCategory(int id,TypeTile type)
            :this()
        {
            Id = id;
            TypeTile = type;
        }
        #endregion // Ctor

        #region Methods

        public Tile FindTile(uint id)
        {
            return List.Select(tileStyle => tileStyle.FindTile(id)).FirstOrDefault(tile => tile != null);
        }

        public void AddStyle(TileStyle style)
        {
            if(!List.Contains(style))
            List.Add(style);
            style.SetCategory(this);
        }

        public IEnumerable<Tile> FindByPosition(int pos)
        {
            IEnumerable<Tile> list = new List<Tile>();
            return List.Aggregate(list, (current, tileStyle) => current.Union(tileStyle.FindTileByPosition(pos)));
        }
        public IEnumerable<Tile> FindByPosition(string position)
        {
            foreach (var tileStyle in _list)
            {
                foreach (var VARIABLE in tileStyle.FindTileByPosition(position))
                {
                    yield return VARIABLE;
                }
                
            }
        }

        public IEnumerable<Tile> AllTiles()
        {
            IEnumerable<Tile> list = new List<Tile>();
            return List.Aggregate(list, (current, style) => current.Union(style.List));
        }

        public TileStyle FindStyleByName(string name)
        {
            return List.FirstOrDefault(tileStyle => name == tileStyle.Name);
        }
        
        #endregion //Methods

        #region Props
        
        public int Id { get { return _index; } set { _index = value; RaisePropertyChanged(() => Id); } }
        public string Name { get { return _name; } set { _name = value; RaisePropertyChanged(()=>Name); } }
        public ObservableCollection<TileStyle> List { get { return _list; } set { _list = value; RaisePropertyChanged(()=>List); } }
        public TypeTile TypeTile { get { return _typetile; } set { _typetile = value; RaisePropertyChanged(()=>TypeTile); } }
        
        #endregion //Props

        public object Value()
        {
            return this;
        }
    }


    
}
