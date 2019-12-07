using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntCodeCpu
{
  public class InstructionParams
  {
    public int OpCode { get; set; }
    public List<int> ParamsMode { get; set; } = new List<int>();
    public List<int> Values { get; set; } = new List<int>();
    public InstructionParams()
    {
    }
  }
}
