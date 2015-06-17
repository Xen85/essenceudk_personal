using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace EssenceUDK.Platform.DataTypes
{
    /// <summary>
    /// Point coordinates in 2d demensial decard system
    /// </summary>
    public struct Point2D : IPoint2D, IComparable, IComparable<Point2D>
    {
        public int X
        {
            get { return _X; }
            set { _X = value; }
        } private int _X;

        public int Y
        {
            get { return _Y; }
            set { _Y = value; }
        } private int _Y;

        public static readonly Point2D Zero = new Point2D(0, 0);

        public Point2D(int x, int y)
        {
            _X = x;
            _Y = y;
        }

        public Point2D(short x, short y) : this((int)x, (int)y)
        {
        }

        public Point2D(IPoint2D p) : this(p.X, p.Y)
        {
        }


        public override string ToString()
        {
            return String.Format("({0}, {1})", X, Y);
        }

        public static Point2D Parse(string value)
        {
            int start = value.IndexOf('(');
            int end = value.IndexOf(',', start + 1);
            string param1 = value.Substring(start + 1, end - (start + 1)).Trim();

            start = end;
            end = value.IndexOf(')', start + 1);
            string param2 = value.Substring(start + 1, end - (start + 1)).Trim();

            return new Point2D(Convert.ToInt16(param1), Convert.ToInt16(param2));
        }

        public override bool Equals(object o)
        {
            if (o == null || !(o is Point2D)) return false;
            Point2D p = (Point2D)o;
            return X == p._X && Y == p._Y;
        }

        public override int GetHashCode()
        {
            return (int)_X << 16 + (int)_Y; //X ^ Y;
        }

        public int CompareTo(Point2D other)
        {
            var v = (_X.CompareTo(other.X));
            if (v == 0)
                v = (_Y.CompareTo(other.Y));
            return v;
        }

        public int CompareTo(object other)
        {
            if (other is Point2D)
                return this.CompareTo((Point2D)other);
            else if (other == null)
                return -1;
            throw new ArgumentException();
        }

        public static bool operator ==(Point2D l, Point2D r)
        {
            return l._X == r._X && l._Y == r._Y;
        }

        public static bool operator !=(Point2D l, Point2D r)
        {
            return l._X != r._X || l._Y != r._Y;
        }

        public static bool operator >(Point2D l, Point2D r)
        {
            return l._X > r._X && l._Y > r._Y;
        }

        public static bool operator <(Point2D l, Point2D r)
        {
            return l._X < r._X && l._Y < r._Y;
        }

        public static bool operator >=(Point2D l, Point2D r)
        {
            return l._X >= r._X && l._Y >= r._Y;
        }

        public static bool operator <=(Point2D l, Point2D r)
        {
            return l._X <= r._X && l._Y <= r._Y;
        }

        public static void Serialize(BinaryWriter writer, Point2D value)
        {
            writer.Write((byte)1);     // version
            writer.Write((Int16)value._X);
            writer.Write((Int16)value._Y);
        }

        public static Point2D Deserialize(BinaryReader reader)
        {
            var version = reader.ReadByte();
            switch (version) {
                case 1: return new Point2D(
                        reader.ReadInt16(),  // X
                        reader.ReadInt16()); // Y
                default: throw new Exception("Unknown version");
            }
            return Point2D.Zero;
        }
    }

    /// <summary>
    /// Point coordinates in 3d demensial decard system
    /// </summary>
    public struct Point3D : IPoint2D, IComparable, IComparable<Point3D>
    {
        public int X
        {
            get { return _X; }
            set { _X = value; }
        } private int _X;

        public int Y
        {
            get { return _Y; }
            set { _Y = value; }
        } private int _Y;

        public int Z
        {
            get { return _Z; }
            set { _Z = value; }
        } private int _Z;

        public static readonly Point3D Zero = new Point3D(0, 0, 0);

        public Point3D(int x, int y, int z)
        {
            _X = x;
            _Y = y;
            _Z = z;
        }

        public Point3D(short x, short y, short z) : this((int)x, (int)y, (int)z)
        {
            
        }

        public Point3D(Point3D p): this(p._X, p._Y, p._Z)
        {
        }


        public override string ToString()
        {
            return String.Format("({0}, {1}, {2})", X, Y, Z);
        }

        public static Point3D Parse(string value)
        {
            int start = value.IndexOf('(');
            int end = value.IndexOf(',', start + 1);
            string param1 = value.Substring(start + 1, end - (start + 1)).Trim();

            start = end;
            end = value.IndexOf(',', start + 1);
            string param2 = value.Substring(start + 1, end - (start + 1)).Trim();

            start = end;
            end = value.IndexOf(')', start + 1);
            string param3 = value.Substring(start + 1, end - (start + 1)).Trim();

            return new Point3D(Convert.ToInt16(param1), Convert.ToInt16(param2), Convert.ToInt16(param3));
        }

        public override bool Equals(object o)
        {
            if (o == null || !(o is Point3D)) return false;
            Point3D p = (Point3D)o;
            return X == p._X && Y == p._Y && Z == p._Z;
        }

        public override int GetHashCode()
        {
            return (int)_X << 22 + (int)_Y << 11 + (int)_Z; //X ^ Y ^ Z;
        }

        public int CompareTo(Point3D other)
        {
            var v = (_X.CompareTo(other.X));
            if (v == 0)
                v = (_Y.CompareTo(other.Y));
            if (v == 0)
                v = (_Z.CompareTo(other.Z));
            return v;
        }

        public int CompareTo(object other)
        {
            if (other is Point3D)
                return this.CompareTo((Point3D)other);
            else if (other == null)
                return -1;
            throw new ArgumentException();
        }

        public static bool operator ==(Point3D l, Point3D r)
        {
            return l._X == r._X && l._Y == r._Y && l._Z == r._Z;
        }

        public static bool operator !=(Point3D l, Point3D r)
        {
            return l._X != r._X || l._Y != r._Y || l._Z != r._Z;
        }
        /*
		public static bool operator > (Point3D l, Point3D r)
		{
			return l._X > r._X && l._Y > r._Y;
		}

		public static bool operator < (Point3D l, Point3D r)
		{
			return l._X < r._X && l._Y < r._Y;
		}

		public static bool operator >= (Point3D l, Point3D r)
		{
			return l._X >= r._X && l._Y >= r._Y;
		}

		public static bool operator <= (Point3D l, Point3D r)
		{
			return l._X <= r._X && l._Y <= r._Y;
		}
        */
        public static void Serialize(BinaryWriter writer, Point3D value)
        {
            writer.Write((byte)1);     // version
            writer.Write((Int16)value._X);
            writer.Write((Int16)value._Y);
            writer.Write((Int16)value._Z);
        }

        public static Point3D Deserialize(BinaryReader reader)
        {
            var version = reader.ReadByte();
            switch (version)
            {
                case 1: return new Point3D(
                    reader.ReadInt16(),  // X
                    reader.ReadInt16(),  // Y
                    reader.ReadInt16()); // Z
                default: throw new Exception("Unknown version");
            }
            return Point3D.Zero;
        }
    }

    public struct Rectangle2D
    {
        public Point2D Start
        {
            get { return _Start; }
            set { _Start = value; }
        } private Point2D _Start;

        public Point2D End
        {
            get { return _End; }
            set { _End = value; }
        } private Point2D _End;

        public int X1
        {
            get { return _Start.X; }
            set { _Start.X = value; }
        }

        public int Y1
        {
            get { return _Start.Y; }
            set { _Start.Y = value; }
        }

        public int X2
        {
            get { return _End.X; }
            set { _End.X = value; }
        }

        public int Y2
        {
            get { return _End.Y; }
            set { _End.Y = value; }
        }

        public int Width
        {
            get { return (int)(_End.X - _Start.X); }
            set { _End.X = (int)(_Start.X + value); }
        }

        public int Height
        {
            get { return (int)(_End.Y - _Start.Y); }
            set { _End.Y = (int)(_Start.Y + value); }
        }

        public static readonly Rectangle2D Zero = new Rectangle2D(0, 0, 0, 0);

        public Rectangle2D(IPoint2D start, IPoint2D end)
        {
            _Start = new Point2D(start);
            _End = new Point2D(end);
        }

        public Rectangle2D(int x, int y, int width, int height) : this(new Point2D(x, y), new Point2D((int)(x + width), (int)(y + height)))
        {
        }

        public Rectangle2D(short x, short y, ushort width, ushort height) : this((int)x, (int)y, (int)width, (int)height)
        {
        }

        public bool Contains(IPoint2D p)
        {
            return (_Start.X <= p.X && _Start.Y <= p.Y && _End.X > p.X && _End.Y > p.Y);
            //return ( m_Start <= p && m_End > p );
        }

        public void MakeHold(Rectangle2D r)
        {
            if (r._Start.X < _Start.X)
                _Start.X = r._Start.X;
            if (r._Start.Y < _Start.Y)
                _Start.Y = r._Start.Y;
            if (r._End.X > _End.X)
                _End.X = r._End.X;
            if (r._End.Y > _End.Y)
                _End.Y = r._End.Y;
        }

        public static Rectangle2D Parse(string value)
        {
            int start = value.IndexOf('(');
            int end = value.IndexOf(',', start + 1);
            string param1 = value.Substring(start + 1, end - (start + 1)).Trim();

            start = end;
            end = value.IndexOf(',', start + 1);
            string param2 = value.Substring(start + 1, end - (start + 1)).Trim();

            start = end;
            end = value.IndexOf(',', start + 1);
            string param3 = value.Substring(start + 1, end - (start + 1)).Trim();

            start = end;
            end = value.IndexOf(')', start + 1);
            string param4 = value.Substring(start + 1, end - (start + 1)).Trim();

            return new Rectangle2D(Convert.ToInt16(param1), Convert.ToInt16(param2), Convert.ToInt16(param3), Convert.ToInt16(param4));
        }

        public override string ToString()
        {
            return String.Format("({0}, {1})+({2}, {3})", X1, Y1, Width, Height);
        }

        public static void Serialize(BinaryWriter writer, Rectangle2D value)
        {
            writer.Write((byte)2);     // version

            writer.Write((short)value._Start.X);
            writer.Write((short)value._Start.Y);
            writer.Write((short)value._End.X);
            writer.Write((short)value._End.Y);
        }

        public static Rectangle2D Deserialize(BinaryReader reader)
        {
            var version = reader.ReadByte();
            switch (version)
            {
                case 2: return new Rectangle2D(
                        new Point2D(reader.ReadInt16(),   // Start.X
                                    reader.ReadInt16()),  // Start.Y
                        new Point2D(reader.ReadInt16(),   // End.X
                                    reader.ReadInt16())); // End.Y
                case 1: return new Rectangle2D(
                        Point2D.Deserialize(reader),  // Start
                        Point2D.Deserialize(reader)); // End
                default: throw new Exception("Unknown version");
            }
            return Rectangle2D.Zero;
        }
    }

    public struct Clipper2D : IClipper
    {
        private int   _X1, _Y1, _X2, _Y2;
        private int   _Width, _Height;

        public int X1 { get { return _X1; } set { _Width  = (int)(1 + _X2 - (_X1 = value)); } }
        public int X2 { get { return _X2; } set { _Width  = (int)(1 + (_X2 = value) - _X1); } }
        public int Y1 { get { return _Y1; } set { _Height = (int)(1 + _Y2 - (_Y1 = value)); } }
        public int Y2 { get { return _Y2; } set { _Height = (int)(1 + (_Y2 = value) - _Y1); } }
        public int Width  { get { return _Width; }  set { _X2 = (short)(_X1 + (_Width = value) - 1); } }
        public int Height { get { return _Height; } set { _Y2 = (short)(_Y1 + (_Height = value) - 1); } }

        public Clipper2D(int x1, int y1, uint width, uint height)
        {
            _X1 = x1;
            _Y1 = y1;
            _Y2 = _X2 = 0;
            _Width = _Height = 0; 
            Width  = (int)width;
            Height = (int)height;
        }

        public Clipper2D(int x1, int y1, int x2, int y2)
        {
            _X1 = x1;
            _Y1 = y1;
            _Y2 = _X2 = 0;
            _Width = _Height = 0; 
            X2 = x2;
            Y2 = y2;
        }
    }
}
