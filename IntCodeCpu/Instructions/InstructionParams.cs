using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntCodeCpu
{
  public class InstructionParams
  {
    public long OpCode { get; set; }
    public List<int> ParamsMode { get; set; } = new List<int>();
    public List<long> Values { get; set; } = new List<long>();
    public InstructionParams()
    {
    }
  }
}
