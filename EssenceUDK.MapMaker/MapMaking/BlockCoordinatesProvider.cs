namespace EssenceUDK.MapMaker.MapMaking
{
    public class BlockCoordinatesProvider
    {
        private readonly int _width;
        private readonly int _height;
        private int _x;
        private int _y;
        private int _xLast;
        private int _yLast;

        private const int Block = 8;

        public BlockCoordinatesProvider(int width, int height, int minX, int minY)
        {
            _width = width;
            _height = height;
            _x = minX;
            _y = minY;
            _xLast = minX;
            _yLast = minY;
        }

        public bool HasNextCoord()
        {
            return _yLast < _height && _y < _height;
        }

        public void GetNext(out int x, out int y)
        {
            x = _x;
            y = _y;
            var yIncrementataEFinita = false;
            var xLimite = IncrementaX();
            if (!xLimite)
                return;
            if (YPenultimo())
            {
                IncriseLastX();
            }
            yIncrementataEFinita = IncrementaY();

            if (!yIncrementataEFinita || !XArrivatoAllaFine()) return;
            ResetX();

            IncriseLastY();
        }

        private bool IncrementaX()
        {
            if (_x < _xLast + Block && _x < _width)
            {
                _x++;
            }
            if (_x == _xLast + Block)
            {
                _x = _xLast;
                return true;
            }
            return false;
        }

        private bool IncrementaY()
        {
            if (_y < _yLast + Block && _y < _height)
            {
                _y++;
            }
            if (_y == _yLast + Block)
            {
                _y = _yLast;
                return true;
            }
            return false;
        }

        private bool XArrivatoAllaFine()
        {
            return _xLast == _width;
        }

        private void ResetX()
        {
            _x = 0;
            _xLast = 0;
        }

        private void IncriseLastY()
        {
            _yLast += Block;
            _y = _yLast;
        }

        private void IncriseLastX()
        {
            _xLast += Block;
        }

        private bool YPenultimo()
        {
            return _y % Block == Block - 1;
        }

        public int Progress { get { return _yLast; } }
    }
}