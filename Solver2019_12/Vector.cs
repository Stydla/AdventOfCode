using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solver2019_12
{
  public class Vector
  {
    public int X { get; set; }
    public int Y { get; set; }
    public int Z { get; set; }

    public Vector(int x, int y, int z)
    {
      X = x;
      Y = y;
      Z = z;
    }

    public Vector(Vector p)
    {
      X = p.X;
      Y = p.Y;
      Z = p.Z;
    }

    public Vector Abs()
    {
      return new Vector(Math.Abs(X), Math.Abs(Y), Math.Abs(Z));
    }

    public static Vector operator -(Vector v1, Vector v2)
    {
      return new Vector(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
    }
    public static Vector operator +(Vector v1, Vector v2)
    {
      return new Vector(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
    }

    public static Vector operator / (Vector v1, Vector v2)
    {
      return new Vector(v1.X / (v2.X == 0 ? 1 : v2.X), v1.Y / (v2.Y == 0 ? 1 : v2.Y), v1.Z / (v2.Z == 0 ? 1 : v2.Z));
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

