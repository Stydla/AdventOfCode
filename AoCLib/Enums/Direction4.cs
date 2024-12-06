using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoCLib.Enums
{
  public enum EDirection4
  {
    UP = 0,
    RIGHT = 1,
    DOWN = 2,
    LEFT = 3
  }

  public static class EDirection4Helper
  {
    public static EDirection4 Parse(char value)
    {
      switch (value)
      {
        case '>':
          return EDirection4.RIGHT;
        case 'v':
          return EDirection4.DOWN;
        case '<':
          return EDirection4.LEFT;
        case '^':
          return EDirection4.UP;
      }
      throw new Exception($"Invalid EDirection4 value: {value}");
    }
    public static bool TryParse(char value, out EDirection4 direction) 
    { 
      if(value == '>')
      {
        direction = EDirection4.RIGHT;
        return true;
      }
      if (value == 'v')
      {
        direction = EDirection4.DOWN;
        return true;
      }
      if (value == '<')
      {
        direction = EDirection4.LEFT;
        return true;
      }
      if (value == '^')
      {
        direction = EDirection4.UP;
        return true;
      }
      direction = EDirection4.UP;
      return false;
    }
  }
}
