using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solver2019_16
{
  public class FFT
  {

    public int Phase { get; set; } = 0;
    public List<int> Values { get; set; } = new List<int>();
    public int Count { get; set; }

    public FFT(string input)
    {
      using(StringReader sr = new StringReader(input))
      {
        string line = sr.ReadLine();
        for(int i = 0; i < line.Length; i++)
        {
          Values.Add(line[i] - '0');
        }
      }
      Count = Values.Count;
    }

    private List<int> PatternBase = new List<int>() { 0, 1, 0, -1 };
    private List<int> PatternBase2 = new List<int>() { 1, 0, -1, 0 };

    public void Solve(int phases)
    {
      for(int i = 0; i < phases; i++)
      {
        SolvePhaseParallel();
      }
    }

    public void Solve2(int offset)
    {
      if (offset < Count / 2) throw new Exception("This will not work");

      for(int i = 0; i < 100; i++)
      {
        SolvePhaseLowerPart(offset);
      }

    }

    private void SolvePhaseLowerPart(int offset)
    {
      List<int> next = new List<int>(Values);

      int sum = 0;
      for (int i = offset; i < Count; i++)
      {
        if(i == offset)
        {
          for (int j = i; j < Count; j++)
          {
            sum += Values[j];
          }
        } else
        {
          sum -= Values[i - 1];
        }
        next[i] = Math.Abs(sum % 10);
      }

      Values = next;

    }


    private void SolvePhaseParallel()
    {
      List<int> next = new List<int>(Values);

      Parallel.For(0, Count, (i) =>
      {
        int repeatCount = i + 1;

        int sum = 0;
        for (int j = i; j < Count;)
        {

          int div = ((j + 1) / repeatCount) % 4;
          int multiplier = PatternBase[div];

          repeatCount = Math.Min(repeatCount, Count - j);

          if(multiplier == 1)
          {
            for (int k = 0; k < repeatCount; k++)
            {
              sum += Values[j + k];
            }
          }
          if (multiplier == -1)
          {
            for (int k = 0; k < repeatCount; k++)
            {
              sum -= Values[j + k];
            }
          }
          j += repeatCount;
        }

        next[i] = Math.Abs(sum % 10);
      });
      Values = next;
    }
  }
}
