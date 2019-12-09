using IntCodeCpu;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solver2019_07
{
  public class Program : BaseAdventSolver
  {
    public Program() : base(2019, 7)
    {
    }

    public override string SolverName => "Day 7: Amplification Circuit";

    public override string SolveTask1(string inputData)
    {
      var configurations = GetConfigurations1();

      long maxThrust = 0;

      foreach(var config in configurations)
      {
        
        Output output = new Output();
        output.Add(0);
        foreach (int setting in config)
        {
          string data = inputData;
          Processor cpu = new Processor(data);
          cpu.Input.Add(setting);
          cpu.Input.Add(output.GetNext());
          cpu.Run();

          output.Add(cpu.Output.GetNext());
        }

        long tmpVal = output.LastValue();
        if (tmpVal > maxThrust)
        {
          maxThrust = tmpVal;
        }
        
      }

      return maxThrust.ToString();

    }

    private List<IEnumerable<int>> GetConfigurations1()
    {
      int[] arr = { 0, 1, 2, 3, 4 };
      return GetPermutations(arr, 5).ToList();
    }

    private List<IEnumerable<int>> GetConfigurations2()
    {
      int[] arr = { 5,6,7,8,9};
      return GetPermutations(arr, 5).ToList();
    }

    public override string SolveTask2(string inputData)
    {
      var configurations = GetConfigurations2();

      long maxThrust = 0;

      foreach (var configTmp in configurations)
      {
        List<int> config = configTmp.ToList();
        int lastConfig = config.Last();

        List<Processor> processors = new List<Processor>();
        for (int i = 0; i < 5; i++)
        {
          processors.Add(new Processor(inputData));
          processors[i].Input.Add(config[i]);
          
        }
        processors[0].Input.Add(0);

        int currentCpu = 0;
        while (true)
        {

          processors[currentCpu].Run();
          processors[(currentCpu + 1) % 5].Input.Add(processors[currentCpu].Output.GetNext());
          if(currentCpu == 4 && processors[currentCpu].Halted == false)
          {
            break;
          } else
          {
            currentCpu = (currentCpu + 1) % 5;
          }
        }

        long tmp = processors[4].Output.LastValue();
        if (tmp > maxThrust)
        {
          maxThrust = tmp;
        }
        
      }

      return maxThrust.ToString();
    }

    static IEnumerable<IEnumerable<T>> GetPermutations<T>(IEnumerable<T> list, int length)
    {
      if (length == 1) return list.Select(t => new T[] { t });

      return GetPermutations(list, length - 1)
          .SelectMany(t => list.Where(e => !t.Contains(e)),
              (t1, t2) => t1.Concat(new T[] { t2 }));
    }


  }


}
