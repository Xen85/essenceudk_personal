using System;
using System.Linq;
using EssenceUDK.MapMaker.Elements.ColorArea;
using EssenceUDK.MapMaker.Elements.ColorArea.ColorArea;

namespace EssenceUDK.MapMaker.TextFileReading.Factories.Colors
{
    public class FactoryCoast:Factory
    {
        public  CollectionAreaColor Area { get; set; }

        public FactoryCoast(string location) : base(location)
        {
           Area = new CollectionAreaColor();
        }

        public override void Read()
        {
            foreach (var area in from s in Strings
                                 where !s.StartsWith("//") && !string.IsNullOrEmpty(s)
                                 select s.Split('/') into name
                                 let strings = name.First().Split(separator, StringSplitOptions.RemoveEmptyEntries)
                                 select new AreaColorCoast()
                                 {
                                     Color = ReadColorFromInt(Convert.ToInt32(strings[0], 16)),
                                     Name = name.Last(),
                                     TextureIndex=int.Parse(strings[1]) ,
                                     Min = int.Parse(strings[2]),
                                     Index =  Area.List.Count,
                                     Max = int.Parse(strings[3])
                                 })
            {
                Area.List.Add(area);
                
            }
        }
    }
}
