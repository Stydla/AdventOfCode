using IntCodeCpu;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solver2019_17
{
  public class Program : BaseAdventSolver
  {
    public Program() : base(2019, 17)
    {
    }

    public override string SolverName => "Day 17: Set and Forget";

    public override string SolveTask1(string InputData)
    {
      Robot r = new Robot(InputData);
      
      int res = r.Solve1();

      return res.ToString();
    }

    public override string SolveTask2(string InputData)
    {


      Robot r = new Robot(InputData);
      r.Print();
      long res = r.Solve2(InputData);

      return res.ToString();
    }

  }
}
