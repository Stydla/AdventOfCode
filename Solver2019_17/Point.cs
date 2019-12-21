using static Solver2019_17.Direction;

namespace Solver2019_17
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
      }
      throw new System.Exception($"Invalid dir {dir}");
    }

  }
}