using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaderboardLib.Data
{
  public class Star
  {

    public int Id { get; set; }
    public long Timestamp;
    public DateTime Time { get; set; }


    public Star(dynamic data)
    {
      Id = int.Parse(data.Name);
      Timestamp = data.Value.get_star_ts;
      Time = UnixTimeStampToDateTime(Timestamp);
    }

    public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
    {
      // Unix timestamp is seconds past epoch
      DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
      dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();
      return dateTime;
    }
  }
}
