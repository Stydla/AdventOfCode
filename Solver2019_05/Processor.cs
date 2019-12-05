using Solver2019_05.Instructions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solver2019_05
{
  public class Processor
  {
    public int InstructionPointer;
    public List<int> ProgramData;
    public Input Input;
    public Output Output;

    private Dictionary<int, BaseInstruction> Instructions = new Dictionary<int, BaseInstruction>()
    {
      { 1, new InstrAdd() },
      { 2, new InstrMultiply() },
      { 3, new InstrInput() },
      { 4, new InstrOutput() },
      { 5, new InstrJumpIfTrue() },
      { 6, new InstrJumpIfFalse() },
      { 7, new InstrLessThen() },
      { 8, new InstrEquals() },
    };

    public Processor(List<int> programData, Input input, Output output)
    {
      InstructionPointer = 0;
      ProgramData = programData;
      Input = input;
      Output = output;
    }
    public void Run()
    {
      int opCode = GetOpCode(ProgramData[InstructionPointer]);
      while(opCode != 99)
      {
        if(!Instructions.ContainsKey(opCode))
        {
          throw new Exception($"Instruction {opCode} not implemented!");
        }
        var instruction = Instructions[opCode];
        var instructionParams = instruction.Parse(ProgramData, InstructionPointer);
        instruction.Execute(this, Input, Output, instructionParams);
        opCode = GetOpCode(ProgramData[InstructionPointer]);
      }
    }

    private int GetOpCode(int value)
    {
      return value % 100;
    }

    
  }
}
