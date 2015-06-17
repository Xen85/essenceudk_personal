using System;
using System.Collections.ObjectModel;
using EssenceUDK.MapMaker.Elements.ColorArea;
using EssenceUDK.MapMaker.Elements.ColorArea.ColorArea;
using EssenceUDK.MapMaker.Elements.ColorArea.ColorMountains;

namespace EssenceUDK.MapMaker.TextFileReading.Factories.Colors
{
    public class FactoryMountains:Factory
    {
        public CollectionAreaColorMountains Mountains { get; set; }

        public FactoryMountains(string location) : base(location)
        {
            Mountains = new CollectionAreaColorMountains();
        }

        public override void Read()
        {
            int counter = 0;
            var mountains = new AreaColor();
            foreach (string s in Strings)
            {
                if(s.StartsWith("//"))
                {
                    mountains.Name = s.Replace("//","");
                    continue;
                }
                if (string.IsNullOrEmpty(s))
                {
                    if(mountains.Color != System.Windows.Media.Colors.Black)
                        Mountains.List.Add(mountains);
                    mountains = new AreaColor();
                    continue;
                }
                char[] chars = {','};
                var str = s.Split(chars,StringSplitOptions.RemoveEmptyEntries);
                if(str.Length == 1)
                {
                    if (mountains.Color == System.Windows.Media.Colors.Black)
                        mountains.Color = ReadColorFromInt(s);
                    else
                    {
                        mountains.ColorTopMountain = ReadColorFromInt(s);
                    }
                    continue;
                }

                if(str.Length == 2)
                {
                    if(mountains.TextureIndex == 0)
                    {
                        mountains.TextureIndex = int.Parse(str[0]);
                        mountains.ModeAutomatic = int.Parse(str[1]) == 1;
                        continue;
                    }
                    if(s.StartsWith("0x"))
                    {
                        mountains.ColorTopMountain = ReadColorFromInt(str[0]);
                        mountains.IndexTextureTop = int.Parse(str[1]);
                        continue;
                    }
                    var circle = new CircleMountain {From = int.Parse(str[0]), To = int.Parse(str[1])};
                    if(mountains.List == null) mountains.List=new ObservableCollection<CircleMountain>();
                    mountains.List.Add(circle);
                    continue;
                }
                
            }
            //Mountains.List.Add(mountains);
        }
    }
}
