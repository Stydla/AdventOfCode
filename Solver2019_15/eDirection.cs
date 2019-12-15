using System;

namespace Solver2019_15
{

  public static class Direction
  {
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
      North = 1,
      South = 2,
      West = 3,
      East = 4,
    }
  }
}