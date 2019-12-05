using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solver2019_05.Instructions
{
  public class InstrOutput : BaseInstruction
  {
    public override int OpCode => 4;

    public override int ParamCount => 1;

    public override void Execute(Processor proc, Input input, Output output, InstructionParams instructParams)
    {
      output.Value = GetParam(0, proc, instructParams);
      proc.InstructionPointer += 2;
    }
  }
}
