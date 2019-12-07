using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solver2019_06
{
  public class Tree
  {

    public Node Root { get; set; }

    private Dictionary<string, Node> NodeList { get; } = new Dictionary<string, Node>();

    public Tree(List<InputItem> data)
    {

      foreach(var item in data)
      {
        Node itemCom, itemOrbit;

        if(NodeList.ContainsKey(item.ObjCom))
        {
          itemCom = NodeList[item.ObjCom];
        } else
        {
          itemCom = new Node(item.ObjCom);
          NodeList.Add(item.ObjCom, itemCom);
        }

        if (NodeList.ContainsKey(item.ObjOrbit))
        {
          itemOrbit = NodeList[item.ObjOrbit];
        }
        else
        {
          itemOrbit = new Node(item.ObjOrbit);
          NodeList.Add(item.ObjOrbit, itemOrbit);
        }

        itemCom.Childs.Add(itemOrbit);
        itemOrbit.parent = itemCom;
      }

      var roots = NodeList.Values.Where(x => x.parent == null);
      if(roots.Count() != 1)
      {
        throw new Exception($"More then 1 COM. {roots.Count()}");
      } else
      {
        Root = roots.First();
      }

    }

    internal int TotalOrbits()
    {
      int cnt = 0;
      foreach(Node n in NodeList.Values)
      {
        cnt += n.GetOrbits();
      }
      return cnt;
    }

    internal int OrbitalTransfers(string obj1, string obj2)
    {

      List<Node> nodesToParent1 = NodeList[obj1].NodesToParent();
      List<Node> nodesToParent2 = NodeList[obj2].NodesToParent();

      int i = 0;
      while(nodesToParent1[nodesToParent1.Count - 1 - i] == nodesToParent2[nodesToParent2.Count - 1 -i])
      {
        i++;
      }

      return nodesToParent1.Count + nodesToParent2.Count - i * 2;


    }
  }
}
