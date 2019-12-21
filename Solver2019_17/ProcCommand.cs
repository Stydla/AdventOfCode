using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Solver2019_17
{
  internal class ProcCommand
  {

    public string FunctionA { get; set; }
    public string FunctionB { get; set; }
    public string FunctionC { get; set; }

    public string FunctionACount { get; set; }
    public string FunctionBCount { get; set; }
    public string FunctionCCount { get; set; }

    public string Input { get; set; }

    public string Main
    {
      get
      {
        string tmp = Input;
        StringBuilder sb = new StringBuilder();
        while(tmp.Length > 0)
        {
          if(tmp.StartsWith(FunctionA)) {
            tmp = tmp.Substring(FunctionA.Length);
            sb.Append('A');
          }
          if (tmp.StartsWith(FunctionB))
          {
            tmp = tmp.Substring(FunctionB.Length);
            sb.Append('B');
          }
          if (tmp.StartsWith(FunctionC))
          {
            tmp = tmp.Substring(FunctionC.Length);
            sb.Append('C');
          }
        }
        return sb.ToString();
      }
    }
    


    public bool IsValid { get; set; } = false;


    internal static List<ProcCommand> Create(List<int> cmd)
    {
      List<ProcCommand> res = new List<ProcCommand>();
      string command = string.Join("", cmd.Select(x => (char)x));
      int maxFLength = 12;
      for (int i = 1; i < maxFLength + 1; i++)
      {
        string fA = command.Substring(0, i);
        //if (fA.EndsWith("R") || fA.EndsWith("L")) continue;

        string rest1 = command;
        int fACount = 0;
        while (RemoveStart(rest1, fA).Length != rest1.Length)
        {
          rest1 = RemoveStart(rest1, fA);
          fACount++;
        }

        for (int j = 1; j < maxFLength + 1; j++)
        {
          string fB = rest1.Substring(0, j);
          //if (fB.EndsWith("R") || fB.EndsWith("L")) continue;

          string rest2 = rest1;
          int fBCount = 0;
          bool changed = true;
          while (changed)
          {
            changed = false;
            while (RemoveStart(rest2, fB).Length != rest2.Length)
            {
              rest2 = RemoveStart(rest2, fB);
              fBCount++;
              changed = true;
            }
            while (RemoveStart(rest2, fA).Length != rest2.Length)
            {
              rest2 = RemoveStart(rest2, fA);
              fACount++;
              changed = true;
            }
          }
          
          for (int k = 1; k < maxFLength + 1; k++)
          {
            string fC = rest2.Substring(0, k);
            //if (fC.EndsWith("R") || fC.EndsWith("L")) continue;

            string rest3 = rest2;
            int fCCount = 0;
            changed = true;
            while (changed)
            {
              changed = false;
              while (RemoveStart(rest3, fB).Length != rest3.Length)
              {
                rest3 = RemoveStart(rest3, fB);
                fBCount++;
                changed = true;
              }
              while (RemoveStart(rest3, fA).Length != rest3.Length)
              {
                rest3 = RemoveStart(rest3, fA);
                fACount++;
                changed = true;
              }
              while (RemoveStart(rest3, fC).Length != rest3.Length)
              {
                rest3 = RemoveStart(rest3, fC);
                fCCount++;
                changed = true;
              }
            }
           // System.Diagnostics.Debug.WriteLine(rest3);

            if (string.IsNullOrEmpty(rest3))
            {
              if(fACount + fBCount + fCCount <= 10)
              {
                res.Add( new ProcCommand()
                {
                  FunctionA = fA,
                  FunctionB = fB,
                  FunctionC = fC,
                  Input = command,
                  IsValid = true
                });
              }
            }
          }
        }
      }
      return res;
    }

    private static string RemoveStart(string str, string val)
    {
      if(str.StartsWith(val)) {
        return str.Substring(val.Length);
      }
      return str;
    }


  }


}