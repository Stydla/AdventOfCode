using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace AoCLib
{
  public class Bound
  {

    public int Min { get; set; }
    public int Max { get; set; }

    public Bound(int min, int max)
    {
      Min = min;
      Max = max;
    }

    public Bound(Bound original)
    {
      Min = original.Min;
      Max = original.Max;
    }


    public override string ToString()
    {
      return $"<{Min};{Max}>";
    }

    public override int GetHashCode()
    {
      return (int)Min + 256 * (int)Max;
    }

    public override bool Equals(object obj)
    {
      if(obj is Bound b) 
      {
        return Min == b.Min && Max == b.Max;
      }
      return false;
    }
  }
}
