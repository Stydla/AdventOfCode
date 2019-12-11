﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solver2019_11
{
  public class Point
  {
    public int X { get; set; }
    public int Y { get; set; }

    public Point(int x, int y)
    {
      X = x;
      Y = y;
    }

    public Point(Point p)
    {
      X = p.X;
      Y = p.Y;
    }
  }
}
