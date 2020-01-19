using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solver2019_22
{
  public class Program : BaseAdventSolver
  {
    public Program() : base(2019, 22)
    {
    }

    public override string SolverName => "Day 22: Slam Shuffle";

    public override string SolveTask1(string InputData)
    {
      Shuffler_V2 sflv2 = new Shuffler_V2(InputData, 10007);

      long res = sflv2.FinalFunc.Solve(2019);
      return res.ToString();

    }

    public override string SolveTask2(string InputData)
    {
      long cards = 119315717514047;
      long shuffleCnt = 101741582076661;

      Shuffler_V2 sflv2 = new Shuffler_V2(InputData, cards);
      sflv2.CreateFinalFunc2(shuffleCnt);

      long resInverse = sflv2.FinalFunc2.Inverse().Solve(2020);

      return resInverse.ToString();

      //TestA(InputData);

      //Shuffler_V2 sflv2 = new Shuffler_V2(InputData, 119315717514047);

      //Deck d = new Deck(119315717514047, true);

      //Dictionary<long, long> dict = new Dictionary<long, long>();

      //Shuffler sfl = new Shuffler(InputData);

      //long indexTmp = 2020;
      //long prevIndex = sfl.BacktrackCard(d, indexTmp);
      //while (!dict.ContainsKey(indexTmp))
      //{
      //  dict.Add(indexTmp, prevIndex);
      //  indexTmp = prevIndex;
      //  prevIndex = sfl.BacktrackCard(d, indexTmp);
      //}
      

      //return prevIndex.ToString();
    }

    private void TestX(string input)
    {
      int cards = 10007;
      int shuffleCnt = 3;

      Shuffler_V2 sflv2 = new Shuffler_V2(InputData, cards);
      sflv2.CreateFinalFunc2(shuffleCnt);

      long resInverse = sflv2.FinalFunc2.Inverse().Solve(2020);

      //Deck d = new Deck(cards);
      //Shuffler sfl = new Shuffler(input);
      //for(int i = 0; i < shuffleCnt; i++)
      //{
      //  sfl.Shuffle(d);
      //}
      //long res2 = d.Cards.IndexOf(2019);
      
    }

    private void TestA(string input)
    {
      int deckSize = 10007;
      Deck d = new Deck(deckSize);

      Shuffler sfl = new Shuffler(input);
      sfl.Shuffle(d);

      List<long> tmp = new List<long>(d.Cards);
      for (int i = 0; i < deckSize; i++)
      {
        long pos = sfl.BacktrackCard(d, i);
        tmp[(int)pos] = d.Cards[i];
      }
      System.Diagnostics.Debug.WriteLine(string.Join(",", tmp));
    }

    private void Test1()
    {
      int deckSize = 11;
      Deck d = new Deck(deckSize);

      Shuffler sfl = new Shuffler("deal with increment 3");
      sfl.Shuffle(d);

      List<long> tmp = new List<long>(d.Cards);
      for(int i = 0; i < deckSize; i++)
      {
        long pos = sfl.BacktrackCard(d, i);
        tmp[(int)pos] = d.Cards[i];
      }
      System.Diagnostics.Debug.WriteLine(string.Join(",", tmp));
    }

    private void Test2()
    {
      int deckSize = 11;
      Deck d = new Deck(deckSize);

      Shuffler sfl = new Shuffler("cut 3");
      sfl.Shuffle(d);

      List<long> tmp = new List<long>(d.Cards);
      for (int i = 0; i < deckSize; i++)
      {
        long pos = sfl.BacktrackCard(d, i);
        tmp[(int)pos] = d.Cards[i];
      }
      System.Diagnostics.Debug.WriteLine(string.Join(",", tmp));
    }

    private void Test3()
    {
      int deckSize = 11;
      Deck d = new Deck(deckSize);

      Shuffler sfl = new Shuffler("cut -3");
      sfl.Shuffle(d);

      List<long> tmp = new List<long>(d.Cards);
      for (int i = 0; i < deckSize; i++)
      {
        long pos = sfl.BacktrackCard(d, i);
        tmp[(int)pos] = d.Cards[i];
      }
      System.Diagnostics.Debug.WriteLine(string.Join(",", tmp));
    }

    private void Test4()
    {
      int deckSize = 11;
      Deck d = new Deck(deckSize);

      Shuffler sfl = new Shuffler("deal into new stack");
      sfl.Shuffle(d);

      List<long> tmp = new List<long>(d.Cards);
      for (int i = 0; i < deckSize; i++)
      {
        long pos = sfl.BacktrackCard(d, i);
        tmp[(int)pos] = d.Cards[i];
      }
      System.Diagnostics.Debug.WriteLine(string.Join(",", tmp));
    }
  }
}
