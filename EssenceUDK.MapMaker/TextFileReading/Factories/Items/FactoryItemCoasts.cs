using EssenceUDK.MapMaker.Elements.BaseTypes.ComplexTypes.Enum;
using EssenceUDK.MapMaker.Elements.Items;
using EssenceUDK.MapMaker.Elements.Items.ItemCoast;
using System;
using System.Linq;

namespace EssenceUDK.MapMaker.TextFileReading.Factories.Items
{
    public class FactoryItemCoasts : FactoryTransition
    {
        public CollectionAreaTransitionItemCoast CoastsAll { get; set; }

        public FactoryItemCoasts(string location) : base(location)
        {
            CoastsAll = new CollectionAreaTransitionItemCoast();
        }

        protected override void SetTransition(int counter, Elements.BaseTypes.ComplexTypes.Transition transition, int j, string s)
        {
            var coast = transition as TransitionItemsCoast;
            if (coast == null)
            {
                int a = 5;
            }
            int value = Convert.ToInt32(s, 16);
            coast.AddElement((LineType)counter, j, value);
        }

        public override void Read()
        {
            var coastpart = new TransitionItemsCoast();
            var CoastTotal = new AreaTransitionItemCoast();
            var counterLengt4 = 0;

            foreach (string s in Strings.Where(s => !string.IsNullOrEmpty(s)).Where(s => !s.StartsWith("//") && s != "1"))
            {
                if (s.Contains("="))
                {
                    CoastTotal.Name = s;
                    continue;
                }
                var str = s.Split(separator, StringSplitOptions.RemoveEmptyEntries);

                if (str.Length == 1)
                {
                    if (s.StartsWith("0x") && coastpart.Color == System.Windows.Media.Colors.Black)
                    {
                        coastpart.Color = ReadColorFromInt(s);
                        continue;
                    }
                    if (s.StartsWith("0x"))
                    {
                        coastpart.Texture = Convert.ToInt32(s, 16);
                        continue;
                    }
                }

                if (str.Length == 4)
                {
                    TransitionCheck(coastpart, str.ToList(), counterLengt4);
                    counterLengt4++;
                    counterLengt4 = counterLengt4 % 3;
                }

                if (counterLengt4 == 0)
                {
                    if (CoastTotal.Coast.Color == System.Windows.Media.Colors.Black)
                    {
                        CoastTotal.Coast = coastpart;
                    }
                    else
                    {
                        CoastTotal.Ground = coastpart;
                        CoastsAll.List.Add(CoastTotal);
                        CoastTotal = new AreaTransitionItemCoast();
                    }
                    coastpart = new TransitionItemsCoast();
                    continue;
                }
            }
        }
    }
}