using System;

namespace Solver2019_17
{
  public class Field
  {

    public eFieldType Type { get; set; }
    public Point Location { get; set; }

    public Field(Point location, eFieldType type)
    {
      Location = location;
      Type = type;
    }

    public char GetCharValue()
    {
      switch (Type)
      {
        case eFieldType.Empty:
          return '.';
        case eFieldType.Scaffold:
          return '#';
        default:
          throw new Exception("Invalid Field Type");
      }
    }
  }

  public enum eFieldType
  {
    Empty,
    Scaffold
  }
}