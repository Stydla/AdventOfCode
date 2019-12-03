using Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solver2019_03
{
  public class Program : BaseAdventSolver, IAdventSolver
  {
    public Program() : base(2019, 3)
    {
    }

    public override string SolverName => "Day 3: Crossed Wires";

    public override string SolveTask1()
    {
      Wires w = LoadData();
      var res = w.Wire1.CoordPath.Intersect(w.Wire2.CoordPath, new CoordComparer());

      int distance = int.MaxValue;
      Coord startCoord = new Coord(0, 0, 0);
      foreach(var coord in res)
      {
        int distanceTmp = Distance(startCoord, coord);
        if(distance > distanceTmp)
        {
          distance = distanceTmp;
        }
      }

      return distance.ToString();
    }

    private int Distance(Coord c1, Coord c2)
    {
      return Math.Abs(c1.X - c2.X) + Math.Abs(c1.Y - c2.Y);
    }

    public override string SolveTask2()
    {
      Wires w = LoadData();
      var res = w.Wire1.CoordPath.Intersect(w.Wire2.CoordPath, new CoordComparer()).ToList();
      

      int stepValue = int.MaxValue;
      Coord startCoord = new Coord(0, 0, 0);
      foreach (var c1 in res)
      {
        var coords = w.Wire2.CoordPath.Where(c2 => c2.X == c1.X && c2.Y == c1.Y );
        foreach(var c2 in coords)
        {
          int stepValueTmp = StepValue(c1, c2);
          if (stepValue > stepValueTmp)
          {
            stepValue = stepValueTmp;
          }
        }
      }
    
      return stepValue.ToString();
    }

    private int StepValue(Coord startCoord, Coord coord)
    {
      return startCoord.StepNr + coord.StepNr;
    }

    private Wires LoadData()
    {
      using (StreamReader sr = new StreamReader(Filename))
      {
        return new Wires()
        {
          Wire1 = new Wire(sr.ReadLine()),
          Wire2 = new Wire(sr.ReadLine()),
        };
      }
    }
  }

  public class Wires
  {
    public Wire Wire1 { get; set; }
    public Wire Wire2 { get; set; }
  }

  public class Wire
  {
    public Wire(string path)
    {
      SymbolPath.AddRange(path.Split(','));
      CreateCoordPath();
    }

    private void CreateCoordPath()
    {
      Coord current = StartCoord;
      foreach(var symbol in SymbolPath)
      {
        char dir = symbol[0];
        int value = int.Parse(symbol.Substring(1));
        switch(dir)
        {
          case 'U':
            for(int i = 0; i < value; i++)
            {
              current = new Coord(current.X, current.Y + 1, current.StepNr + 1);
              CoordPath.Add(current);
            }
            break;
          case 'D':
            for (int i = 0; i < value; i++)
            {
              current = new Coord(current.X, current.Y - 1, current.StepNr + 1);
              CoordPath.Add(current);
            }
            break;
          case 'L':
            for (int i = 0; i < value; i++)
            {
              current = new Coord(current.X - 1, current.Y, current.StepNr + 1);
              CoordPath.Add(current);
            }
            break;
          case 'R':
            for (int i = 0; i < value; i++)
            {
              current = new Coord(current.X + 1, current.Y, current.StepNr + 1);
              CoordPath.Add(current);
            }
            break;
        }
      }
    }

    public Coord StartCoord { get; set; } = new Coord(0, 0, 0);
    public List<string> SymbolPath { get; set; } = new List<string>();
    public List<Coord> CoordPath { get; set; } = new List<Coord>();
  }


  public class Coord
  {
    public Coord(int x, int y, int stepNr)
    {
      X = x;
      Y = y;
      StepNr = stepNr;
    }
    public int X { get; set; }
    public int Y { get; set; }
    public int StepNr { get; set; }
    
  }

  public class CoordComparer : IEqualityComparer<Coord>
  {
    public bool Equals(Coord item1, Coord item2)
    {
      if (object.ReferenceEquals(item1, item2))
        return true;
      if (item1 == null || item2 == null)
        return false;
      return item1.X.Equals(item2.X) &&
             item1.Y.Equals(item2.Y);
    }

    public int GetHashCode(Coord obj)
    {
      return new { obj.X, obj.Y }.GetHashCode();
    }
  }
}
