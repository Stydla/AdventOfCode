using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solver2019_22
{
  public class Shuffler
  {

    public string Input { get; }
    public List<string> Commands { get; } = new List<string>();

    public Shuffler(string input)
    {
      Input = input;

      using (StringReader sr = new StringReader(Input))
      {
        string line;
        while((line = sr.ReadLine()) != null)
        {
          Commands.Add(line);
        }
      }

    }

    public void Shuffle(Deck deck)
    {
      foreach(var cmd in Commands)
      {
        if(cmd.StartsWith("deal with"))
        {
          string[] words = cmd.Split(' ');
          int value = int.Parse(words[3]);
          deck.DealIncrement(value);
        }

        if(cmd.StartsWith("deal into"))
        {
          deck.DealNewStack();
        }

        if(cmd.StartsWith("cut"))
        {
          string[] words = cmd.Split(' ');
          int value = int.Parse(words[1]);
          deck.DealCut(value);
        }

      }
    }

    public long BacktrackCard(Deck deck, long position)
    {
      long currentPosition = position;
      for(int i = Commands.Count - 1; i >= 0; i--)
      {
        string cmd = Commands[i];
        if (cmd.StartsWith("deal with"))
        {
          string[] words = cmd.Split(' ');
          int value = int.Parse(words[3]);
          currentPosition = deck.BacktrackIncrement(value, currentPosition);
        }

        if (cmd.StartsWith("deal into"))
        {
          currentPosition = deck.BacktrackDealNewStack(currentPosition);
        }

        if (cmd.StartsWith("cut"))
        {
          string[] words = cmd.Split(' ');
          int value = int.Parse(words[1]);
          currentPosition = deck.BacktrackDealCut(value, currentPosition);
        }
      }
      return currentPosition;
    }

  }
}
