using System;
using System.Linq;
using EssenceUDK.MapMaker.Elements.ColorArea;
using EssenceUDK.MapMaker.Elements.ColorArea.ColorArea;

namespace EssenceUDK.MapMaker.TextFileReading.Factories.Colors
{
    public class FactoryColorArea : Factory
    {
        public CollectionAreaColor Areas { get; set; }
        #region ctor
        public FactoryColorArea(string location) : base(location)
        {
            Areas = new CollectionAreaColor();
        }
        #endregion

        public override void Read()
        {
            foreach (string s in Strings)
            {
                if (s.StartsWith("//") || string.IsNullOrEmpty(s)) continue;
                var strings = s.Split('/');
                var read = strings[0].Split(separator, StringSplitOptions.RemoveEmptyEntries);
                var area = new AreaColor();
                area.Color = ReadColorFromInt(read[0]);
                area.Name = strings.Last();
                area.TextureIndex = int.Parse(read[1]);
                area.Min = int.Parse(read[2]);
                area.Max = int.Parse(read[3]);
                area.Index = Areas.List.Count;
                Areas.List.Add(area);
            }
        }
    }
}
