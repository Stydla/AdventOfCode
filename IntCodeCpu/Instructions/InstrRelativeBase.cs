using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntCodeCpu.Instructions
{
  public class InstrRelativeBase : BaseInstruction
  {
    public override int OpCode => 9;

    public override int ParamCount => 1;

    public override eProcState Execute(Processor proc, Input input, Output output, InstructionParams instructParams)
    {
      long value1 = GetParam(0, proc, instructParams);
      proc.RelativeBase = (int)proc.RelativeBase + (int)value1;
      proc.InstructionPointer += 2;
      return eProcState.Continue;
    }
  }
}
