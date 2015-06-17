using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EssenceUDK.Platform.DataTypes;

namespace EssenceUDK.Platform.TileEngine
{
    public interface ISceneEntry
    {
        sbyte Altitude { get; }
        ushort EntryID { get; }
    }

    public interface ISceneLand : ISceneEntry
    {
    }

    public interface ISceneTexm : ISceneLand
    {
        sbyte AltitudeLeft  { get; }
        sbyte AltitudeRight { get; }
        sbyte AltitudeDown  { get; }
    }

    public interface ISceneItem : ISceneEntry
    {
    }

    public interface ISceneObject : ISceneEntry
    {
        int Serial { get; }
    }


    public interface ISceneMobile
    {
    }


    public class SceneBuffer
    {
        private UODataManager dataManager;
        private TilesComparer tileComparer;

        public SceneBuffer(UODataManager manager)
        {
            dataManager = manager;
            if (dataManager.DataFactory == null)
                throw new NullReferenceException("DataFactory wasn't initialized.");

            tileComparer = new TilesComparer(dataManager);
        }


        internal class TilesComparer : IComparer<IEntryMapTile>
        {
            private UODataManager dataManager;

            public TilesComparer(UODataManager manager)
            {
                dataManager = manager;
            }

            public int Compare(IEntryMapTile l, IEntryMapTile r)
            {
                //  0 ->  l = r
                // -1 ->  l < r
                // +1 ->  l > r

                var pl = l.Altitude;
                var pr = r.Altitude;

                var il = l as IItemMapTile;
                var ir = r as IItemMapTile;

                if (il != null) {
                    var dl = dataManager.GetItemTile(l.TileId);

                    if (dl.Height > 0)
                        ++pl;
                    if (dl.Flags.HasFlag(TileFlag.Background))
                        --pl;
                    if (dl.Flags.HasFlag(TileFlag.Foliage))
                        ++pl;
                    if (dl.Flags.HasFlag(TileFlag.Wall) || dl.Flags.HasFlag(TileFlag.Window))
                        ++pl;
                } else
                    pl -= 2;

                if (ir != null) {
                    var dr = dataManager.GetItemTile(r.TileId);

                    if (dr.Height > 0)
                        ++pr;
                    if (dr.Flags.HasFlag(TileFlag.Background))
                        --pr;
                    if (dr.Flags.HasFlag(TileFlag.Foliage))
                        ++pr;
                    if (dr.Flags.HasFlag(TileFlag.Wall) || dr.Flags.HasFlag(TileFlag.Window))
                        ++pr;
                } else
                    pr -= 2;

                if (pl == pr)
                    return 0;
                return pl > pr ? +1 : -1;
            }
        }





    }
}
