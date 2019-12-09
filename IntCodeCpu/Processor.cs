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
    public int RelativeBase;
    public List<long> ProgramData;
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
      { 9, new InstrRelativeBase() },
    };

    public Processor(string inputData)
    {
      InstructionPointer = 0;      
      ProgramData = LoadData(inputData);
      RelativeBase = 0;
      for (int i = 0; i < 10000; i++)
      {
        ProgramData.Add(0);
      }
      
    }

    public Processor(List<long> programData)
    {
      InstructionPointer = 0;
      ProgramData = programData;
    }

    private List<long> LoadData(string inputData)
    {
      using (StringReader sr = new StringReader(inputData))
      {
        string line = sr.ReadLine();
        string[] items = line.Split(',');
        return new List<long>(items.Select(x => long.Parse(x)));
      }
    }

    public void Run()
    {
      Halted = false;
      int opCode = GetOpCode((int)ProgramData[InstructionPointer]);
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
        
        opCode = GetOpCode((int)ProgramData[InstructionPointer]);
      }

    }

    private int GetOpCode(int value)
    {
      return value % 100;
    }

    
  }
}
