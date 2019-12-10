using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solver2019_10
{
  public class Program : BaseAdventSolver
  {
    public Program() : base(2019, 10)
    {
    }

    public override string SolverName => "Day 10: Monitoring Station";

    public override string SolveTask1(string InputData)
    {
      Map m = new Map(InputData);
      m.CalculateVisibleAsteroids();

      MapItem mi = m.TheBestAsteroid();
      return mi.VisibleAsteroids.Count.ToString();
      
    }

    public override string SolveTask2(string InputData)
    {
      Map m = new Map(InputData);
      m.CalculateVisibleAsteroids();

      MapItem mi = m.TheBestAsteroid();
      MapItem nextForKill = mi.KillAndMurder(m.Asteroids, 200);
      int result = nextForKill.Point.X * 100 + nextForKill.Point.Y;
      return result.ToString();
    }
  }
}
