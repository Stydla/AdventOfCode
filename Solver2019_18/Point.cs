using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solver2019_18
{
  public class Point
  {
    public int X { get; }
    public int Y { get; }

    public Point(int x, int y)
    {
      X = x;
      Y = y;
    }

    public static  bool operator == (Point p1, Point p2)
    {
      return p1.X == p2.X && p1.Y == p2.Y;
    }
    public static  bool operator !=(Point p1, Point p2)
    {
      return !(p1 == p2);
    }

    public override string ToString()
    {
      return $"{X},{Y}";
    }

    public override bool Equals(object obj)
    {
      return obj is Point point &&
             X == point.X &&
             Y == point.Y;
    }

    public override int GetHashCode()
    {
      return base.GetHashCode();
    }
  }
}
