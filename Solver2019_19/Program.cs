using IntCodeCpu;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solver2019_19
{
  public class Program : BaseAdventSolver
  {
    public Program() : base(2019, 19)
    {
    }

    public override string SolverName => "Day 19: Tractor Beam";

    public override string SolveTask1(string InputData)
    {
      

      int size = 50;
      long count = 0;

      for(int i = 0; i < size; i++)
      {
        for(int j= 0; j < size; j++)
        {
          Processor proc = new Processor(InputData);
          proc.Input.Add(j);        
          proc.Input.Add(i);
          proc.Run();
          long val = proc.Output.GetNext();
          count += val;
          //if(val == 1)
          //{
          //  Debug.Write("#");
          //} else
          //{
          //  Debug.Write(".");
          //}
        }
        //Debug.WriteLine("");
      }

      return count.ToString();
    }

    public override string SolveTask2(string InputData)
    {
      int distance = 20;
      
      Tractor t = CalculateTractor(distance, InputData);
      int distanceTmp = distance * 100 / t.Size;

      while (distance != distanceTmp)
      {
        t = CalculateTractor(distance, InputData);
        distanceTmp = distance;
        distance = distance * 100 / t.Size;
      }

      int bestDistance = distance;
      while (t.Size >= 98)
      {
        distance--;
        t = CalculateTractor(distance, InputData);
        if(t.Size == 100)
        {
          bestDistance = distance;
        }
      }

      t = CalculateTractor(bestDistance, InputData);

      int y = Math.Min(t.V1.Y, t.V2.Y);
      int x = Math.Min(t.V1.X, t.V2.X);


      return (x * 10000 + y).ToString();
    }

    private Tractor CalculateTractor(int distance, string input)
    {
      Tractor tractor = new Tractor();

      for( int i = 0; i < 100; i++)
      {
        int r = distance;
      }

      int x = 0;
      int y = distance;
      while(true)
      {
        if(Check(x, y, input)) {
          break;
        }
        x++;
      }
      tractor.V1 = new Vector(x, y);

      int size = 1;
      while (true)
      {
        if (!Check(x+1, y-1, input))
        {
          break;
        }
        size++;
        x++;
        y--;
      }
      tractor.V2 = new Vector(x, y);
      tractor.Size = size;

      return tractor;
    }


    private bool Check(int x, int y, string inputData)
    {
      Processor proc = new Processor(inputData);
      proc.Input.Add(x);
      proc.Input.Add(y);
      proc.Run();
      return proc.Output.GetNext() == 1;
    }
   

  }
}
