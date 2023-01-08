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



  }

}
