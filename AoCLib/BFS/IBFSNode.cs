using System.Collections.Generic;

namespace AoCLib.BFS
{
  public interface IBFSNode<T>
  {
    IEnumerable<IBFSNode<T>> GetNeighbours(T context);
    bool IsOpen(T context);
  }
}