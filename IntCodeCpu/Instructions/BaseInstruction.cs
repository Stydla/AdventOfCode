using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntCodeCpu.Instructions
{
  public abstract class BaseInstruction
  {
    public abstract int OpCode { get; }
    public abstract int ParamCount { get; }
    public abstract eProcState Execute(Processor proc, Input input, Output output, InstructionParams instructParams);
    public virtual InstructionParams Parse(List<long> programData, int pos)
    {
      InstructionParams ip = new InstructionParams();
      ip.OpCode = programData[pos] % 100;

      string tmpModes = (programData[pos] / 100).ToString();
      tmpModes = tmpModes.PadLeft(ParamCount, '0');
      for(int i = ParamCount - 1; i >=0; i--)
      {
        ip.ParamsMode.Add(tmpModes[i] - '0');
      }
      for (int i = pos + 1; i < pos + ParamCount + 1; i++)
      {
        ip.Values.Add(programData[i]);
      }
      return ip;
    }
    protected long GetParam(int index, Processor proc, InstructionParams instructParams)
    {
      int paramMode = instructParams.ParamsMode[index];
      switch (paramMode)
      {
        case 0:
          return proc.ProgramData[(int)proc.ProgramData[(int)proc.InstructionPointer + 1 + index]];
        case 1: 
          return proc.ProgramData[(int)proc.InstructionPointer + 1 + index];
        case 2:
          return proc.ProgramData[(int)proc.RelativeBase + (int)instructParams.Values[index]];
        default:
          throw new Exception($"Invalid Param Mode: {paramMode}");
      }
    }
    protected void SetParam(int index, long value, Processor proc, InstructionParams instructParams)
    {
      int paramMode = instructParams.ParamsMode[index];
      switch (paramMode)
      {
        case 0:
          proc.ProgramData[(int)proc.ProgramData[(int)proc.InstructionPointer + 1 + index]] = value;
          break;
        case 1:
          proc.ProgramData[(int)proc.InstructionPointer + 1 + index] = value;
          break;
        case 2:
          proc.ProgramData[(int)proc.RelativeBase + (int)instructParams.Values[index]] = value;
          break;
        default:
          throw new Exception($"Invalid Param Mode: {paramMode}");
      }

    }
  }

  public enum eProcState
  {
    Halt,
    Continue
  }
}
