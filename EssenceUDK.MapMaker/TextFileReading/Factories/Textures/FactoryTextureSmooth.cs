using EssenceUDK.MapMaker.Elements.Textures;
using EssenceUDK.MapMaker.Elements.Textures.TextureTransition;
using System;
using System.Linq;

namespace EssenceUDK.MapMaker.TextFileReading.Factories.Textures
{
    public class FactoryTextureSmooth : FactoryTransition
    {
        public CollectionAreaTransitionTexture Smooth { get; set; }

        public FactoryTextureSmooth(string filelocation) : base(filelocation)
        {
            Smooth = new CollectionAreaTransitionTexture();
        }

        public override void Read()
        {
            var smooth = new AreaTransitionTexture();
            var counter4 = 0;
            foreach (string s in Strings.Where(s => !string.IsNullOrEmpty(s) && !s.StartsWith("//")))
            {
                if (s.Contains("="))
                {
                    if (smooth.ColorFrom != System.Windows.Media.Colors.Black)
                    {
                        Smooth.List.Add(smooth);
                    }
                    smooth = new AreaTransitionTexture();
                    smooth.Name = s;
                    continue;
                }
                var chars = new char[] { '/' };
                var name = s.Split(chars, StringSplitOptions.RemoveEmptyEntries);
                var str = name[0].Split(separator, StringSplitOptions.RemoveEmptyEntries);

                if (str.Length == 1 && str[0].StartsWith("0x"))
                {
                    smooth.ColorFrom = ReadColorFromInt(str[0]);
                    continue;
                }
                if (str.Length == 2)
                {
                    smooth.ColorFrom = ReadColorFromInt(str[0]);
                    smooth.ColorTo = ReadColorFromInt(str[1]);
                    continue;
                }

                if (str.Length == 4)
                {
                    TransitionCheck(smooth, str.ToList(), counter4);
                    counter4++;
                    counter4 = counter4 % 3;
                    continue;
                }
            }
        }
    }
}