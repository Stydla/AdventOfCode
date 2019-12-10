namespace Solver2019_10
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

    public static Point operator + (Point p, Vector v)
    {
      return new Point(p.X + v.X, p.Y + v.Y);
    }

    public static Point operator - (Point p, Vector v)
    {
      return new Point(p.X - v.X, p.Y - v.Y);
    }

    

    public static Vector operator -(Point p1, Point p2)
    {
      return new Vector(p1.X - p2.X, p1.Y - p2.Y);
    }

    public static Vector operator +(Point p1, Point p2)
    {
      return new Vector(p1.X + p2.X, p1.Y + p2.Y);
    }

    public override string ToString()
    {
      return $"{X},{Y}";
    }

  }
}