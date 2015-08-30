using System;
using EssenceUDK.MapMaker.Elements.BaseTypes.ComplexTypes.Enum;
using EssenceUDK.MapMaker.Elements.Textures.TextureArea;

namespace MapMakerPlugin.Models.DesignData
{
    public class AreaTextureDesignDataService : IServiceModelTexture
    {
        public void GetData(Action<object, Exception> callback)
        {
            var areatexture = new AreaTextures() { Index = 10, Name = "DataVirtual" };
            var random = new Random();
            for (var index = 0; index < 10; index++)
            {
                areatexture.List.Add(random.Next(1, 100));
                areatexture.AreaTransitionTexture.List.Add(
                               new EssenceUDK.MapMaker.Elements.Textures.TextureTransition.AreaTransitionTexture() { TextureIdTo = random.Next(10, 150), Name = String.Format("Land Trnasition {0}", index) });
                areatexture.CollectionAreaItems.List.Add(

                    new EssenceUDK.MapMaker.Elements.Items.ItemsTransition.AreaTransitionItem()
                    {
                        TextureIdTo = random.Next(10, 150),
                        Name = String.Format("TransitionItems_{0}", index)
                    });
            }
            foreach (
            LineType lineType in
                Enum.GetValues(typeof(LineType)))
            {
                foreach (var direction in Enum.GetValues(typeof(EssenceUDK.MapMaker.Elements.Direction)))
                {
                    foreach (var trans in areatexture.AreaTransitionTexture.List)
                        for (var i = 0; i < 10; i++)
                        {
                            trans.AddElement(lineType, (int)direction, random.Next(0, 100));
                        }
                    foreach (var trans in areatexture.CollectionAreaItems.List)
                        for (var i = 0; i < 10; i++)
                        {
                            trans.AddElement(lineType, (int)direction, random.Next(0, 100));
                        }
                }
            }

            callback(areatexture, null);
        }
    }
}