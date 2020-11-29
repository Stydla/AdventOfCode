using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solver2019_18
{
  public class Field
  {
    public Point Point { get; }
    public FieldType Type { get; }
    public Item Item { get; set; }

    public Field(char c, Point point)
    {
      Point = point;
      switch (c)
      {
        case '#':
          Type = FieldType.Wall;
          break;
        default:
          Type = FieldType.Empty;
          break;
      }
      return;
    }
      

    public string Print()
    {
      return ToString();
    }

    public override string ToString()
    {
      switch (Type)
      {
        case FieldType.Wall:
          return "#";
        case FieldType.Empty:
          if (Item == null)
          {
            return " ";
          }
          else
          {
            return Item.Print();
          }
      }
      return "";
    }

    public enum FieldType
  {
    Wall,
    Empty,
  }

  }
}