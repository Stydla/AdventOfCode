using System;
using System.Collections.Generic;
using static Solver2019_15.Direction;

namespace Solver2019_15
{
  public class MapPath
  {
    public PathItem Root;

    public MapPath(PathItem root)
    {
      Root = root;
    }

    public bool Contains(Field f)
    {
      return Root.Contains(f);
    }

    public PathItem Find(Field f)
    {
      return Root.Find(f);
    }

    public List<eDirection> GetDirectionList(Field f2)
    {
      List<eDirection> ret = new List<eDirection>();
      var pi = Find(f2);
      while(pi.Parent != null)
      {
        ret.Add(pi.Parent.Childs[pi]);
        pi = pi.Parent;
      }
      ret.Reverse();
      return ret;
    }

    public int GetDepth()
    {
      return Root.GetDepth();
    }
  }
}