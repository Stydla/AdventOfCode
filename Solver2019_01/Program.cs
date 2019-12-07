using Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Solver2019_01
{

  public class Program : BaseAdventSolver, IAdventSolver
  {
    public override string SolverName => "Day 1: The Tyranny of the Rocket Equation";

    public Program() : base(2019, 1)
    {
    }

    public override string SolveTask1(string inputData)
    {
      int total = 0;

      using (StringReader sr = new StringReader(inputData))
      {
        string line;
        while ((line = sr.ReadLine()) != null)
        {
          
          int mass = int.Parse(line);

          total += CalculateFuel(mass);

        }
      }
      return total.ToString();
    }

    public override string SolveTask2(string inputData)
    {
      int total = 0;

      using (StringReader sr = new StringReader(inputData))
      {
        string line;
        while ((line = sr.ReadLine()) != null)
        {
          int mass = int.Parse(line);

          total += CalculateTotalFuel(mass);

        }
      }
      return total.ToString();
    }

    private int CalculateTotalFuel(int mass)
    {
      int total = 0;
      int tmpFuel = CalculateFuel(mass);
      while (tmpFuel > 0)
      {
        total += tmpFuel;
        tmpFuel = CalculateFuel(tmpFuel);
      }
      return total;
    }

    private int CalculateFuel(int mass)
    {
      return (mass / 3) - 2;
    }
  }
}
