using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntCodeCpu
{
  public class Input
  {

    private List<int> Values { get; } = new List<int>();
    public int Pos { get; private set; }
    public Input()
    {
      Pos = 0;
    }

    public int GetNext()
    {
      return Values[Pos++];
    }

    public bool HasNext()
    {
      return Values.Count > Pos;
    }

    public void Add(int value)
    {
      Values.Add(value);
    }

  }
}
