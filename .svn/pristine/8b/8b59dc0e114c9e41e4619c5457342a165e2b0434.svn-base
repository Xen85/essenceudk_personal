using System.Collections.Generic;
using System.IO;
using System.Linq;
using EssenceUDK.Platform.MiscHelper.Components;
using EssenceUDK.Platform.MiscHelper.Components.Enums;

namespace EssenceUDK.Platform.MiscHelper.Factories
{
    public class SuppInfo
    {
       

        private readonly UODataManager _dataManager;
        private Dictionary<uint, PositionTiles> Positions; 
        private const string South = @"\";
        private const string corner = @"v";
        private const string East = "./";
        private const string Post = "o";
        public SuppInfo(UODataManager location)
        {
            _dataManager = location;
            Positions = new Dictionary<uint, PositionTiles>();
        }

        public void Populate()
        {
            var lines = File.ReadAllLines(_dataManager.Location.LocalPath + "misc.txt");
            for (int i = 2; i < lines.Length; i++)
            {
                var info = lines[i].Split('\t');
                if(!Positions.Keys.Contains(uint.Parse(info[1])))
                Positions.Add(uint.Parse(info[1]),GetPositionTile(info[0]));
            }
        }



        private PositionTiles GetPositionTile(string resolve)
        {
            switch (resolve)
            {
                case South:
                    {
                        return PositionTiles.South;
                    }
                case East:
                    {
                        return PositionTiles.East;
                    }
                case corner:
                    {
                        return PositionTiles.Corner;
                    }
                case Post:
                    {
                        return PositionTiles.Post;
                    }
                default:
                    return PositionTiles.None;
            }
        }


        public void PositionCheck(IList<TileCategory> categories)
        {
            foreach (var tile in from tileCategory in categories from style in tileCategory.List from tile in style.List select tile)
            {
                PositionTiles pos;
                Positions.TryGetValue(tile.Id, out pos);
                tile.Position = (int)pos;
            }
        }
    }
}
