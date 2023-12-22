using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoCLib
{
  public class Bounds2D
  {



    public Bound X { get; set; }
    public Bound Y { get; set; }

    public Bounds2D(int minX, int maxX, int minY, int maxY) : this(new Bound(minX, maxX), new Bound(minY, maxY))
    {
    }
    public Bounds2D(Bound x, Bound y)
    {
      X = x;
      Y = y;
    }

    public Bounds2D(Bounds2D original)
    {
      X = new Bound(original.X);
      Y = new Bound(original.Y);
    }

    public Bounds2D(List<Point2D> points)
    {
      X = new Bound(points.Min(x => x.X), points.Max(x=>x.X));
      Y = new Bound(points.Min(x => x.Y), points.Max(x => x.Y));
    }

    public override string ToString()
    {
      return $"X∊{X} Y:{Y}";
    }

    public override int GetHashCode()
    {
      return X.GetHashCode() ^ (256 * 256 * Y.GetHashCode());
    }

    public override bool Equals(object obj)
    {
      if(obj is Bounds2D b) 
      { 
        return X.Equals(b.X) && Y.Equals(b.Y);
      }
      return false;
    }

  }
}
