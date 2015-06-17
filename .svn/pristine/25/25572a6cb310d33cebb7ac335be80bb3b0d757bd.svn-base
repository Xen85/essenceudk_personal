using System.Collections.Generic;
using System.IO;
using System.Linq;
using EssenceUDK.Platform.MiscHelper.Components;
using EssenceUDK.Platform.MiscHelper.Components.Tiles;
using EssenceUDK.Platform.MiscHelper.Interfaces;

namespace EssenceUDK.Platform.MiscHelper.Factories
{
    public class Doors : Factory , IFactory
    {

        public Doors(UODataManager location) : base(location)
        {
        }

        public List<TileCategory> Categories
        {
            get { return Categories; }
        }

        public override void Populate()
        {
            var txtFileLines = File.ReadAllLines(DataManager.Location.LocalPath + "doors.txt");
            var typeNames = txtFileLines[1].Split(Separators);

            for (int i = 2; i < txtFileLines.Length; i++)
            {
                var infos = txtFileLines[i].Split('\t');
                var category = new TileCategory();
                category.Name = infos.Last();

                var style = new TileStyle();
                category.AddStyle(style);

                for (int j = 1; j < typeNames.Length - 2; j++)
                {
                    if (infos[j] != "0")
                    {
                        var tile = new TileDoor { Id = uint.Parse(infos[j]) };
                        style.List.Add(tile);
                    }
                }
                Categories.Add(category);

            }
            TilesCategorySDKModule.Supp.PositionCheck(Categories);
        }
    }
}
