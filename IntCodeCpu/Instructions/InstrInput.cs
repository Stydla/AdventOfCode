using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntCodeCpu.Instructions
{
  public class InstrInput : BaseInstruction
  {
    public override int OpCode => 3;

    public override int ParamCount => 1;

    public override eProcState Execute(Processor proc, Input input, Output output, InstructionParams instructParams)
    {
      if (!input.HasNext())
      {
        return eProcState.Halt;
      }
      SetParam(0, input.GetNext(), proc, instructParams);
      proc.InstructionPointer += 2;
      return eProcState.Continue;
    }
  }
}
