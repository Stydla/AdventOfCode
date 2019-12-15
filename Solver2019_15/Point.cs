using System;
using static Solver2019_15.Direction;

namespace Solver2019_15
{
  public class Point
  {
    public int X { get; set; }
    public int Y { get; set; }

    public Point(int x, int y)
    {
      X = x;
      Y = y;
    }

    public Point(Point p)
    {
      X = p.X;
      Y = p.Y;
    }

    public Point Move(eDirection dir)
    {
      switch (dir)
      {
        case eDirection.North:
          return new Point(X, Y - 1);
        case eDirection.South:
          return new Point(X, Y + 1);
        case eDirection.West:
          return new Point(X - 1, Y);
        case eDirection.East:
          return new Point(X + 1, Y);
        default:
          throw new Exception("Direction not exists");
      }
    }

    public override string ToString()
    {
      return $"{X},{Y}";
    }
  }
}