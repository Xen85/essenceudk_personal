using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using EssenceUDK.Platform.DataTypes;
using EssenceUDK.Platform.MiscHelper.Components;
using EssenceUDK.Platform.MiscHelper.Components.Base;
using EssenceUDK.Platform.MiscHelper.Components.Interface;
using EssenceUDK.Platform.MiscHelper.Components.Tiles;

namespace EssenceUDK.Platform.MiscHelper.TileInfo.Components.MultiStruct
{
    [Serializable]
    public class MultiCollection : NotificationObject
    {

        #region Fields

        private ObservableCollection<MultiTile> _multiTiles;
        private ObservableCollection<TileCategory> _categories;
        private ObservableCollection<Tile> _candidates;
        private readonly UODataManager _tileData;
        private string _text;
        #endregion

        #region Props

        public ObservableCollection<MultiTile> MultiTiles { get { return _multiTiles; } set { _multiTiles = value; RaisePropertyChanged(()=>MultiTiles);} }
        
        public ObservableCollection<TileCategory> Categories { get { return _categories; } set { _categories = value; RaisePropertyChanged(()=>_categories); } }
        
        public ObservableCollection<Tile> Candidates { get { return _candidates; } set { _candidates = value; RaisePropertyChanged(()=>Candidates); } }

        public String Text { get { return ToString(); } }

        #endregion

        #region Ctor

        public MultiCollection(UODataManager tileData)
        {
            this._tileData = tileData;
            _multiTiles = new ObservableCollection<MultiTile>();
            _categories = new ObservableCollection<TileCategory>();
        }
        
        #endregion

        #region methods

        public void AddTile(uint id, int x, int y, int z,int flag)
        {
            var multi = new MultiTile() {Id = id, X = x, Y = y, Z = z,Flag=flag};
           
            SelectTileforMultiTile(multi);
            MultiTiles.Add(multi);
        }

        #region Method about substituction stuff
        public void MassSubstitue(TileCategory categoryContained, TileCategory categoryOutside)
        {
            var oldTiles = MultiTiles.Select(tile => categoryContained.FindTile(tile.Id)).Where(t => t != null);

            foreach (var oldtile in oldTiles)
            {
                var oldIdDescription = _tileData.GetItemTile(oldtile.Id);
                var tiles = categoryOutside.FindByPosition(oldtile.PositionString);
                var tileDataTiles =
                    tiles.Where(
                        tile =>
                        _tileData.GetItemTile(tile.Id).Height == oldIdDescription.Height &&
                        oldIdDescription.Flags.HasFlag(TileFlag.Window) ==
                        _tileData.GetItemTile(tile.Id).Flags.HasFlag(TileFlag.Window) &&
                        oldIdDescription.Flags.HasFlag(TileFlag.Wall) ==
                        _tileData.GetItemTile(tile.Id).Flags.HasFlag(TileFlag.Wall) &&
                        oldIdDescription.Flags.HasFlag(TileFlag.Roof) ==
                        _tileData.GetItemTile(tile.Id).Flags.HasFlag(TileFlag.Roof) &&
                        oldIdDescription.Flags.HasFlag(TileFlag.Surface) ==
                        _tileData.GetItemTile(tile.Id).Flags.HasFlag(TileFlag.Surface));
                var tmp = tileDataTiles.FirstOrDefault();
                if(tmp!= null)
                MassSet(oldtile.Id,tmp);
            }
            Categories.Remove(categoryContained);
            Categories.Add(categoryOutside);
            UpdateCategories();

            RaisePropertyChanged(() => Categories);
            RaisePropertyChanged(() => MultiTiles);
            RaisePropertyChanged(()=>Text);

        }

        public void MassRemove(TileCategory categoryToRemove)
        {
            var tiletoremove = new List<MultiTile>(MultiTiles.Where(t => categoryToRemove.FindTile(t.Id) != null));
            foreach (var multiTile in tiletoremove)
            {
                _multiTiles.Remove(multiTile);
            }
            RaisePropertyChanged(()=>Text);
            UpdateCategories();
        }

        private void SelectTileforMultiTile(MultiTile multitile)
        {
            var found = false;

            if (Categories.Any(c => c.FindTile(multitile.Id) != null))
                return;

            foreach (var cats in TilesCategorySDKModule.Categories)
            {
                foreach (var tileCategory in cats.Where(tileCategory => tileCategory.FindTile(multitile.Id) != null))
                {
                    var tile = tileCategory.FindTile(multitile.Id);

                    var category = Categories.FirstOrDefault(c => c.Name == tileCategory.Name) ??
                                   new TileCategory() { Name = tileCategory.Name, Id = tileCategory.Id };

                    var style = category.List.FirstOrDefault(st => st.Name == tile.GetStyle().Name)
                        ??
                        new TileStyle() { Name = tile.GetStyle().Name, Id = tile.GetStyle().Id };

                    style.List.Add(tile);


                    if (!category.List.Contains(style))
                        category.AddStyle(style);

                    if (!Categories.Contains(category))
                        Categories.Add(category);
                    found = true;
                }
            }
            if (found == false)
            {
                if (Categories.First().Name != "Unfound")
                {
                    Categories.Insert(0, new TileCategory() { Name = "Unfound" });
                }

                if (Categories.First().FindTile(multitile.Id) != null)
                    return;
                if (_tileData.GetItemTile(multitile.Id).Flags.HasFlag(TileFlag.Roof))
                {
                    DynamicSelectionTile("Roof", multitile);
                    return;
                }
                if (_tileData.GetItemTile(multitile.Id).Flags.HasFlag(TileFlag.Surface))
                {
                    DynamicSelectionTile("Floor", multitile);
                    return;
                }
                if (_tileData.GetItemTile(multitile.Id).Flags.HasFlag(TileFlag.Wall))
                {
                    DynamicSelectionTile("Wall", multitile);
                    return;
                }
                DynamicSelectionTile("Various", multitile);
            }
            
                            

        }

