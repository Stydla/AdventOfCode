using LeaderboardLib.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LeaderboardLib
{
  public class Leaderboard
  {
    public static Event LoadEvent(string year, string sessionCookie)
    {
      WebClient webClient = new WebClient();
      webClient.Headers.Add(HttpRequestHeader.Cookie, sessionCookie);
      byte[] data = webClient.DownloadData($"https://adventofcode.com/{year}/leaderboard/private/view/452842.json");
      string res = Encoding.UTF8.GetString(data);

      Event ev = new Event(res);
      return ev;
    }

  }
}
