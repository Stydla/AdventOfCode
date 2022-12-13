using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LeaderboardLib.Data
{
  public class Day
  {

    public int DayNumber { get; set; }
    public List<Star> Stars { get; set; } = new List<Star>();

    public Day(dynamic data) 
    {
      DayNumber = int.Parse(data.Name);
      foreach(var star in data.Value)
      {
        Star s = new Star(star);
        Stars.Add(s);
      }
      Stars.Sort((x,y) => x.Id- y.Id);
    }

    public override string ToString()
    {
      StringBuilder sb = new StringBuilder();
      sb.Append(DayNumber);
      sb.Append(":");
      foreach (Star s in Stars)
      {
        sb.Append("*");
      }
      return sb.ToString();
    }
  }
}
