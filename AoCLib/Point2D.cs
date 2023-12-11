using AoCLib.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AoCLib
{
  public class Point2D
  {

    public int X {get; set; }
    public int Y { get; set; }

    public Point2D(int x, int y)
    {
      X = x;
      Y = y;
    }

    public Point2D(Point2D orig)
    {
      this.X = orig.X;
      this.Y = orig.Y;
    }

    public override string ToString()
    {
      return $"{X},{Y}";
    }

    public override bool Equals(object obj)
    {
      if(obj is Point2D p)
      {
        return p.X == X && p.Y == Y;
      }
      return false;
    }

    public override int GetHashCode()
    {
      return X * 65536 + Y;
    }

    public static int GetCustomHashCode(int x, int y)
    {
      return x * 65536 + y;
    }

    public static int GetCustomHashCode(Point2D point)
    {
      return GetCustomHashCode(point.X, point.Y);
    }

    public Point2D L()
    {
      return new Point2D(X - 1, Y);
    }

    public Point2D R()
    {
      return new Point2D(X + 1, Y);
    }

    public Point2D U()
    {
      return new Point2D(X, Y - 1);
    }

    public Point2D D()
    {
      return new Point2D(X, Y + 1);
    }

    public Point2D UR()
    {
      return new Point2D(X + 1, Y - 1);
    }

    public Point2D UL()
    {
      return new Point2D(X - 1, Y - 1);
    }

    public Point2D DR()
    {
      return new Point2D(X + 1, Y + 1);
    }

    public Point2D DL()
    {
      return new Point2D(X - 1, Y + 1);
    }

    public Point2D Move(EDirection4 dir)
    {
      switch (dir)
      {
        case EDirection4.UP:
          return U();
        case EDirection4.RIGHT:
          return R();
        case EDirection4.DOWN:
          return D();
        case EDirection4.LEFT:
          return L();
      }
      throw new Exception($"Missin direction {dir}");
    }

    public List<Point2D> GetNeightbours4()
    {
      return new List<Point2D>
      {
        U(), R(), D(), L()
      };
    }

    public List<Point2D> GetNeightbours8()
    {
      return new List<Point2D>
      {
        U(), UR(), R(), DR(), D(), DL(), L(), UL()
      };
    }

    public long ManhattanDistance(Point2D other)
    {
      return Math.Abs(X - other.X) + Math.Abs(Y - other.Y);
    }



  }

}
