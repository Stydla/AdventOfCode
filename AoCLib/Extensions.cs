﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoCLib
{
  public static class Extensions
  {

    public static T Next<T>(this T src) where T : struct
    {
      if (!typeof(T).IsEnum) throw new ArgumentException(String.Format("Argument {0} is not an Enum", typeof(T).FullName));

      T[] Arr = (T[])Enum.GetValues(src.GetType());
      int j = Array.IndexOf<T>(Arr, src) + 1;
      return (Arr.Length == j) ? Arr[0] : Arr[j];
    }

    public static T Prev<T>(this T src) where T : struct
    {
      if (!typeof(T).IsEnum) throw new ArgumentException(String.Format("Argument {0} is not an Enum", typeof(T).FullName));

      T[] Arr = (T[])Enum.GetValues(src.GetType());
      int j = (Array.IndexOf<T>(Arr, src) - 1 + Arr.Length) % Arr.Length;
      return (Arr.Length == j) ? Arr[0] : Arr[j];
    }
  }
}
