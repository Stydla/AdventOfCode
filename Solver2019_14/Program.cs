using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solver2019_14
{
  public class Program : BaseAdventSolver
  {
    public Program() : base(2019, 14)
    {
    }

    public override string SolverName => "Day 14: Space Stoichiometry";

    public override string SolveTask1(string InputData)
    {
      Nanofactory nf = new Nanofactory(InputData);
      long oreCount = 1000000000000;
      nf.MaterialStock.GetStockItem("ORE").Count = oreCount;
      nf.Create("FUEL", 1);
      return (oreCount - nf.MaterialStock.GetStockItem("ORE").Count).ToString();
    }

    public override string SolveTask2(string InputData)
    {

      Nanofactory nf = new Nanofactory(InputData);
      long oreCount = 1000000000000;
      nf.MaterialStock.GetStockItem("ORE").Count = oreCount;

      for(int i = 1; i < 30; i++)
      {
        nf.OptimalizeReaction("FUEL", (int)Math.Pow(2 ,i));
      }

      nf.Create("FUEL", 1);
      long maxOreForOneFuel = (oreCount - nf.MaterialStock.GetStockItem("ORE").Count);
      long bunch = nf.MaterialStock.GetStockItem("ORE").Count / maxOreForOneFuel;

      try
      {
        while (true && bunch > 0)
        {
          long nextBunch = nf.MaterialStock.GetStockItem("ORE").Count / maxOreForOneFuel;
          if(nextBunch >= 1)
          {
            nf.Create("FUEL", nextBunch);
          } else
          {
            if(!nf.Reverse(new List<string>() { "FUEL" }))
            {
            nf.Create("FUEL", 1);
            break;
            }
          }
        }
      }
      catch (OutOfMaterialException)
      {
        nf.Reverse(new List<string>() { "FUEL" });
      }
        
      
      
      return (nf.MaterialStock.GetStockItem("FUEL").Count).ToString();
    }
  }
}
