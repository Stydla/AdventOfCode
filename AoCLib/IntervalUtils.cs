using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoCLib
{
  public class IntervalUtils
  {

    public static List<Interval> MergeIntervals(List<Interval> intervals)
    {
      List<IntervalPoint> ips = new List<IntervalPoint>();
      foreach (Interval interval in intervals)
      {
        ips.AddRange(interval.Points);
      }
      ips.Sort((x, y) =>
      {
        long val1 = x.Type == EIntervalPointType.Start ? x.Value : x.Value + 1;
        long val2 = y.Type == EIntervalPointType.Start ? y.Value : y.Value + 1;
        if (val1 < val2)
        {
          return -1;
        }
        else if (val1 > val2)
        {
          return 1;
        }
        else
        {
          if (x.Type == EIntervalPointType.Start)
          {
            if (y.Type == EIntervalPointType.Start)
            {
              return 0;
            }
            else
            {
              return -1;
            }
          }
          else
          {
            if (y.Type == EIntervalPointType.Start)
            {
              return 1;
            }
            else
            {
              return 0;
            }
          }
        }
      });

      List<Interval> ret = new List<Interval>();

      int depth = 0;
      long start = long.MinValue;
      long end = long.MaxValue;
      foreach (IntervalPoint ip in ips)
      {
        if (depth == 0)
        {
          start = ip.Value;
        }
        if (ip.Type == EIntervalPointType.Start)
        {
          depth++;
        }
        else
        {
          depth--;
        }
        if (depth == 0)
        {
          end = ip.Value;
          ret.Add(new Interval(start, end));
        }

      }

      return ret;
    }

  }
}
