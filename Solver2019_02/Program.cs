using Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Solver2019_02
{
  public class Program : BaseAdventSolver, IAdventSolver
  {
    public Program() : base(2019, 2)
    {
    }

    public override string SolverName => "Day 2: 1202 Program Alarm";

    public override string SolveTask1(string inputData)
    {
      Solver solver = LoadData(inputData);
      //solver.Values[1] = 12;
      //solver.Values[2] = 2;
      while (solver.NextStep() != Result.Finished)
      {
      }
      return solver.Values[0].ToString();
    }

    public override string SolveTask2(string inputData)
    {
      for (int i = 0; i < 100; i++)
      {
        for(int j = 0; j < 100; j++)
        {
          Solver solver = LoadData(inputData);
          solver.Values[1] = i;
          solver.Values[2] = j;
          while (solver.NextStep() != Result.Finished)
          {
          }
          if(solver.Values[0] == 19690720)
          {
            return (i * 100 + j).ToString();
          }
        }
      }
      
      return "Not Found";
    }

    private Solver LoadData(string inputData)
    {
      using (StringReader sr = new StringReader(inputData))
      {
        string line = sr.ReadLine();
        var list = new List<string>(line.Split(',')).Select(x=>int.Parse(x)).ToList();
        return new Solver(list);
      }
    }
  }
}
