using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solver2019_08
{
  public class Program : BaseAdventSolver
  {
    public Program() : base(2019, 8)
    {
    }

    public override string SolverName => "Day 8: Space Image Format";

    public override string SolveTask1(string InputData)
    {

      Picture p = new Picture(InputData, 25, 6);

      return p.Task1().ToString();
    }

    public override string SolveTask2(string InputData)
    {
      Picture p = new Picture(InputData, 25, 6);

      return p.Task2();
    }
  }
}
