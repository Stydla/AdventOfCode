using AoCLib.Enums;
using System;
using System.Collections.Generic;
using System.Data;
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


    public Point2D L(int cnt)
    {
      return new Point2D(X - cnt, Y);
    }

    public Point2D R(int cnt)
    {
      return new Point2D(X + cnt, Y);
    }

    public Point2D U(int cnt)
    {
      return new Point2D(X, Y - cnt);
    }

    public Point2D D(int cnt)
    {
      return new Point2D(X, Y + cnt);
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

    public Point2D Move(EDirection8 dir)
    {
      switch (dir)
      {
        case EDirection8.UP:
          return U();
        case EDirection8.UP_RIGHT:
          return UR();
        case EDirection8.RIGHT:
          return R();
        case EDirection8.DOWN_RIGHT:
          return DR();
        case EDirection8.DOWN:
          return D();
        case EDirection8.DOWN_LEFT:
          return DL();
        case EDirection8.LEFT:
          return L();
        case EDirection8.UP_LEFT:
          return UL();

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

    public Dictionary<EDirection4 ,Point2D> GetNeightboursDict4()
    {
      return new Dictionary<EDirection4, Point2D>
      {
        {EDirection4.UP, U()},
        {EDirection4.RIGHT, R()},
        {EDirection4.DOWN, D()},
        {EDirection4.LEFT, L()},
      };
    }

    public List<Point2D> GetNeightbours8()
    {
      return new List<Point2D>
      {
        U(), UR(), R(), DR(), D(), DL(), L(), UL()
      };
    }

    public HashSet<Point2D> GetNeighboursAtDistance(int distance)
    {
      HashSet<Point2D> points = new HashSet<Point2D>();
      for(int i = -distance; i <= distance; i++)
      {
        int x = X + i;
        int y1 = Y + (distance - Math.Abs(i));
        Point2D p1 = new Point2D(x, y1);
        if (!points.Contains(p1)) 
        { 
          points.Add(p1);
        }

        int y2 = Y + (-distance + Math.Abs(i));
        Point2D p2 = new Point2D(x, y2);
        if (!points.Contains(p2))
        {
          points.Add(p2);
        }
      }
      return points;
    }

    public long ManhattanDistance(Point2D other)
    {
      return Math.Abs(X - other.X) + Math.Abs(Y - other.Y);
    }

    public Point2D Move(EDirection4 dir, int cnt)
    {
      switch (dir)
      {
        case EDirection4.UP:
          return U(cnt);
        case EDirection4.RIGHT:
          return R(cnt);
        case EDirection4.DOWN:
          return D(cnt);
        case EDirection4.LEFT:
          return L(cnt);
      }
      throw new Exception($"Missin direction {dir}");
    }

    public static bool operator ==(Point2D a, Point2D b)
    {
      if (ReferenceEquals(a, b))
        return true;
      if (ReferenceEquals(a, null))
        return false;
      if (ReferenceEquals(b, null))
        return false;
      return a.Equals(b);
    }

    public static bool operator !=(Point2D a, Point2D b)
    {
      return !(a == b);
    }
  }

}
