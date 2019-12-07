using IntCodeCpu.Instructions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntCodeCpu
{
  public class Processor
  {
    public int InstructionPointer;
    public List<int> ProgramData;
    public Input Input = new Input();
    public Output Output = new Output();

    public bool Halted { get; set; }

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

    public Processor(string inputData)
    {
      InstructionPointer = 0;
      ProgramData = LoadData(inputData);
    }

    public Processor(List<int> programData)
    {
      InstructionPointer = 0;
      ProgramData = programData;
    }

    private List<int> LoadData(string inputData)
    {
      using (StringReader sr = new StringReader(inputData))
      {
        string line = sr.ReadLine();
        string[] items = line.Split(',');
        return new List<int>(items.Select(x => int.Parse(x)));
      }
    }

    public void Run()
    {
      Halted = false;
      int opCode = GetOpCode(ProgramData[InstructionPointer]);
      while(opCode != 99)
      {
        if(!Instructions.ContainsKey(opCode))
        {
          throw new Exception($"Instruction {opCode} not implemented!");
        }
        var instruction = Instructions[opCode];
        var instructionParams = instruction.Parse(ProgramData, InstructionPointer);
        eProcState state = instruction.Execute(this, Input, Output, instructionParams);
        if (state == eProcState.Halt)
        {
          Halted = true;
          return;
        }
        
        opCode = GetOpCode(ProgramData[InstructionPointer]);
      }

    }

    private int GetOpCode(int value)
    {
      return value % 100;
    }

    
  }
}
