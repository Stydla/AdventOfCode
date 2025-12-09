using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoCLib
{
  public class PointUtils
  {

    public static long GetArea(Point2D p1, Point2D p2)
    {
      return
        (Math.Abs(p1.X - p2.X) + 1L) *
        (Math.Abs(p1.Y - p2.Y) + 1L);
    }
   
  }
}
