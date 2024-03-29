﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace AoCLib.BFS
{
  public class BFS<T>
  {
    private Dictionary<IBFSNode<T>, int> Distances { get; set; } = new Dictionary<IBFSNode<T>, int>();
    private Dictionary<IBFSNode<T>, int> DistancesToClosed { get; set; } = new Dictionary<IBFSNode<T>, int>();

    public IBFSNode<T> From { get; }
    public T Context { get; }


    public bool IsReachable(IBFSNode<T> target)
    {
      return Distances.ContainsKey(target);
    }

    public int GetDistance(IBFSNode<T> target)
    {
      return Distances[target];
    }

    public IEnumerable<IBFSNode<T>> GetPath(IBFSNode<T> target)
    {
      List<IBFSNode<T>> path = new List<IBFSNode<T>>();
      IBFSNode<T> current = target;
      while (current != From) 
      {
        path.Add(current);

        int bestDist = int.MaxValue;
        foreach (IBFSNode<T> neighbour in current.GetNeighbours(Context))
        {
          if (Distances.ContainsKey(neighbour))
          {
            int distTmp = Distances[neighbour];
            if(bestDist > distTmp)
            {
              current = neighbour;
              bestDist = distTmp;
            }
          }
        }
      }
      path.Add(From);
      return path.Reverse<IBFSNode<T>>();
    }

    public bool IsReachableClosed(IBFSNode<T> target)
    {
      return DistancesToClosed.ContainsKey(target);
    }
    public int GetDistanceToClosed(IBFSNode<T> target)
    {
      return DistancesToClosed[target];
    }

    public BFS(IBFSNode<T> from, T context)
    {
      From = from;
      Context = context;
      FindShortestDistances();
    }

    private void FindShortestDistances()
    {
      Distances.Add(From, 0);

      HashSet<IBFSNode<T>> current = new HashSet<IBFSNode<T>>() { From };

      int dist = 0;
      while (current.Count > 0)
      {
        HashSet<IBFSNode<T>> next = new HashSet<IBFSNode<T>>();
        dist++;

        foreach (IBFSNode<T> n in current)
        {
          foreach (IBFSNode<T> neighbour in n.GetNeighbours(Context))
          {
            if (neighbour.IsOpen(Context))
            {
              if (!Distances.ContainsKey(neighbour))
              {
                next.Add(neighbour);
                Distances.Add(neighbour, dist);
              }
            } else
            {
              if (!DistancesToClosed.ContainsKey(neighbour))
              {
                DistancesToClosed.Add(neighbour, dist);
              }
            }
          }
        }
        current = next;
      }
    }

  }
}
