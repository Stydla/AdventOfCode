using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntCodeCpu.Instructions
{
  public class InstrJumpIfFalse : BaseInstruction
  {
    public override int OpCode => 6;

    public override int ParamCount => 2;

    public override eProcState Execute(Processor proc, Input input, Output output, InstructionParams instructParams)
    {
      int value1 = GetParam(0, proc, instructParams);
      if(value1 == 0)
      {
        int value2 = GetParam(1, proc, instructParams);
        proc.InstructionPointer = value2;
      } else
      {
        proc.InstructionPointer += 3;
      }
      return eProcState.Continue;
    }
  }
}