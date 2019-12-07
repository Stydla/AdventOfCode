using Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solver2019_06
{
  public class Program : BaseAdventSolver
  {
    public Program() : base(2019, 6)
    {
    }

    public override string SolverName => "Day 6: Universal Orbit Map";

    public override string SolveTask1(string inputData)
    {
      var data = LoadData(inputData);
      Tree t = new Tree(data);
      int totalOrbits = t.TotalOrbits();

      return totalOrbits.ToString();
    }

    public override string SolveTask2(string inputData)
    {
      var data = LoadData(inputData);
      Tree t = new Tree(data);
      int totalOrbits = t.OrbitalTransfers("YOU", "SAN");

      return totalOrbits.ToString();
    }

    private List<InputItem> LoadData(string inputData)
    {
      using(StringReader sr = new StringReader(inputData))
      {
        List<InputItem> list = new List<InputItem>();
        string line;
        while((line = sr.ReadLine()) != null)
        {
          var inputItem = new InputItem(line);
          list.Add(inputItem);
        }
        return list;
      }
    }
  }
}
