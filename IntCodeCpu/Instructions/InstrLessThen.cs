using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntCodeCpu.Instructions
{
  public class InstrLessThen : BaseInstruction
  {
    public override int OpCode => 7;

    public override int ParamCount => 3;

    public override eProcState Execute(Processor proc, Input input, Output output, InstructionParams instructParams)
    {
      int value1 = GetParam(0, proc, instructParams);
      int value2 = GetParam(1, proc, instructParams);
      if (value1 < value2)
      {
        SetParam(2, 1, proc, instructParams);
      } else
      {
        SetParam(2, 0, proc, instructParams);
      }
      proc.InstructionPointer += 4;
      return eProcState.Continue;
    }
  }
}
