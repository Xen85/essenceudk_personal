using System;
using System.Linq;
using EssenceUDK.MapMaker.Elements.Textures;

namespace EssenceUDK.MapMaker.TextFileReading.Factories.Textures
{
    public class FactoryTextureArea : Factory
    {
        public CollectionAreaTexture Textures { get; set; }

        public FactoryTextureArea(string location) : base(location)
        {
            Textures = new CollectionAreaTexture();
        }

        public override void Read()
        {

            var texture = new Elements.Textures.TextureArea.AreaTextures();
            int counter = -1;
            foreach (string s in Strings.Where(s => !s.StartsWith("// ")).Where(s => !string.IsNullOrEmpty(s)))
            {
                if(s.Contains("//"))
                {
                    texture.Name = s.Replace("//","");
                    continue;
                }
                if(texture.Index ==0)
                {
                    texture.Index = int.Parse(s);
                    continue;
                }
                if(counter == -1)
                {
                    counter = int.Parse(s);
                    continue;
                }
                if(counter > 0)
                {
                    texture.List.Add( Convert.ToInt32(s,16));
                    counter--;
                }
                if(counter == 0)
                {
                    counter--;
                    Textures.List.Add(texture);
                    texture = new Elements.Textures.TextureArea.AreaTextures();
                    continue;
                }

            }
        }
    }
}
