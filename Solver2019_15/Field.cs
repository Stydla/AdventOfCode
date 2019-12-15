using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Solver2019_15.Direction;

namespace Solver2019_15
{
  public class Field
  {
    public Point Location { get; set; }
    public eFieldType Type {get; set;}
    public Dictionary<eDirection, Field> Neighbours { get; set; } = new Dictionary<eDirection, Field>();

    public Dictionary<eDirection, Field> EmptyNeighbours { get; set; } = new Dictionary<eDirection, Field>();

    public Field(Point location, eFieldType type)
    {
      Location = location;
      Type = type;
    }

    public bool IsSearched()
    {
      if (Type == eFieldType.Wall) return true;
      if (Neighbours.Count == 4) return true;
      return false;
    }

    public List<eDirection> GetMissingNeighboursDirections()
    {
      List<eDirection> ret = new List<eDirection>();
      var values = Enum.GetValues(typeof(eDirection));
      foreach(eDirection val in values)
      {
        if (!Neighbours.ContainsKey(val))
        {
          ret.Add(val);
        }
      }
      return ret;
    }

    public void AddNeighbours(Map m)
    {
      var dirs = GetMissingNeighboursDirections();
      foreach (var dir in dirs)
      {
        Field tmp = m.GetField(Location.Move(dir));
        if (tmp != null)
        {
          Neighbours.Add(dir, tmp);
          if (tmp.Type != eFieldType.Wall)
          {
            EmptyNeighbours.Add(dir, tmp);
          }
        }
      }
    }

    public override string ToString()
    {
      return $"{Location},{Type}";
    }

    public char Print()
    {
      switch (Type)
      {
        case eFieldType.Wall:
          return '#';
        case eFieldType.Empty:
          return ' ';
        case eFieldType.Oxygen:
          return 'O';
        default:
          return '!';
      }
    }

  }

  public enum eFieldType
  {
    Wall,
    Empty,
    Oxygen
  }



}
