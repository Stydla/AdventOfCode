using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solver2019_16
{
  public class Program : BaseAdventSolver
  {
    public Program() : base(2019, 16)
    {
    }

    public override string SolverName => "Day 16: Flawed Frequency Transmission";

    public override string SolveTask1(string InputData)
    {
      FFT fft = new FFT(InputData);
      fft.Solve(100);

      return string.Join("",fft.Values.Take(8));

    }

    public override string SolveTask2(string InputData)
    {
      StringBuilder sb = new StringBuilder();
      int repeatBlockSize = 10000;
      for(int i = 0; i < repeatBlockSize; i++)
      {
        sb.Append(InputData);
      }
      int offset = int.Parse(InputData.Substring(0, 7));

      FFT fft = new FFT(sb.ToString());
      fft.Solve2(offset);

      

      return string.Join("", fft.Values.Skip(offset).Take(8));
    }
  }
}
