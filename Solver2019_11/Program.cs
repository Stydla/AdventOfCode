using IntCodeCpu;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solver2019_11
{
  public class Program : BaseAdventSolver
  {
    public Program() : base(2019, 11)
    {
    }

    public override string SolverName => "Day 11: Space Police";

    public override string SolveTask1(string InputData)
    {

      Robot r = new Robot(InputData);
      r.Run(0);

      int res = r.GetBlackFields();

      return res.ToString();
    }

    public override string SolveTask2(string InputData)
    {
      Robot r = new Robot(InputData);
      r.Run(1);

      string res = r.GetResultPlane();

      return res;
    }
  }
}
