using Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solver2019_05
{
  public class Program : BaseAdventSolver
  {
    public Program() : base(2019, 5)
    {
    }

    public override string SolverName => "Day 5: Sunny with a Chance of Asteroids";

    public override string SolveTask1()
    {
      Input input = new Input() { Value = 1 };
      Output output = new Output();

      List<int> programData = LoadData();
      Processor processor = new Processor(programData, input, output);
      
      processor.Run();

      return output.Value.ToString();
    }

    public override string SolveTask2()
    {
      Input input = new Input() { Value = 5 };
      Output output = new Output();

      List<int> programData = LoadData();
      Processor processor = new Processor(programData, input, output);

      processor.Run();

      return output.Value.ToString();
    }

    private List<int> LoadData()
    {
      using(StreamReader sr = new StreamReader(Filename))
      {
        string line = sr.ReadLine();
        string[] items = line.Split(',');
        return new List<int>(items.Select(x => int.Parse(x)));
      }
    }
  }
}
