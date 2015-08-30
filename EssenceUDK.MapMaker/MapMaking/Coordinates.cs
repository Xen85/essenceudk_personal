namespace EssenceUDK.MapMaker.MapMaking
{
    public struct Coordinates
    {
        private readonly int[] _array;
        public int Center { get { return _array[(int)Directions.Center]; } }
        public int North { get { return _array[(int)Directions.North]; } }
        public int South { get { return _array[(int)Directions.South]; } }
        public int East { get { return _array[(int)Directions.East]; } }
        public int West { get { return _array[(int)Directions.West]; } }
        public int NorthEast { get { return _array[(int)Directions.NorthEast]; } }
        public int NorthWest { get { return _array[(int)Directions.NorthWest]; } }
        public int SouthEast { get { return _array[(int)Directions.SouthEast]; } }
        public int SouthWest { get { return _array[(int)Directions.SouthWest]; } }
        public int X { get { return _array[9]; } }
        public int Y { get { return _array[10]; } }

        public int[] List { get { return _array; } }

        public Coordinates(int shiftX, int shiftY, int x, int y, int stride, int lenght)
        {
            _array = new int[11];
            _array[(int)Directions.Center] = MapMaker.CalculateZone(x, y, stride);
            _array[(int)Directions.North] = MapMaker.CalculateZone(x, y - shiftY, stride);
            _array[(int)Directions.South] = MapMaker.CalculateZone(x, y + shiftY, stride);
            _array[(int)Directions.East] = MapMaker.CalculateZone(x + shiftX, y, stride);
            _array[(int)Directions.West] = MapMaker.CalculateZone(x - shiftX, y, stride);
            _array[(int)Directions.NorthWest] = MapMaker.CalculateZone(x - shiftX, y - shiftY, stride);
            _array[(int)Directions.NorthEast] = MapMaker.CalculateZone(x + shiftX, y - shiftY, stride);
            _array[(int)Directions.SouthWest] = MapMaker.CalculateZone(x - shiftX, y + shiftY, stride);
            _array[(int)Directions.SouthEast] = MapMaker.CalculateZone(x + shiftX, y + shiftY, stride);
            _array[9] = x;
            _array[10] = y;
            for (int i = 0; i < List.Length; i++)
            {
                if (_array[i] < 0 || _array[i] > lenght)
                    _array[i] = 0;
            }
        }
    }
}