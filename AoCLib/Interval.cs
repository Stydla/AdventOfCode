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

    public bool Overlapping(Interval intr)
    {
      if (StartPoint.Value >= intr.StartPoint.Value && StartPoint.Value <= intr.EndPoint.Value) return true;
      if (EndPoint.Value >= intr.StartPoint.Value && EndPoint.Value <= intr.EndPoint.Value) return true;

      if (StartPoint.Value < intr.StartPoint.Value && EndPoint.Value >= intr.StartPoint.Value) return true;
      if (EndPoint.Value > intr.EndPoint.Value && StartPoint.Value <= intr.EndPoint.Value) return true;

      return false;
       
    }

    public long GetMinOverlap(Interval intr)
    {
      if(intr.Contains(StartPoint.Value))
      {
        return StartPoint.Value;
      } else
      {
        return intr.StartPoint.Value;
      }
      
    }

    public Interval Intersect(Interval other)
    {
      if (!Overlapping(other)) return null;

      if(other.Contains(StartPoint.Value))
      {
        if(other.Contains(EndPoint.Value))
        {
          return new Interval(StartPoint.Value, EndPoint.Value);
        } else
        {
          return new Interval(StartPoint.Value, other.EndPoint.Value);
        }
      } else
      {
        if (other.Contains(EndPoint.Value))
        {
          return new Interval(other.StartPoint.Value, EndPoint.Value);
        }
        else
        {
          return new Interval(other.StartPoint.Value, other.EndPoint.Value);
        }
      }


      //if(StartPoint.Value < other.StartPoint.Value)
      //{
      //  if(EndPoint.Value >= other.StartPoint.Value && EndPoint.Value <= other.EndPoint.Value)
      //  {
      //    return new Interval(other.StartPoint.Value, )
      //  }

      //}

      //if (StartPoint.Value >= other.StartPoint.Value && StartPoint.Value <= other.EndPoint.Value)
      //{
      //  return new Interval(StartPoint.Value, Math.Min(EndPoint.Value, other.EndPoint.Value));
      //}

    }
  }
}
