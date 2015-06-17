using System.Collections.Generic;
using UltimaXNA.Ultima.Entities;
using UltimaXNA.Ultima.Entities.Items;

namespace UltimaXNA.Ultima.World
{
    public static class StaticManager
    {
        private static readonly List<StaticItem> ActiveStatics = new List<StaticItem>();

        public static void AddStaticThatNeedsUpdating(StaticItem item)
        {
            if (item.IsDisposed || item.Overheads.Count == 0)
                return;

            ActiveStatics.Add(item);
        }

        public static void Update(double frameMs)
        {
            for (var i = 0; i < ActiveStatics.Count; i++)
            {
                ActiveStatics[i].Update(frameMs);
                if (ActiveStatics[i].IsDisposed || ActiveStatics[i].Overheads.Count == 0)
                    ActiveStatics.RemoveAt(i);
            }
        }
    }
}
