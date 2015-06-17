using System;
using System.Drawing;
using System.Linq;
using EssenceUDK.MapMaker.Elements.BaseTypes.ComplexTypes.Enum;
using EssenceUDK.MapMaker.Elements.Items;
using EssenceUDK.MapMaker.Elements.Items.ItemsTransition;

namespace EssenceUDK.MapMaker.TextFileReading.Factories.Items
{
    public class FactorySmoothItems : FactoryTransition
    {
        public CollectionAreaTransitionItems SmoothsAll { get; set; }

        public FactorySmoothItems(string filelocation) : base(filelocation)
        {
            SmoothsAll = new CollectionAreaTransitionItems();
        }

        protected override void SetTransition(int counter, Elements.BaseTypes.ComplexTypes.Transition transition, int j, string s)
        {
            transition.AddElement((LineType)counter,j, Convert.ToInt32(s,16));
        }

        public override void Read()
        {
            Color from = Color.Black;
            Color To = Color.Black;
            var items = new AreaTransitionItem();
            var counter4 = 0;
            foreach (var strings in from s in Strings where !string.IsNullOrEmpty(s) where s.StartsWith("0x") select s.Split(separator))
            {
                if(strings.Length<4)
                {
                    items.ColorFrom = ReadColorFromInt(strings[0]);
                    if (strings.Length > 1)
                        items.ColorTo = ReadColorFromInt(strings[1]);
                    continue;
                }
                if(strings.Length == 4)
                {
                    TransitionCheck(items,strings.ToList(),counter4);
                    counter4++;
                    counter4 = counter4%3;
                }

                if(counter4 == 0)
                {
                    SmoothsAll.List.Add(items);
                    items = new AreaTransitionItem();
                    continue;
                }
            }

        }

    }
}
