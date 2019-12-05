using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solver2019_05.Instructions
{
  public class InstrInput : BaseInstruction
  {
    public override int OpCode => 3;

    public override int ParamCount => 1;

    public override void Execute(Processor proc, Input input, Output output, InstructionParams instructParams)
    {
      SetParam(0, input.Value, proc, instructParams);
      proc.InstructionPointer += 2;
    }
  }
}
