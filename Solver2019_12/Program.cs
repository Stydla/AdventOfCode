using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solver2019_12
{
  public class Program : BaseAdventSolver
  {
    public Program() : base(2019, 12)
    {
    }

    public override string SolverName => "Day 12: The N-Body Problem";

    public override string SolveTask1(string InputData)
    {

      Map m = new Map(InputData);
      m.Simulate(1000);
      int energy = m.Energy();

      return energy.ToString();

    }

    public override string SolveTask2(string InputData)
    {

      Map m = new Map(InputData);
      m.SimulateUntilRepeat();

      return m.RepeatStepsCount.ToString();

    }

  }
}
