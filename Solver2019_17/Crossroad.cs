using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Solver2019_17.Direction;

namespace Solver2019_17
{
  public class Crossroad
  {
    public Dictionary<eDirection, Road> Roads = new Dictionary<eDirection, Road>();
    public Point Location { get; set; }

    public bool IsUsed { get; set; }
    public eTurn UseTurn { get; set; }

    public int ID { get; } = _ID++;

    private static int _ID = 0;

    public Crossroad(Point location)
    {
      Location = location;
    }

    public void Use(eTurn turn)
    {
      UseTurn = turn;
      IsUsed = true;
    }

    public void Unuse()
    {
      IsUsed = false;
    }

    public override string ToString()
    {
      return string.Join("," , Roads.Values);
    }

    public eTurn GetTurn(Road from, Road to)
    {
      eDirection fromDir = Roads.FirstOrDefault(x => x.Value == from).Key;
      eDirection toDir = Roads.FirstOrDefault(x => x.Value == to).Key;

      return Direction.GetTurn(fromDir, toDir);

    }

    public eDirection GetDirection(Road to)
    {
      return Roads.FirstOrDefault(x => x.Value == to).Key;
    }

    internal CommandResult CreateCommand(Road from, Road to)
    {
      CommandResult cmdRes = new CommandResult();
      List<int> res = new List<int>();
      eTurn t = GetTurn(from, to);
      switch (t)
      {
        case eTurn.Left:
          res.Add('1');
          res.Add('L');
          res.Add('1');
          break;
        case eTurn.Right:
          res.Add('1');
          res.Add('R');
          res.Add('1');
          break;
        case eTurn.Forward:
          res.Add('1');
          res.Add('1');
          break;
      }
      cmdRes.Command = res;
      cmdRes.NextDirection = GetDirection(to);
      cmdRes.NextPoint = Location.Move(cmdRes.NextDirection);
      return cmdRes;
    }

  }
}
