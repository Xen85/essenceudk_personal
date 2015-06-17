using System;
using System.IO;
using System.Linq;
using EssenceUDK.Platform.MiscHelper.Components;
using EssenceUDK.Platform.MiscHelper.Components.Enums;
using EssenceUDK.Platform.MiscHelper.Components.Tiles;
using EssenceUDK.Platform.MiscHelper.Interfaces;

namespace EssenceUDK.Platform.MiscHelper.Factories
{
    public class Roofs : Factory ,IFactory
    {
        public Roofs(UODataManager location) : base(location)
        {
        }

        public override void Populate()
        {
            var txtFileLines = File.ReadAllLines(DataManager.Location.LocalPath + "roofs.txt");
            var typeNames = txtFileLines[1].Split(Separators);
            TileCategory category = null;
            for (int i = 2; i < txtFileLines.Length; i++)
            {
                var infos = txtFileLines[i].Split('\t');

                if (infos[1] == "0")
                {
                    category = new TileCategory(Int32.Parse(infos[2]), TypeTile.Roofs) {Name = infos.Last()};
                    Categories.Add(category);
                }
                var style = new TileStyle();
                category.AddStyle(style);
                style.Name = infos.Last();
                style.Id = Int32.Parse(infos[1]);
                for (int j = 3; j < typeNames.Length - 3; j++)
                {
                    if (infos[j] != "0")
                    {
                        var tile = new TileRoof { Id = uint.Parse(infos[j]) };
                        style.AddTile(tile);
                       tile.Position = (j-2);
                    }
                }
                
            }
        }
    }
}
