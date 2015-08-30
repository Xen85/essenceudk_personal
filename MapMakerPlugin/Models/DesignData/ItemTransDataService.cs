using System;
using EssenceUDK.MapMaker.Elements.BaseTypes.ComplexTypes.Enum;
using EssenceUDK.MapMaker.Elements.Items.ItemsTransition;
using EssenceUDKMVVM.Models;

namespace MapMakerPlugin.Models.DesignData
{
    internal class ItemTransDataService : IAreaItemTransDataService
    {
        public void GetData(Action<object, Exception> callback)
        {
            var trans = new AreaTransitionItem { Name = "Trans Item Data Model" };
            var random = new Random();
            trans.TextureIdTo = random.Next(0, 200);
            foreach (
           LineType lineType in
               Enum.GetValues(typeof(LineType)))
            {
                foreach (var direction in Enum.GetValues(typeof(EssenceUDK.MapMaker.Elements.Direction)))
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
}