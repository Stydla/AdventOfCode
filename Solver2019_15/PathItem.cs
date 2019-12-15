using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Solver2019_15.Direction;

namespace Solver2019_15
{
  public class PathItem
  {
    public PathItem Parent { get; set; }
    public Field Field { get; set; }

    public Dictionary<PathItem, eDirection> Childs { get; set; } = new Dictionary<PathItem, eDirection>();

    public PathItem(Field field, PathItem parent)
    {
      Field = field;
      Parent = parent;
    }

    public PathItem Find(Field f)
    {
      if (Field == f) return this;
      foreach (var pi in Childs.Keys)
      {
        PathItem res = pi.Find(f);
        if (res != null) return res;
      }
      return null;
    }

    public bool Contains(Field f)
    {
      if (Field == f) return true;
      foreach(var pi in Childs.Keys)
      {
        if (pi.Contains(f)) return true;
      }
      return false;
    }

    internal int GetDepth()
    {
      if (Childs.Count == 0) return 0;
      return Childs.Max(x => x.Key.GetDepth()) + 1;
    }
  }
}
