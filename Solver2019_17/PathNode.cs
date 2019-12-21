using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Solver2019_17.Direction;

namespace Solver2019_17
{
  public class PathNode
  {
    public Dictionary<eDirection, PathNode> Childs { get; set; } = new Dictionary<eDirection, PathNode>();

    public int Count { get; set; }
    public PathNode(int count)
    {
      Count = count;
    }


  }
}
