using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solver2019_05.Instructions
{
  public abstract class BaseInstruction
  {
    public abstract int OpCode { get; }
    public abstract int ParamCount { get; }
    public abstract void Execute(Processor proc, Input input, Output output, InstructionParams instructParams);
    public virtual InstructionParams Parse(List<int> programData, int pos)
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
    protected int GetParam(int index, Processor proc, InstructionParams instructParams)
    {
      if (instructParams.ParamsMode[index] == 0)
      {
        return proc.ProgramData[proc.ProgramData[proc.InstructionPointer + 1 + index]];
      }
      else
      {
        return proc.ProgramData[proc.InstructionPointer + 1 + index];
      }
    }
    protected void SetParam(int index, int value, Processor proc, InstructionParams instructParams)
    {
      if (instructParams.ParamsMode[index] == 0)
      {
        proc.ProgramData[proc.ProgramData[proc.InstructionPointer + 1 + index]] = value;
      }
      else
      {
        proc.ProgramData[proc.InstructionPointer + 1 + index] = value;
      }
    }
  }
}
