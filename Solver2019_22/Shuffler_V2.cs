using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solver2019_22
{
  public class Shuffler_V2
  {

    public string Input { get; }
    public List<LinCongFunc> Funcs { get; } = new List<LinCongFunc>();

    public LinCongFunc FinalFunc { get; }
    public LinCongFunc FinalFunc2 { get; private set; }

    public Shuffler_V2(string input, long mod)
    {
      Input = input;

      using (StringReader sr = new StringReader(Input))
      {
        string line;
        while ((line = sr.ReadLine()) != null)
        {
          var func = CreateFunc(line, mod);
          Funcs.Add(func);
        }
      }
      LinCongFunc fTmp = Funcs[0];
      for(int i = 1; i < Funcs.Count; i++)
      {
        fTmp = fTmp.Compose(Funcs[i]);
      }
      FinalFunc = fTmp;
    }

    public void CreateFinalFunc2(long numShuffles)
    {
      var fTmp = FinalFunc;
      List<LinCongFunc> fList = new List<LinCongFunc>();
      while (numShuffles !=0)
      {
        if (numShuffles % 2 == 1)
        {
          fList.Add(fTmp);
        }
        fTmp = fTmp.Compose(fTmp);
        numShuffles /= 2;
      }

      FinalFunc2 = fList[0];
      for (int i = 1; i < fList.Count; i++)
      {
        FinalFunc2 = FinalFunc2.Compose(fList[i]);
      }
    }

    private LinCongFunc CreateFunc(string line, long mod)
    {
      if (line.StartsWith("deal with"))
      {
        string[] words = line.Split(' ');
        int value = int.Parse(words[3]);
        return new LinCongFunc(value, 0, mod);
      }

      if (line.StartsWith("deal into"))
      {
        return new LinCongFunc(-1, -1, mod);
      }

      if (line.StartsWith("cut"))
      {
        string[] words = line.Split(' ');
        int value = int.Parse(words[1]);
        return new LinCongFunc(1, -value, mod);
      }

      return null;
    }


  }
}
