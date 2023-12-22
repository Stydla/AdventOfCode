using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoCLib
{
  public class Bounds3D
  {

    public Bound X { get; set; }
    public Bound Y { get; set; }
    public Bound Z { get; set; }

    public Bounds3D(int minX, int maxX, int minY, int maxY, int minZ, int maxZ) : this(new Bound(minX, maxX), new Bound(minY, maxY), new Bound(minZ, maxZ))
    {
    }
    public Bounds3D(Bound x, Bound y, Bound z)
    {
      X = x;
      Y = y;
      Z = z;
    }

    public Bounds3D(Bounds3D original)
    {
      X = new Bound(original.X);
      Y = new Bound(original.Y);
      Z = new Bound(original.Z);
    }

    public Bounds3D(List<Point3D> points)
    {
      X = new Bound(points.Min(x => x.X), points.Max(x => x.X));
      Y = new Bound(points.Min(x => x.Y), points.Max(x => x.Y));
      Z = new Bound(points.Min(x => x.Z), points.Max(x => x.Z));
    }

    public override string ToString()
    {
      return $"X∊{X} Y∊{Y} Z∊{Z}";
    }

    public override int GetHashCode()
    {
      return X.GetHashCode() ^ (128 * Y.GetHashCode()) ^ (128 * 128 * Z.GetHashCode());
    }

    public override bool Equals(object obj)
    {
      if (obj is Bounds3D b)
      {
        return X.Equals(b.X) && Y.Equals(b.Y) && Y.Equals(b.Y);
      }
      return false;
    }


  }
}
