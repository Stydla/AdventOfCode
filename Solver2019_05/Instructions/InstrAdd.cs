using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solver2019_05.Instructions
{
  public class InstrAdd : BaseInstruction
  {
    public override int OpCode => 1;

    public override int ParamCount => 3;

    public override void Execute(Processor proc, Input input, Output output, InstructionParams instructParams)
    {
      int value1 = GetParam(0, proc, instructParams);
      int value2 = GetParam(1, proc, instructParams);
      int res = value1 + value2;
      SetParam(2, res, proc, instructParams);
      proc.InstructionPointer += 4;
    }
  }
}
