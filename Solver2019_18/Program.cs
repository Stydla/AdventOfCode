using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solver2019_18
{
  public class Program : BaseAdventSolver
  {
    public Program() : base(2019, 18)
    {
    }

    public override string SolverName => "Day 18: Many-Worlds Interpretation";

    public override string SolveTask1(string InputData)
    {

      Map m = new Map(InputData);
      System.Diagnostics.Debug.WriteLine(m.Print());
     

      return "";
    }

    public override string SolveTask2(string InputData)
    {
      throw new NotImplementedException();
    }
  }
}
