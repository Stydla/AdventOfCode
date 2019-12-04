using Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solver2019_04
{
  public class Program : BaseAdventSolver
  {
    public Program() : base(2019, 4)
    {
    }

    public override string SolverName => "Day 4: Secure Container";

    public override string SolveTask1()
    {
      Input input = LoadData();

      int count = 0;
      for(int i = input.Number1; i <= input.Number2; i++)
      {
        if(CheckCriteria1(i))
        {
          count++;
        }
      }
      return count.ToString();
    }

    public override string SolveTask2()
    {
      Input input = LoadData();
      
      int count = 0;
      for (int i = input.Number1; i <= input.Number2; i++)
      {
        if (CheckCriteria2(i))
        {
          count++;
        }
      }
      return count.ToString();
    }

    private bool CheckCriteria1(int number)
    {
      string strNumber = number.ToString();
      
      return AdjacentCheck(strNumber) && DecreasingCheck(strNumber) ;
    }

    private bool CheckCriteria2(int number)
    {
      string strNumber = number.ToString();

      return DecreasingCheck(strNumber) && AdjacentCheck2(strNumber);
    }

    private bool DecreasingCheck(string strNumber)
    {     
      for (int i = 0; i < strNumber.Length - 1; i++)
      {
        if (strNumber[i] > strNumber[i + 1])
        {
          return false;
        }
      }
      return true;
    }

    private bool AdjacentCheck(string strNumber)
    {
      bool adjacent = false;
      for (int i = 0; i < strNumber.Length - 1; i++)
      {
        if (strNumber[i] == strNumber[i + 1])
        {
          adjacent = true;
        }
      }
      return adjacent;
    }

    private bool AdjacentCheck2(string strNumber)
    {
      var groups = strNumber.GroupBy(x => x);
      return groups.Any(x=>x.Count() == 2);
    }

    private Input LoadData()
    {
      using (StreamReader sr = new StreamReader(Filename))
      {
        string line = sr.ReadLine();
        string [] parts = line.Split('-');
        return new Input()
        {
          Number1 = int.Parse(parts[0]),
          Number2 = int.Parse(parts[1])
        };
      }
    }
  }

  public class Input
  {
    public int Number1 { get; set; }
    public int Number2 { get; set; }
  }
}