        private void DynamicSelectionTile(string flag, ITile tile)
        {
            var style = Categories.First().List.FirstOrDefault(s => s.Name == flag) ?? new TileStyle(){Name = flag};
            switch (flag)
            {
                case "Roof":
                    {
                        style.AddTile(new TileRoof() { Id = tile.Id });
                        break;
                    }
                case "Floor":
                    {
                        style.AddTile(new TileFloor() {Id = tile.Id});
                        break;
                    }
                case "Wall":
                    {
                        style.AddTile(new TileWall() { Id = tile.Id });
                        break;
                    }
                default:
                    {
                        style.AddTile(new TileWall() { Id = tile.Id });
                        break;
                    }
            }

            Categories.First().AddStyle(style);
        }
        
        public void GenerateCandidates(MultiTile multiTile, TileCategory category)
        {
            var oldTileDescription = _tileData.GetItemTile(multiTile.Id);
            var tileinfo = Categories.Select(c=>c.FindTile(multiTile.Id)).FirstOrDefault(t => t!=null);
            

            var candidates = category.AllTiles().Where(tile =>
                                                       tileinfo != null && (_tileData.GetItemTile(tile.Id).Height == oldTileDescription.Height &&
                                                                            oldTileDescription.Flags.HasFlag(TileFlag.Window) ==
                                                                            _tileData.GetItemTile(tile.Id).Flags.HasFlag(TileFlag.Window) &&
                                                                            oldTileDescription.Flags.HasFlag(TileFlag.Wall) ==
                                                                            _tileData.GetItemTile(tile.Id).Flags.HasFlag(TileFlag.Wall) &&
                                                                            oldTileDescription.Flags.HasFlag(TileFlag.Roof) ==
                                                                            _tileData.GetItemTile(tile.Id).Flags.HasFlag(TileFlag.Roof) &&
                                                                            oldTileDescription.Flags.HasFlag(TileFlag.Surface) ==
                                                                            _tileData.GetItemTile(tile.Id).Flags.HasFlag(TileFlag.Surface)
                                                                            && tile.PositionString == tileinfo.PositionString)
                                                       );
            
            Candidates= new ObservableCollection<Tile>(candidates);
        }

        public void GenerateCandidates(ITile multiTile, TileCategory category)
        {
            if(multiTile is MultiTile)
                GenerateCandidates(multiTile,category);
            else
            {
               var newtile= _multiTiles.FirstOrDefault(t => t.Id == multiTile.Id);
                if(newtile!=null) GenerateCandidates(newtile,category);
                else
                {
                    if(Candidates!=null)
                    Candidates.Clear();
                }
            }

        }

        private IEnumerable<MultiTile> SelectTilesById(uint id )
        {
            return MultiTiles.Where(m => m.Id == id);
        }

        public void MassSet(uint oldId, Tile tile)
        {

            var list = SelectTilesById(oldId);
            foreach (var multiTile in list)
            {
                multiTile.SetTile(tile);
            }
            if(Categories.Contains(tile.GetStyle().GetCategory()))
            return;

            UpdateCategories();
        }

        public void RemoveRoof()
        {
            var roofs = _multiTiles.Where(t => _tileData.GetItemTile(t.Id).Flags.HasFlag(TileFlag.Roof));
            foreach (var multiTile in roofs)
            {
                _multiTiles.Remove(multiTile);
            }
            UpdateCategories();
        }

        public void RemoveFloor()
        {
            var floors = _multiTiles.Where(t => _tileData.GetItemTile(t.Id).Flags.HasFlag(TileFlag.Surface));
            foreach (var multiTile in floors)
            {
                _multiTiles.Remove(multiTile);
            }
            UpdateCategories();
        }

        private void UpdateCategories()
        {
            Categories.Clear();
            for (int index = 0; index < _multiTiles.Count; index++)
            {
                var multiTile = _multiTiles[index];
                if (Categories.Any(c => c.FindTile(multiTile.Id) != null))
                    continue;
                SelectTileforMultiTile(multiTile);
            }
        }
        #endregion



        #region Override Methods

        public override string ToString()
        {
            var liststring = new List<string>();
            var builder = new StringBuilder();
            builder.Append("6 version");
            builder.AppendLine();
            builder.Append("1 template id");
            builder.AppendLine();
            builder.Append("-1 item version");
            builder.AppendLine();
            builder.AppendFormat("{0} {1}", MultiTiles.Count, "num components");
            builder.AppendLine();
            foreach (var mtile in _multiTiles)
            {
                builder.AppendFormat("{0} {1} {2} {3} {4}", mtile.Id, mtile.X, mtile.Y, mtile.Z,
                                                   mtile.Flag);
                liststring.Add(string.Format("{0} {1} {2} {3} {4}", mtile.Id, mtile.X, mtile.Y, mtile.Z,
                                                   mtile.Flag));
                builder.AppendLine();
                
            }
            
            return builder.ToString();
            //MultiTiles.Aggregate("", (current, tile) => current + string.Format("{0} {1} {2} {3} {4} {5}", tile.Id, tile.X, tile.Y, tile.Z, tile.Flag, '\n'));
        }
        
        #endregion //Override Methods
        
        
        #endregion //Methods

    }
}
