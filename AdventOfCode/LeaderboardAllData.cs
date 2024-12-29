using LeaderboardLib;
using LeaderboardLib.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
  internal class LeaderboardAllData
  {

    public List<Event> Events = new List<Event>();

    public LeaderboardAllData()
    {
      string session = File.ReadAllText("session.txt");

      for(int i = 2015; i <= DateTime.Now.Year; i++)
      {
        Events.Add(Leaderboard.LoadEvent(i.ToString(), session));
      }

    }

  }
}
