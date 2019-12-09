using IntCodeCpu;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solver2019_09
{
  public class Program : BaseAdventSolver
  {
    public Program() : base(2019, 9)
    {
    }

    public override string SolverName => "Day 9: Sensor Boost ";

    public override string SolveTask1(string InputData)
    {

      Processor proc = new Processor(InputData);
      proc.Input.Add(1);
      proc.Run();
      
      string result = proc.Output.Print();
      
      return result;
    }

    public override string SolveTask2(string InputData)
    {
      Processor proc = new Processor(InputData);
      proc.Input.Add(2);
      proc.Run();

      string result = proc.Output.Print();

      return result;
    }
  }
}
