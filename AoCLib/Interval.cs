using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoCLib
{
  public class Interval
  {

    public List<IntervalPoint> Points { get; set; } = new List<IntervalPoint>();
    public IntervalPoint StartPoint
    {
      get
      {
        return Points[0];
      }
    }
    public IntervalPoint EndPoint
    {
      get
      {
        return Points[1];
      }
    }

    public Interval(long start, long end)
    {
      Points.Add(new IntervalPoint(start, EIntervalPointType.Start));
      Points.Add(new IntervalPoint(end, EIntervalPointType.End));
    }


    internal long GetLength()
    {
      return Points[1].Value - Points[0].Value + 1;
    }

    public bool Contains(long number)
    {
      if (Points[0].Value <= number && Points[1].Value >= number)
      {
        return true;
      }
      return false;
    }

    public override string ToString()
    {
      return $"[{Points[0].Value},{Points[1].Value}]";
    }
  }
}
