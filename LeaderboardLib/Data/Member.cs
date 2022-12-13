using System;
using System.Collections.Generic;
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


    public List<Day> Days { get; set; } = new List<Day>();


    public Member(dynamic data)
    {
      Id = data.id;
      Name = data.name;
      LocalScore = data.local_score.Value;
      StarCount = data.stars.Value;
      foreach(var day in data.completion_day_level) 
      {
        Day d = new Day(day);
        Days.Add(d);
      }
      Days.Sort((x,y)=> x.DayNumber.CompareTo(y.DayNumber));
    }

    public override string ToString()
    {
      return $"Name: {Name} Score: {LocalScore} Stars: {StarCount}";
    }

  }
}
