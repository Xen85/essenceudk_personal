using EssenceUDK.MapMaker.Elements.ColorArea.ColorArea;
using System.Linq;

namespace EssenceUDK.MapMaker.MapMaking
{
    public class AreaColorCoordinates
    {
        private readonly AreaColor _center;
        private readonly AreaColor _north;
        private readonly AreaColor _south;
        private readonly AreaColor _east;
        private readonly AreaColor _west;
        private readonly AreaColor _northEast;
        private readonly AreaColor _northWest;
        private readonly AreaColor _southEast;
        private readonly AreaColor _southWest;

        public AreaColor Center
        {
            get { return _center; }
        }

        public AreaColor North
        {
            get { return _north; }
        }

        public AreaColor South
        {
            get { return _south; }
        }

        public AreaColor East
        {
            get { return _east; }
        }

        public AreaColor West
        {
            get { return _west; }
        }

        public AreaColor NorthEast
        {
            get { return _northEast; }
        }

        public AreaColor NorthWest
        {
            get { return _northWest; }
        }

        public AreaColor SouthEast
        {
            get { return _southEast; }
        }

        public AreaColor SouthWest
        {
            get { return _southWest; }
        }

        public AreaColor[] List { get; set; }

        public AreaColorCoordinates(Coordinates coordinates, AreaColor[] map)
        {
            List = new AreaColor[9];

            List[(int)Directions.Center] = map[coordinates.Center];
            _center = List[(int)Directions.Center];

            List[(int)Directions.East] = map[coordinates.East];
            _east = List[(int)Directions.East];

            List[(int)Directions.North] = map[coordinates.North];
            _north = List[(int)Directions.North];

            List[(int)Directions.NorthEast] = map[coordinates.NorthEast];
            _northEast = List[(int)Directions.NorthEast];

            List[(int)Directions.NorthWest] = map[coordinates.NorthWest];
            _northWest = List[(int)Directions.NorthWest];

            List[(int)Directions.South] = map[coordinates.South];
            _south = List[(int)Directions.South];

            List[(int)Directions.SouthEast] = map[coordinates.SouthEast];
            _southEast = List[(int)Directions.SouthEast];

            List[(int)Directions.SouthWest] = map[coordinates.SouthWest];
            _southWest = List[(int)Directions.SouthWest];

            List[(int)Directions.West] = map[coordinates.West];
            _west = List[(int)Directions.West];
        }

        public bool IsAllColor()
        {
            return List.All(areaColor => areaColor.Index == Center.Index);
        }

        public bool IsAllType()
        {
            return List.All(areaColor => areaColor.Type == Center.Type);
        }

        public bool IsEastLine(int id)
        {
            if (West.Index != id
               || NorthWest.Index != id
               || SouthWest.Index != id
               || North.Index != id
               || South.Index != id
               )
                return false;

            return East.Index != id
                   || NorthEast.Index != id
                   || SouthEast.Index != id;
        }

        public bool IsEastLine(TypeColor type)
        {
            if (West.Type != type
                || NorthWest.Type != type
                || SouthWest.Type != type
                || North.Type != type
                || South.Type != type
                )
                return false;

            return East.Type != type
                   || NorthEast.Type != type
                   || SouthEast.Type != type;
        }

        #region Location Methods

        public bool IsWestLine(int index)
        {
            if (
                NorthEast.Index != index
                || North.Index != index
                || East.Index != index
                || SouthEast.Index != index
                || South.Index != index
                )
                return false;

            return West.Index != index || SouthWest.Index != index || NorthWest.Index != index;
        }

        public bool IsWestLine(TypeColor type)
        {
            if (
                NorthEast.Type != type
                || North.Type != type
                || East.Type != type
                || SouthEast.Type != type
                || South.Type != type
                )
                return false;

            return West.Type != type || SouthWest.Type != type || NorthWest.Type != type;
        }

        public bool IsNorthLine(TypeColor type)
        {
            if (South.Type != type
                || SouthWest.Type != type
                || SouthEast.Type != type
                || East.Type != type
                || West.Type != type)
                return false;

            return North.Type != type
                   || NorthEast.Type != type
                   || NorthWest.Type != type;
        }

        public bool IsNorthLine(int index)
        {
            if (South.Index != index
                || SouthWest.Index != index
                || SouthEast.Index != index
                || East.Index != index
                || West.Index != index)
                return false;

            return North.Index != index
                   || NorthEast.Index != index
                   || NorthWest.Index != index;
        }

        public bool IsSouthLine(TypeColor type)
        {
            if (North.Type != type
                || NorthWest.Type != type
                || NorthEast.Type != type
                || East.Type != type
                || West.Type != type)
                return false;

            return South.Type != type || SouthEast.Type != type ||
                   SouthWest.Type != type;
        }

