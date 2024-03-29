﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoCLib
{
  public class ModuloMath
  {
    public static ulong GCD(ulong a, ulong b)
    {
      while (a != 0 && b != 0)
      {
        if (a > b)
          a %= b;
        else
          b %= a;
      }

      return a | b;
    }
  }
}
