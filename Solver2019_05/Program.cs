using IntCodeCpu;
using Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solver2019_05
{
  public class Program : BaseAdventSolver
  {
    public Program() : base(2019, 5)
    {
    }

    public override string SolverName => "Day 5: Sunny with a Chance of Asteroids";

    public override string SolveTask1(string inputData)
    {
      
      Processor processor = new Processor(inputData);
      processor.Input.Add(1);
      
      processor.Run();

      return processor.Output.Value.ToString();
    }

    public override string SolveTask2(string inputData)
    {
      Processor processor = new Processor(inputData);
      processor.Input.Add(5);

      processor.Run();

      return processor.Output.Value.ToString();
    }

  }
}
