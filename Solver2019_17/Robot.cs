using IntCodeCpu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Solver2019_17.Direction;

namespace Solver2019_17
{
  public class Robot
  {

    public Map Map { get; set; }

    public Point Location { get; set; }
    public eDirection Direction { get; set; }


    public Robot(string input)
    {
      Processor processor = new Processor(input);
      processor.Run();

      List<int> tmp = new List<int>();
      while(processor.Output.HasNext())
      {
        tmp.Add((int)processor.Output.GetNext());
      }

      Map = new Map(tmp);

      char? res = (char?)tmp.Where(x => x == 'v' || x == '^' || x == '<' || x == '>').FirstOrDefault();
      if(res == null)
      {
        throw new Exception("Map does not contain robot!");
      } else
      {
        Direction = GetEnumDirection((char)res);
        Location = Map.InitialRobotLocation;
      }
    }

    public void Print()
    {
      string stringMap = Map.Print(this);
      System.Diagnostics.Debug.WriteLine(stringMap);
    }

    public int Solve1()
    {
      var fields = Map.GetIntersectPoints();

      int res = 0;
      foreach(var f in fields)
      {
        res += (f.Location.X * f.Location.Y);
      }
      return res;
    }


    public long Solve2(string input)
    {

      RoadGraph rg = new RoadGraph();

      foreach (var intersect in Map.GetIntersectPoints())
      {
        rg.Crossroads.Add(new Crossroad(intersect.Location));
      }

      foreach(var cr in rg.Crossroads)
      {
        foreach(eDirection dir in Enum.GetValues(typeof(eDirection))) 
        {
          if(!cr.Roads.ContainsKey(dir))
          {
            Road road = BuildRoad(cr.Location.Move(dir), dir, rg);
            cr.Roads[dir] = road;
            road.Endpoint1 = cr;
            road.Endpoint1Direction = GetOposite(dir);
            rg.Roads.Add(road);
          }
        }
      }

      Road startRoad = rg.FindRoad(Location);
      startRoad.IsFirstRoad = true;
      var roads = rg.Roads.Where(x => (x.Endpoint1 == null || x.Endpoint2 == null) && x != startRoad);
      if(roads.Count() != 1)
      {
        throw new Exception("multiple last roads!");
      } else
      {
        roads.First().IsLastRoad = true;
      }

      List<Road> rl = new List<Road>();
      List<List<Road>> results = new List<List<Road>>();
      FindPaths(rg, startRoad, rl, results);

      ProcCommand pc = CreateCommand(results, rg);

      if(pc == null)
      {
        throw new Exception("Function not found");
      }

      Processor proc = new Processor(input);
      proc.ChangeProgramData(0, 2);

      var main = CreateProcInput(pc.Main);
      var fA = CreateProcInput(pc.FunctionA);
      var fB = CreateProcInput(pc.FunctionB);
      var fC = CreateProcInput(pc.FunctionC);

      proc.Input.AddRange(main);
      proc.Input.AddRange(fA);
      proc.Input.AddRange(fB);
      proc.Input.AddRange(fC);
      proc.Input.Add('n');
      proc.Input.Add('\n');

      proc.Run();

      return proc.Output.LastValue();

    }

    private List<long> CreateProcInput(string val)
    {
      List<int> resL = new List<int>();
      for (int i = 0; i < val.Length; i++)
      {

        if (val[i] >= 'A' && val[i] <= 'Z')
        {
          resL.Add(val[i]);
        }
        else
        {
          if(i+1 < val.Length)
          {
            if(!(val[i+1] >= 'A' && val[i+1] <= 'Z'))
            {
              int c1 = val[i] - '0';
              int c2 = val[i + 1] - '0';
              int c3 = c1 + c2;
              resL.Add((char)(c3 / 10) + '0');
              resL.Add((char)(c3 % 10) + '0');
              i++;
            } else
            {
              resL.Add(val[i]);
            }
          } else
          {
            resL.Add(val[i]);
          }
        }
      }
      StringBuilder sb = new StringBuilder();
      for(int i = 0; i < resL.Count; i++)
      {
        if(i == 0)
        {
          sb.Append((char)resL[i]);
        } else
        {
          if(resL[i-1] >= '0' && resL[i-1] <= '9' && resL[i] >= '0' && resL[i] <= '9')
          {
            sb.Append((char)resL[i]);
          } else
          {
            sb.Append(',');
            sb.Append((char)resL[i]);
          }
        }
      }
      sb.Append("\n");
      string str = sb.ToString();

      return str.Select(x=>(long)x).ToList();
     
      //string cmd = string.Join(",", resL.Select(x=>(char)x));
      //cmd += '\n';
      ////var chars = val.ToCharArray();
      ////string cmd = string.Join(",", chars);
      ////cmd = cmd + '\n';
      //var res = cmd.Select(x => (long)x).ToList();
      //return res;

    }

