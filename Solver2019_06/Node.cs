using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solver2019_06
{
  public class Node
  {
    public Node parent { get; set; } = null;
    public List<Node> Childs { get;} = new List<Node>();
    public string Name { get; set; }
    public Node(string name) 
    {
      Name = name;
    }

    public int GetOrbits()
    {
      int orbitCnt = 0;
      Node tmp = parent;
      while(tmp != null)
      {
        orbitCnt++;
        tmp = tmp.parent;
      }
      return orbitCnt;
    }

    public List<Node> NodesToParent()
    {
      List<Node> ret = new List<Node>();
      Node tmp = parent;
      while(tmp != null)
      {
        ret.Add(tmp);
        tmp = tmp.parent;
      }
      return ret;
    }
  }
}
