using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solver2019_12
{
  public class Map
  {

    public List<Moon> Moons { get; set; } = new List<Moon>();
    public long RepeatStepsCount {
      get 
      {
        long l1 = DetermineLCM(XRepeat, YRepeat);
        return DetermineLCM(l1, ZRepeat);
      }
    }

    private long XRepeat = -1;
    private long YRepeat = -1;
    private long ZRepeat = -1;


    public Map(string input)
    {
      var points = LoadPoints(input);
      foreach (Point p in points)
      {
        Moon m = new Moon(p);
        Moons.Add(m);
      }
    }

    private List<Point> LoadPoints(string input)
    {
      using(StringReader sr = new StringReader(input))
      {
        List<Point> list = new List<Point>();
        string line;
        while((line = sr.ReadLine()) != null)
        {
          Point p = Point.Parse(line);
          list.Add(p);
        }
        return list;
      }
    }

    internal void SimulateUntilRepeat()
    {
      List<Moon> initialState = new List<Moon>();
      foreach(Moon m in Moons)
      {
        initialState.Add(new Moon(m.Location));
      }
      int count = 1;
      while(XRepeat == -1 || YRepeat == -1 || ZRepeat == -1)
      {
        SimulateStep();
        count++;

        if(XRepeat == -1 && initialState.All(x=>Moons.Any(y=>y.Location.X == x.Location.X)))
        {
          XRepeat = count;
        }
        if (YRepeat == -1 && initialState.All(x => Moons.Any(y => y.Location.Y == x.Location.Y)))
        {
          YRepeat = count;
        }
        if (ZRepeat == -1 && initialState.All(x => Moons.Any(y => y.Location.Z == x.Location.Z)))
        {
          ZRepeat = count;
        }


      }
    }

    public void Simulate(int count)
    {
      for(int i = 0; i < count; i++)
      {
        SimulateStep();
      }
    }

    private void SimulateStep()
    {
      foreach (Moon m in Moons)
      {
        m.CalculateGravity(Moons);
      }

      foreach (Moon m in Moons)
      {
        m.AplyGravity();
      }

      foreach (Moon m in Moons)
      {
        m.Move();
      }
    }

    public int Energy()
    {
      int energy = 0;
      foreach (Moon m in Moons)
      {
        energy += m.Energy;
        
      }
      return energy;
    }

    private long DetermineLCM(long a, long b)
    {
      long num1, num2;
      if (a > b)
      {
        num1 = a; num2 = b;
      }
      else
      {
        num1 = b; num2 = a;
      }

      for (int i = 1; i < num2; i++)
      {
        if ((num1 * i) % num2 == 0)
        {
          return i * num1;
        }
      }
      return num1 * num2;
    }

  }
}
