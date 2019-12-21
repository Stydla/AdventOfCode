using System;
using System.Collections.Generic;
using System.Linq;
using static Solver2019_17.Direction;

namespace Solver2019_17
{
  public class Road
  {

    public List<Point> Points { get; set; } = new List<Point>();

    public Crossroad Endpoint1 { get; set; }
    public eDirection Endpoint1Direction { get; set; }
    public Crossroad Endpoint2 { get; set; }
    public eDirection Endpoint2Direction { get; set; }

    public Crossroad StartCrossroad { get; set; }

    public int ID { get; } = _ID++;
    public bool IsUsed { get; internal set; }

    private static int _ID = 0;

    public Crossroad GetEndCrossroad()
    {
      return StartCrossroad == Endpoint1 ? Endpoint2 : Endpoint1;
    }

    public eDirection GetEndDirection()
    {
      Crossroad endCR = GetEndCrossroad();
      return endCR == Endpoint1 ? Endpoint1Direction : Endpoint2Direction;
    }

    public bool IsLastRoad { get; set; } = false;
    public bool IsFirstRoad { get; set; } = false;

    public override string ToString()
    {
      return $"{ID}";
    }

    private Dictionary<Tuple<Point, eDirection>, CommandResult> CommandCache = new Dictionary<Tuple<Point, eDirection>, CommandResult>();

    public CommandResult CreateCommand(Point p, eDirection dir)
    {
      Tuple<Point, eDirection> key = new Tuple<Point, eDirection>(p, dir);
      
      if (!CommandCache.ContainsKey(key))
      {
        CommandResult cmdRes = new CommandResult();
        List<int> res = new List<int>();
        Point pTmp = p;
        eDirection dTmp = dir;
        while (CanGoSomewhere(pTmp, dTmp))
        {
          
          foreach (eTurn turn in Enum.GetValues(typeof(eTurn)))
          {
            if (CanGo(pTmp, dTmp, turn))
            {
              switch (turn)
              {
                case eTurn.Left:
                  res.Add('L');
                  res.Add('1');
                  break;
                case eTurn.Right:
                  res.Add('R');
                  res.Add('1');
                  break;
                case eTurn.Forward:
                  res.Add('1');
                  break;
              }
              dTmp = GetNextDirection(dTmp, turn);
              pTmp = pTmp.Move(dTmp);
              break;
            }
          }
        }
        cmdRes.Command = res;
        cmdRes.NextDirection = dTmp;
        cmdRes.NextPoint = pTmp.Move(dTmp);
        CommandCache.Add(key, cmdRes);
      }

      return CommandCache[key];
    }

    private bool CanGoSomewhere( Point p, eDirection dir)
    {
      return CanGo(p, dir, eTurn.Forward) || CanGo(p, dir, eTurn.Left) || CanGo(p, dir, eTurn.Right);
    }

    private bool CanGo(Point p, eDirection dir, eTurn turn)
    {
      eDirection dirNext = GetNextDirection(dir, turn);
      Point newP = p.Move(dirNext);
      return Points.Any(x => x.X == newP.X && x.Y == newP.Y);
    }

  }

}