    private ProcCommand CreateCommand(List<List<Road>> results, RoadGraph rg)
    {
      List<List<int>> commands = new List<List<int>>();
      foreach(var r in results)
      {
        List<int> cmd = new List<int>();
        Point pTmp = Location;
        eDirection dTmp = Direction;
        for (int i = 0; i < r.Count; i++)
        {
          CommandResult cmdRes = r[i].CreateCommand(pTmp, dTmp);
          cmd.AddRange(cmdRes.Command);
          dTmp = cmdRes.NextDirection;
          pTmp = cmdRes.NextPoint;

          if(i+1 < r.Count)
          {
            Crossroad cr = rg.GetCrossroad(pTmp);
            cmdRes = cr.CreateCommand(r[i], r[i + 1]);
            cmd.AddRange(cmdRes.Command);
            dTmp = cmdRes.NextDirection;
            pTmp = cmdRes.NextPoint;
          }
          

        }
        commands.Add(cmd);
      }

      foreach(var cmd in commands)
      {
        for(int i = 0; i < cmd.Count - 1;)
        {
          if(cmd[i] >= '0' && cmd[i] < '9' &&  cmd[i+1] == '1')
          {
            cmd[i]++;
            cmd.RemoveAt(i + 1);
          } else
          {
            i++;
          }

        }
        //System.Diagnostics.Debug.WriteLine(string.Join(",", cmd.Select(x=>(char)x)));
      }


      List<ProcCommand> cmds = new List<ProcCommand>();
      foreach(var cmd in commands)
      {
        var pc = ProcCommand.Create(cmd);
        cmds.AddRange(pc);
        
      }

      return cmds[0];
    }

    private void FindPaths(RoadGraph rg, Road road, List<Road> roadList, List<List<Road>> results)
    {
      roadList.Add(road);
      road.IsUsed = true;
      if (road.IsLastRoad)
      {
        
        if(rg.Roads.All(x=>x.IsUsed)) 
        {
          results.Add(new List<Road>(roadList));
        }
        road.IsUsed = false;
        roadList.Remove(road);
        return;
      }

      
      Crossroad cr = road.GetEndCrossroad();
      

      if (cr.IsUsed)
      {
        eDirection nextDir = GetNextDirection(road.GetEndDirection(), cr.UseTurn);
        Road nextRoad = cr.Roads[nextDir];
        if(!nextRoad.IsUsed)
        {
          nextRoad.StartCrossroad = cr;
          FindPaths(rg, nextRoad, roadList, results);
          nextRoad.StartCrossroad = null;
        }
      }
      else
      {
        foreach (eTurn turn in Enum.GetValues(typeof(eTurn)))
        {
          eDirection nextDir = GetNextDirection(road.GetEndDirection(), turn);
          cr.Use(turn);
          Road nextRoad = cr.Roads[nextDir];
          nextRoad.StartCrossroad = cr;
          FindPaths(rg, nextRoad, roadList, results);
          nextRoad.StartCrossroad = null;
          cr.Unuse();
        }
      }
      road.IsUsed = false;
      roadList.Remove(road);
    }

    private Road BuildRoad(Point location, eDirection dir, RoadGraph rg)
    {
      Road r = new Road();
      r.Points.Add(location);

      Point pTmp = location;
      eDirection dTmp = dir;
      while(CanGoSomewhere(pTmp, dTmp))
      {
        foreach(eTurn turn in Enum.GetValues(typeof(eTurn))) 
        {
          if (CanGo(pTmp, dTmp, turn))
          {
            dTmp = GetNextDirection(dTmp, turn);
            pTmp = pTmp.Move(dTmp);
            if (rg.Crossroads.Any(x => x.Location.X == pTmp.X && x.Location.Y == pTmp.Y))
            {
              Crossroad cr = rg.Crossroads.Where(x => x.Location.X == pTmp.X && x.Location.Y == pTmp.Y).First();
              r.Endpoint2 = cr;
              r.Endpoint2Direction = dTmp;
              cr.Roads[GetOposite(dTmp)] = r;
              return r;
            } else
            {
              r.Points.Add(pTmp);
            }
          }
        }
      }
      return r;
    }

    

    private bool CanGoSomewhere(Point p, eDirection dir)
    {
      return CanGo(p, dir, eTurn.Forward) || CanGo(p, dir, eTurn.Left) || CanGo(p, dir, eTurn.Right);
    }

    private bool CanGo(Point p, eDirection dir, eTurn turn)
    {
      eDirection dirNext = GetNextDirection(dir, turn);
      Point newP = p.Move(dirNext);
      Field f = Map.GetField(newP);
      return f == null ? false : f.Type == eFieldType.Scaffold;
    }

  }

  static class LinqExtensions
  {
    public static IEnumerable<IEnumerable<T>> Split<T>(this IEnumerable<T> list, int parts)
    {
      int i = 0;
      var splits = from item in list
                   group item by i++ % parts into part
                   select part.AsEnumerable();
      return splits;
    }
  }
}
