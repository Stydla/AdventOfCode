using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solver2019_02
{
  public class Solver
  {

    public int Pos;
    public List<int> Values { get; }

    public Solver(List<int> values)
    {
      Values = values;
      Pos = 0;
    }

    public Result NextStep()
    {
      int opCode = Values[Pos];
      switch (opCode)
      {
        case 1:
          Values[Values[Pos + 3]] = Values[Values[Pos + 1]] + Values[Values[Pos + 2]];
          Pos += 4;
          return Result.NotFinished;
        case 2:
          Values[Values[Pos + 3]] = Values[Values[Pos + 1]] * Values[Values[Pos + 2]];
          Pos += 4;
          return Result.NotFinished;
        case 99:
          Pos += 1;
          return Result.Finished;
        default:
          return Result.Error;
      }
    }
  }

  public enum Result
  {
    Finished,
    NotFinished,
    Error
  }
}