        public bool IsSouthLine(int index)
        {
            if (North.Index != index
                || NorthWest.Index != index
                || NorthEast.Index != index
                || East.Index != index
                || West.Index != index)
                return false;

            return South.Index != index || SouthEast.Index != index ||
                   SouthWest.Index != index;
        }

        public bool IsSouthWestEdge(TypeColor type)
        {
            if (North.Type != type || NorthEast.Type != type || East.Type != type)
                return false;

            if (South.Type != type || SouthEast.Type != type || SouthWest.Type != type)
                if (West.Type != type || NorthWest.Type != type)
                    return true;

            return false;
        }

        public bool IsSouthWestEdge(int index)
        {
            if (North.Index != index || NorthEast.Index != index || East.Index != index)
                return false;

            if (South.Index != index || SouthEast.Index != index || SouthWest.Index != index)
                if (West.Index != index || NorthWest.Index != index)
                    return true;

            return false;
        }

        public bool IsNortEastEdge(TypeColor type)
        {
            if (South.Type != type || SouthWest.Type != type || West.Type != type)
                return false;

            if (East.Type != type || SouthEast.Type != type)
                if (North.Type != type || NorthWest.Type != type || NorthEast.Type != type)
                    return true;

            return false;
        }

        public bool IsNortEastEdge(int index)
        {
            if (South.Index != index || SouthWest.Index != index || West.Index != index)
                return false;

            if (East.Index != index || SouthEast.Index != index)
                if (North.Index != index || NorthWest.Index != index || NorthEast.Index != index)
                    return true;

            return false;
        }

        public bool IsSouthEastEdge(TypeColor type)
        {
            if (North.Type != type || NorthWest.Type != type || West.Type != type)
                return false;

            if (South.Type != type || SouthWest.Type != type)
                if (East.Type != type || NorthEast.Type != type || SouthEast.Type != type)
                {
                    return true;
                }
            return false;
        }

        public bool IsSouthEastEdge(int index)
        {
            if (North.Index != index || NorthWest.Index != index || West.Index != index)
                return false;

            if (South.Index != index || SouthWest.Index != index)
                if (East.Index != index || NorthEast.Index != index || SouthEast.Index != index)
                {
                    return true;
                }
            return false;
        }

        public bool IsNorthWestEdge(TypeColor type)
        {
            if (South.Type != type || SouthEast.Type != type || East.Type != type)
                return false;

            if (North.Type != type || NorthEast.Type != type || NorthWest.Type != type)
                if (West.Type != type || SouthWest.Type != type)
                    return true;

            return false;
        }

        public bool IsNorthWestEdge(int index)
        {
            if (South.Index != index || SouthEast.Index != index || East.Index != index)
                return false;

            if (North.Index != index || NorthEast.Index != index || NorthWest.Index != index)
                if (West.Index != index || SouthWest.Index != index)
                    return true;

            return false;
        }

        #region Border

        public bool IsNorthEastBorder()
        {
            return NorthEast.Index != Center.Index
                   && East.Index != NorthEast.Index
                   && North.Index != NorthEast.Index;
        }

        public bool IsNorthWestBorder()
        {
            return NorthWest.Index != Center.Index
                   && West.Index != NorthWest.Index
                   && North.Index != NorthWest.Index;
        }

        public bool IsSouthWestBorder()
        {
            return SouthWest.Index != Center.Index
                   && West.Index != SouthWest.Index
                   && South.Index != SouthWest.Index;
        }

        public bool IsSouthEastBorder()
        {
            return SouthEast.Index != Center.Index
            && East.Index != SouthEast.Index
            && South.Index != SouthEast.Index;
        }

        #endregion Border

        #region Border Texture

        public bool IsNorthEastBorderTexture()
        {
            return NorthEast.TextureIndex != Center.TextureIndex
                   && East.TextureIndex != NorthEast.TextureIndex
                   && North.TextureIndex != NorthEast.TextureIndex;
        }

        public bool IsNorthWestBorderTexture()
        {
            return NorthWest.TextureIndex != Center.TextureIndex
                   && West.TextureIndex != NorthWest.TextureIndex
                   && North.TextureIndex != NorthWest.TextureIndex;
        }

        public bool IsSouthWestBorderTexture()
        {
            return SouthWest.TextureIndex != Center.TextureIndex
                   && West.TextureIndex != SouthWest.TextureIndex
                   && South.TextureIndex != SouthWest.TextureIndex;
        }

        public bool IsSouthEastBorderTexture()
        {
            return SouthEast.TextureIndex != Center.TextureIndex
            && East.TextureIndex != SouthEast.TextureIndex
            && South.TextureIndex != SouthEast.TextureIndex;
        }

        #endregion Border Texture

        #endregion Location Methods
    }
}