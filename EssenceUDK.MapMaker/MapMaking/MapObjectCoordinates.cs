using EssenceUDK.MapMaker.Elements.ColorArea.ColorArea;
using System.Collections.Generic;

namespace EssenceUDK.MapMaker.MapMaking
{
    /// <summary>
    /// this class is needed to describe a TILE in the map
    /// </summary>
    public class MapObjectCoordinates
    {
        private readonly MapObject _center;
        private readonly MapObject _north;
        private readonly MapObject _south;
        private readonly MapObject _east;
        private readonly MapObject _west;
        private readonly MapObject _northEast;
        private readonly MapObject _northWest;
        private readonly MapObject _southEast;
        private readonly MapObject _southWest;

        public MapObject Center { get { return _center; } }
        public MapObject North { get { return _north; } }
        public MapObject South { get { return _south; } }
        public MapObject East { get { return _east; } }
        public MapObject West { get { return _west; } }
        public MapObject NorthEast { get { return _northEast; } }
        public MapObject NorthWest { get { return _northWest; } }
        public MapObject SouthEast { get { return _southEast; } }
        public MapObject SouthWest { get { return _southWest; } }

        public MapObject[] List { get; set; }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="coordinates">coordinates class</param>
        /// <param name="map">mapobejct array</param>
        public MapObjectCoordinates(Coordinates coordinates, MapObject[] map)
        {
            _center = map[coordinates.Center];
            _north = map[coordinates.North];
            _south = map[coordinates.South];
            _east = map[coordinates.East];
            _west = map[coordinates.West];
            _northWest = map[coordinates.NorthWest];
            _northEast = map[coordinates.NorthEast];
            _southWest = map[coordinates.SouthWest];
            _southEast = map[coordinates.SouthEast];

            List = new[] { _center, _north, _south, _east, _west, _northEast, _northWest, _southEast, _southWest };
        }

        /// <summary>
        /// Helper to place objects on the map
        /// </summary>
        /// <param name="areaColorCoordinates">area color coordinates</param>
        /// <param name="altitude">altitude of textures</param>
        /// <param name="itemid">item id</param>
        /// <param name="zItem">item z</param>
        /// <param name="texture">texture</param>
        /// <param name="normal">is it cliff or not</param>
        /// <returns>true</returns>
        public bool PlaceObject(AreaColorCoordinates areaColorCoordinates, sbyte altitude, int itemid,
                                sbyte zItem, int texture, bool normal = true)
        {
            var mapObject = !normal ? Center : SouthEast;
            if (mapObject.Occupied != 0 && (mapObject.Occupied != (byte)TypeColor.WaterCoast && itemid != (int)SpecialAboutItems.ClearAll))
                return true;

            if (itemid >= 0)
                mapObject.Items = new List<ItemClone> { new ItemClone { Id = itemid, Z = zItem } };
            if (itemid == (int)SpecialAboutItems.ClearAll)
                mapObject.Items = null;

            mapObject.Occupied = (byte)areaColorCoordinates.Center.Type;

            if (texture >= 0)
                Center.Texture = (short)texture;
            Center.Altitude = altitude;

            return true;
        }

        /// <summary>
        /// Wraps the PaceObject to choise if add a hue or adding the occupation of the land
        /// </summary>
        /// <param name="areaColorCoordinates">area color coordinates</param>
        /// <param name="altitude">altitude of textures</param>
        /// <param name="itemid">item id</param>
        /// <param name="zItem">item z</param>
        /// <param name="texture">texture</param>
        /// <param name="normal">is it cliff or not</param>
        /// <param name="occupied">should the land be flagged as occupated?</param>
        /// <param name="hue"> hue of the object</param>
        /// <returns>true</returns>
        public bool PlaceObjectOcc(AreaColorCoordinates areaColorCoordinates, sbyte altitude, int itemid,
                                sbyte zItem, int texture, bool normal = true, bool occupied = true, int hue = 0)
        {
            var mapObject = !normal ? Center : SouthEast;
            if (mapObject.Occupied != 0 && (mapObject.Occupied != (byte)TypeColor.WaterCoast && itemid != (int)SpecialAboutItems.ClearAll))
                return true;

            if (itemid >= 0)
                mapObject.Items = new List<ItemClone> { new ItemClone { Id = itemid, Z = zItem, Hue = hue } };
            if (itemid == (int)SpecialAboutItems.ClearAll)
                mapObject.Items = null;

            if (occupied)
                mapObject.Occupied = (byte)areaColorCoordinates.Center.Type;

            if (texture >= 0)
                Center.Texture = (short)texture;
            Center.Altitude = altitude;

            return true;
        }
    }
}