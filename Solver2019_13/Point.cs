﻿namespace Solver2019_13
{
  public class Point
  {
    public int X;
    public int Y;

    public Point(int x, int y)
    {
      X = x;
      Y = y;
    }

    public override string ToString()
    {
      return $"{X},{Y}";
    }
  }
}