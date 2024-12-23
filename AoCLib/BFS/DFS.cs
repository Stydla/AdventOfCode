using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace AoCLib.BFS
{
  public class DFS<T>
  {

    public IBFSNode<T> From { get; }
    public T Context { get; }


    public DFS(IBFSNode<T> from, T context)
    {
      From = from;
      Context = context;
    }

    public List<List<IBFSNode<T>>> GetAllPaths(IBFSNode<T> target)
    {

      List<IBFSNode<T>> visited = new List<IBFSNode<T>>() { };
      List<List<IBFSNode<T>>> results = new List<List<IBFSNode<T>>>();
      Solve(From, target, visited, results);

      return results;
    }

    private void Solve(IBFSNode<T> current, IBFSNode<T> target, List<IBFSNode<T>> visited, List<List<IBFSNode<T>>> results)
    {
      if (visited.Contains(current))
      {
        return;
      }
      visited.Add(current);

      if (current == target)
      {
        results.Add(visited.ToList());
        visited.RemoveAt(visited.Count - 1);
        return;
      }
      
      foreach(IBFSNode<T> neighbour in current.GetNeighbours(Context))
      {
        Solve(neighbour, target, visited, results);
      }

      visited.RemoveAt(visited.Count - 1);
      return;
      



    }
    



  }
}
