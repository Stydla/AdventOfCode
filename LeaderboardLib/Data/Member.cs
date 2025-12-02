using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaderboardLib.Data
{
  public class Member
  {

    public int Id { get; set; }
    public string Name { get; set; }
    public long LocalScore { get; set; }
    public long StarCount { get; set; }

    public int MaxPossibleScore { get; private set; }
    public int MinPossibleScore { get; private set; }
    public int LocalScoreComputed { get; private set; }


    public List<Day> Days { get; set; } = new List<Day>();


    public Member(dynamic data)
    {
      Id = (int)data["id"];
      Name = (string)data["name"];
      LocalScore = data["local_score"].Value;
      StarCount = data["stars"].Value;
      foreach(var day in data["completion_day_level"]) 
      {
        Day d = new Day(day);
        Days.Add(d);
      }
      Days.Sort((x,y)=> x.DayNumber.CompareTo(y.DayNumber));
    }

    public void ComputeMaxPossibleScore(List<Member> members, Event ev)
    {
      int scoreTmp = 0;
      int localScoreTmp = 0;
      for(int i = 1; i <= ev.TaskCount; i++)
      {
        int maxForFirst = members.Count;
        int maxForSecond = members.Count;

        Day myDay = Days.FirstOrDefault(x => x.DayNumber == i);

        foreach (Member m in members.Where(x => x != this))
        {
          Day otherDay = m.Days.FirstOrDefault(x => x.DayNumber == i);
          if (otherDay != null)
          {
            if (myDay != null)
            {
              if (otherDay.Stars[0].Timestamp < myDay.Stars[0].Timestamp)
              {
                maxForFirst--;
              }
              if (otherDay.Stars.Count > 1)
              {
                if (myDay.Stars.Count > 1)
                {
                  if (otherDay.Stars[1].Timestamp < myDay.Stars[1].Timestamp)
                  {
                    maxForSecond--;
                  }
                }
                else
                {
                  maxForSecond--;
                }
              }
            }
            else
            {
              maxForFirst--;
              if (otherDay.Stars.Count > 1)
              {
                maxForSecond--;
              }
            }
          }
        }
        if(myDay != null)
        {
          localScoreTmp += maxForFirst;
          if(myDay.Stars.Count > 1) { localScoreTmp += maxForSecond; }
        }
        scoreTmp += maxForFirst + maxForSecond;
      }
      MaxPossibleScore = scoreTmp;
      LocalScoreComputed = localScoreTmp;
    }

    public void ComputeMinPossibleScore(List<Member> members, Event ev)
    {
      MinPossibleScore = LocalScoreComputed + (ev.MaxStars - (int)StarCount);

    }

    public override string ToString()
    {
      return $"Name: {Name} Score: {LocalScore} Stars: {StarCount}";
    }

  }
}
