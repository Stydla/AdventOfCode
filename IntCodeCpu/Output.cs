using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntCodeCpu
{
  public class Output
  {
    private List<long> Values { get; } = new List<long>();
    public int Pos { get; private set; }
    public Output()
    {
      Pos = 0;
    }

    public long GetNext()
    {
      return Values[Pos++];
    }

    public bool HasNext()
    {
      return Values.Count > Pos;
    }

    public void Add(long value)
    {
      Values.Add(value);
    }

    public long LastValue()
    {
      return Values[Values.Count - 1];
    }

    public string Print()
    {
      return string.Join(",", Values);
    }
  }
}
