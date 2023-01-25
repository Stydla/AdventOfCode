using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoCLib
{
  public class IntervalPoint
  {

    public EIntervalPointType Type { get; set; }
    public long Value { get; set; }

    public IntervalPoint(long value, EIntervalPointType type)
    {
      Value = value;
      Type = type;
    }
  }

  public enum EIntervalPointType
  {
    Start,
    End
  }
}
