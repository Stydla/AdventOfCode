using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solver2019_15
{
  public class Program : BaseAdventSolver
  {
    public Program() : base(2019, 15)
    {
    }

    public override string SolverName => "Day 15: Oxygen System";

    public override string SolveTask1(string InputData)
    {
      Robot r = new Robot(InputData);
      r.RevealMap();

      return r.GetOxygenPathCount().ToString();
    }

    public override string SolveTask2(string InputData)
    {
      Robot r = new Robot(InputData);
      r.RevealMap();

      return r.MapPathFromOxygen().GetDepth().ToString();
    }
  }
}
