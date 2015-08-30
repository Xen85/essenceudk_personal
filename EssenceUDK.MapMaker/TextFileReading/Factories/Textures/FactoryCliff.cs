using EssenceUDK.MapMaker.Elements.Textures;
using EssenceUDK.MapMaker.Elements.Textures.TexureCliff;
using System;

namespace EssenceUDK.MapMaker.TextFileReading.Factories.Textures
{
    public class FactoryCliff : Factory
    {
        public CollectionAreaTransitionCliffTexture CollectionAreaCliffs { get; set; }

        public FactoryCliff(string location) : base(location)
        {
            CollectionAreaCliffs = new CollectionAreaTransitionCliffTexture();
        }

        public override void Read()
        {
            int counter = -1;

            foreach (var s in Strings)
            {
                if (s.StartsWith("//"))
                {
                    counter++;
                    continue;
                }

                if (string.IsNullOrWhiteSpace(s)) continue;

                var cliff = new AreaTransitionCliffTexture();
                var name = s.Split('/');
                var strings = name[0].Split(separator, StringSplitOptions.RemoveEmptyEntries);
                if (strings.Length == 1)
                {
                    CollectionAreaCliffs.Color = ReadColorFromInt(strings[0].Replace("\t", ""));
                    continue;
                }

                if (strings.Length > 1)
                {
                    cliff.Name = name[2];

                    foreach (string s1 in strings)
                    {
                        if (s1.Length == 8)
                            if (cliff.ColorFrom.R == 0 && cliff.ColorFrom.G == 0 && cliff.ColorFrom.B == 0)
                                cliff.ColorFrom = ReadColorFromInt(s1);
                            else
                            {
                                if (cliff.ColorTo.R == 0 && cliff.ColorTo.G == 0 && cliff.ColorTo.B == 0)
                                {
                                    cliff.ColorTo = ReadColorFromInt(s1);
                                }
                                else
                                {
                                    cliff.ColorEdge = ReadColorFromInt(s1);
                                }
                            }
                        else
                        {
                            var id = Convert.ToInt32(s1, 16);
                            if (id != 0)
                                cliff.List.Add(id);
                        }
                        cliff.Directions = (DirectionCliff)counter;
                    }
                }

                CollectionAreaCliffs.List.Add(cliff);
            }
        }
    }
}