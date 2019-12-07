﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntCodeCpu.Instructions
{
  public class InstrOutput : BaseInstruction
  {
    public override int OpCode => 4;

    public override int ParamCount => 1;

    public override eProcState Execute(Processor proc, Input input, Output output, InstructionParams instructParams)
    {
      output.Value = GetParam(0, proc, instructParams);
      proc.InstructionPointer += 2;
      return eProcState.Continue;
    }
  }
}