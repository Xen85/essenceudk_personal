using System.Windows.Media;

namespace EssenceUDK.MapMaker.MapMaking
{
    public struct MapZObjectCoordinates
    {
        private readonly sbyte[] _list;

        private readonly sbyte _center;
        private readonly sbyte _north;
        private readonly sbyte _south;
        private readonly sbyte _east;
        private readonly sbyte _west;
        private readonly sbyte _northEast;
        private readonly sbyte _northWest;
        private readonly sbyte _southEast;
        private readonly sbyte _southWest;

        public sbyte Center { get { return _center; } }
        public sbyte North { get { return _north; } }
        public sbyte South { get { return _south; } }
        public sbyte East { get { return _east; } }
        public sbyte West { get { return _west; } }
        public sbyte NorthEast { get { return _northEast; } }
        public sbyte NorthWest { get { return _northWest; } }
        public sbyte SouthEast { get { return _southEast; } }
        public sbyte SouthWest { get { return _southWest; } }

        public sbyte[] List { get { return _list; } }
        public MapZObjectCoordinates(Coordinates coordinates, Color[] map)
        {

            _center = MapMaker.CalculateHeightValue(map[coordinates.Center]);
            _north = MapMaker.CalculateHeightValue(map[coordinates.North]);
            _south = MapMaker.CalculateHeightValue(map[coordinates.South]);
            _east = MapMaker.CalculateHeightValue(map[coordinates.East]);
            _west = MapMaker.CalculateHeightValue(map[coordinates.West]);
            _northWest = MapMaker.CalculateHeightValue(map[coordinates.NorthWest]);
            _northEast = MapMaker.CalculateHeightValue(map[coordinates.NorthEast]);
            _southWest = MapMaker.CalculateHeightValue(map[coordinates.SouthWest]);
            _southEast = MapMaker.CalculateHeightValue(map[coordinates.SouthEast]);

            _list = new[] { _center, _north, _south, _east, _west, _northEast, _northWest, _southEast, _southWest };
        }


    }
}