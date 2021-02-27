using System;
using EssenceUDK.MapMaker.Elements;
using EssenceUDK.MapMaker.Elements.BaseTypes.ComplexTypes.Enum;
using EssenceUDK.MapMaker.Elements.Textures.TextureTransition;

namespace EssenceUDKMVVM.Models.DesignDataServices
{
    public class AreaTransitionTextureDataServiceStatic : IAreaTransitionTextureDataService
    {
        public void GetData(Action<object, Exception> callback)
        {
            var trans = new AreaTransitionTexture {Name = "Trans Data Model"};
            var random = new Random();
            trans.TextureIdTo = random.Next(0, 200);
            foreach (
           LineType lineType in
               Enum.GetValues(typeof(LineType)))
            {
                foreach (var direction in Enum.GetValues(typeof(Direction)))
                {
                        for (var i = 0; i < 10; i++)
                        {
                            trans.AddElement(lineType, (int)direction, random.Next(0, 100));
                        }

                }
            }
            callback(trans, null);
        }
    }
    
    public class AreaTransitionTextureDataService : IAreaTransitionTextureDataService
    {
        public void GetData(Action<object, Exception> callback)
        {
          
            callback(null, null);
        }
    }
}
