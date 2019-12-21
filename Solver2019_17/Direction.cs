using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solver2019_17
{
  public class Direction
  {

    public static char GetCharDirection(eDirection dir)
    {
      switch (dir)
      {
        case eDirection.North:
          return '^';
        case eDirection.South:
          return 'v';
        case eDirection.West:
          return '<';
        case eDirection.East:
          return '>';
        default:
          throw new Exception($"Ivalid enum direction {dir}");
      }
    }

    public static eDirection GetEnumDirection(char dir)
    {
      switch (dir)
      {
        case '^':
          return eDirection.North;
        case 'v':
          return eDirection.South;
        case '<':
          return eDirection.West;
        case '>':
          return eDirection.East;
        default:
          throw new Exception($"Invalid char direction {dir}");

      }
    }

    public static eDirection GetNextDirection(eDirection dir, eTurn turn)
    {
      switch (dir)
      {
        case eDirection.North:

          switch (turn)
          {
            case eTurn.Left:
              return eDirection.West;
            case eTurn.Right:
              return eDirection.East;
            case eTurn.Forward:
              return eDirection.North;
          }
          break;

        case eDirection.South:
          switch (turn)
          {
            case eTurn.Left:
              return eDirection.East;
            case eTurn.Right:
              return eDirection.West;
            case eTurn.Forward:
              return eDirection.South;
          }
          break;
        case eDirection.West:
          switch (turn)
          {
            case eTurn.Left:
              return eDirection.South;
            case eTurn.Right:
              return eDirection.North;
            case eTurn.Forward:
              return eDirection.West;
          }
          break;
        case eDirection.East:
          switch (turn)
          {
            case eTurn.Left:
              return eDirection.North;
            case eTurn.Right:
              return eDirection.South;
            case eTurn.Forward:
              return eDirection.East;
          }
          break;
      }
      throw new Exception("Invalid something");
    }

    public static eTurn GetTurn(eDirection fromDir, eDirection toDir)
    {
      switch (fromDir)
      {
        case eDirection.North:
          {
            switch (toDir)
            {
              case eDirection.East:
                return eTurn.Left;
              case eDirection.West:
                return eTurn.Right;
              case eDirection.South:
                return eTurn.Forward;
              default:
                throw new Exception("Invalid turn");
            }
          }
        case eDirection.South:
          {
            switch (toDir)
            {
              case eDirection.East:
                return eTurn.Right;
              case eDirection.West:
                return eTurn.Left;
              case eDirection.North:
                return eTurn.Forward;
              default:
                throw new Exception("Invalid turn");
            }
          }
        case eDirection.West:
          {
            switch (toDir)
            {
              case eDirection.North:
                return eTurn.Left;
              case eDirection.East:
                return eTurn.Forward;
              case eDirection.South:
                return eTurn.Right;
              default:
                throw new Exception("Invalid turn");
            }
          }
        case eDirection.East:
          {
            switch (toDir)
            {
              case eDirection.North:
                return eTurn.Right;
              case eDirection.South:
                return eTurn.Left;
              case eDirection.West:
                return eTurn.Forward;
              default:
                throw new Exception("Invalid turn");
            }
          }
      }
      throw new Exception("Invalid turn");
    }

    public static eDirection GetOposite(eDirection dir)
    {
      switch (dir)
      {
        case eDirection.North:
          return eDirection.South;
        case eDirection.South:
          return eDirection.North;
        case eDirection.West:
          return eDirection.East;
        case eDirection.East:
          return eDirection.West;
        default:
          throw new Exception($"Oposite not exists {dir}");
      }
    }

    public enum eDirection
    {
      North,
      South,
      West,
      East
    }

    public enum eTurn
    {
      Left,
      Right,
      Forward
    }

  }
}
