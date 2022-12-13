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
      year = "2022";
      sessionCookie = "session=53616c7465645f5f3584a887342e4b404acd575af7aa7f452956457be4b9754060c8c734ee567822692b186e623b96036e766c3716b32cadb847f7a1801dc301";

      WebClient webClient = new WebClient();
      webClient.Headers.Add(HttpRequestHeader.Cookie, sessionCookie);
      byte[] data = webClient.DownloadData($"https://adventofcode.com/{year}/leaderboard/private/view/452842.json");
      string res = Encoding.UTF8.GetString(data);

      Event ev = new Event(res);
      return ev;
    }

  }
}
