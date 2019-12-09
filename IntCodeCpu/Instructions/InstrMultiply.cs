using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntCodeCpu.Instructions
{
  public class InstrMultiply : BaseInstruction
  {
    public override int OpCode => 2;

    public override int ParamCount => 3;

    public override eProcState Execute(Processor proc, Input input, Output output, InstructionParams instructParams)
    {
      long value1 = GetParam(0, proc, instructParams);
      long value2 = GetParam(1, proc, instructParams);
      long res = value1 * value2;
      SetParam(2, res, proc, instructParams);
      proc.InstructionPointer += 4;
      return eProcState.Continue;
    }
  }
}
