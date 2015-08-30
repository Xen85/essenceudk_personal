using EssenceUDK.MapMaker.Elements.Items.Items;
using EssenceUDK.MapMaker.Elements.Items.ItemText;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EssenceUDK.MapMaker.TextFileReading.Factories.Items
{
    public class FactoryItems : Factory
    {
        public Elements.Items.CollectionAreaItems Items { get; set; }

        public FactoryItems(string location) : base(location)
        {
            Items = new Elements.Items.CollectionAreaItems();
        }

        public override void Read()
        {
            var itemgroup = new CollectionItem();

            var items = new AreaItems();

            var item = new SingleItem();

            foreach (string s in Strings)
            {
                if (s.StartsWith("// ")) continue;

                if (s.StartsWith("//"))
                {
                    items = new AreaItems { Name = s.Replace("//", "") };

                    continue;
                }

                if (string.IsNullOrEmpty(s)) continue;

                if (s.StartsWith("0x"))
                {
                    if (items.Color != System.Windows.Media.Colors.Black)
                    {
                        Items.List.Add(items);
                    }
                    items.Color = ReadColorFromInt(s);
                    itemgroup = new CollectionItem();
                    Items.List.Add(items);
                    continue;
                }
                itemgroup = new CollectionItem();
                var str = s.Split('/');
                itemgroup.Name = str.Last();
                var s1 = str[0].Split(separator, StringSplitOptions.RemoveEmptyEntries).ToList();

                itemgroup.Percent = int.Parse(s1.First());

                s1.Remove(s1.First());

                for (int index = 0; index < s1.Count; index++)
                {
                    DivideEtImpera(index, s1, ref item, itemgroup);
                }

                items.List.Add(itemgroup);
            }
        }

        private void DivideEtImpera(int stringCounter, List<String> list, ref SingleItem item, CollectionItem itemGroup)
        {
            switch (stringCounter % 5)
            {
                case 0:
                    {
                        item.Id = Convert.ToInt16(list[stringCounter], 16);
                        break;
                    }
                case 1:
                    {
                        item.Z = sbyte.Parse(list[stringCounter]);
                        break;
                    }
                case 2:
                    {
                        item.Hue = Convert.ToInt16(list[stringCounter], 16);
                        break;
                    }
                case 3:
                    {
                        item.X = int.Parse(list[stringCounter]);
                        break;
                    }
                case 4:
                    {
                        item.Y = int.Parse(list[stringCounter]);
                        itemGroup.List.Add(item);
                        item = new SingleItem();
                        break;
                    }
                default:
                    break;
            }
        }
    }
}