using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solver2019_12
{
  public class Point
  {
    public int X { get; set; }
    public int Y { get; set; }
    public int Z { get; set; }

    public Point(int x, int y, int z)
    {
      X = x;
      Y = y;
      Z = z;
    }

    public Point ( Point p)
    {
      X = p.X;
      Y = p.Y;
      Z = p.Z;
    }

    public static Point Parse(string input)
    {
      string [] vals =  input.Replace("x=", "").Replace("y=", "").Replace("z=", "").Replace("<", "").Replace(">", "").Split(',');
      return new Point(int.Parse(vals[0]),
        int.Parse(vals[1]),
        int.Parse(vals[2]));
    }

    public static Vector operator -(Point p1, Point p2)
    {
      return new Vector(p1.X - p2.X, p1.Y - p2.Y, p1.Z - p2.Z);
    }

    public static Point operator +(Point p1, Vector v1)
    {
      return new Point(p1.X + v1.X, p1.Y + v1.Y, p1.Z + v1.Z);
    }


    public static Point operator -(Point p1, Vector v1)
    {
      return new Point(p1.X - v1.X, p1.Y - v1.Y, p1.Z - v1.Z);
    }

    public int Energy
    {
      get
      {
        return Math.Abs(X) + Math.Abs(Y) + Math.Abs(Z);
      }
    }

    public override string ToString()
    {
      return $"{X},{Y},{Z}";
    }
  }
}
