using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaderboardLib.Data
{
  public class Event
  {

    public string Id { get; set; }
    public List<Member> Members { get; set; } = new List<Member>();
    public string EventName { get; set; }

    public Event(string input)
    {
      dynamic stuff = JsonConvert.DeserializeObject(input);

      //Id = (string)stuff.owner_id;
      EventName = (string)stuff["event"];
      foreach (var member in stuff["members"])
      {
        //dynamic val = member as dynamic;
        Member tmp = new Member(member.Value);
        Members.Add(tmp);
      } 
      Members.Sort((x,y) => -(int)x.LocalScore + (int)y.LocalScore);

      foreach(Member member in Members)
      {
        member.ComputeMaxPossibleScore(Members);
      }
    }

    public override string ToString()
    {
      return EventName;
    }

    public PlainData GetPlainData()
    {
      return new PlainData(this);
    }

  }
}
