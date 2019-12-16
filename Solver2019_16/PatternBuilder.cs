using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solver2019_16
{
  class PatternBuilder
  {

    private static List<int> PatternBase = new List<int>() { 0, 1, 0, -1 };
    public static void BuildPattern(int size)
    {
      for (int i = 0; i < size; i++)
      {
        System.Diagnostics.Debug.Write($"{i,3}");
      }
      System.Diagnostics.Debug.WriteLine($"");
      for (int i = 0; i < size; i++)
      {
        int repeatCount = i + 1;
        for (int j = 0; j < size; j++)
        {
          int div = ((j + 1) / repeatCount) % 4;
          int multiplier = PatternBase[div];
          System.Diagnostics.Debug.Write($"{multiplier,3}");
        }
        System.Diagnostics.Debug.WriteLine("");
      }
    }

    public static List<int> BuildPattern(int row, int size)
    {
      List<int> ret = new List<int>(size);
      int repeatCount = row + 1;
      for (int j = 0; j < size; j++)
      {
        int div = ((j + 1) / repeatCount) % 4;
        int multiplier = PatternBase[div];
        ret.Add(multiplier);
      }
      return ret;
    }

  }
}
