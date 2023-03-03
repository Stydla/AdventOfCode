using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AoCLib
{
  public class PriorityQueue<T>
    where T : IComparable<T>
  {

    public int TotalCount { get; private set; }
    public SortedDictionary<T, Queue<T>> Data { get; }

    public PriorityQueue()
    {
      Data = new SortedDictionary<T, Queue<T>>();
      TotalCount = 0;
    }

    public bool IsEmpty()
    {
      return TotalCount == 0;
    }

    public T Dequeue()
    {
      if (IsEmpty())
      {
        throw new Exception("Queue is Empty");
      }
      else
      {
        foreach (Queue<T> q in Data.Values)
        {
          
          if (q.Count > 0)
          {
            TotalCount--;
            T tmp = q.Dequeue();
            if(q.Count== 0)
            {
              Data.Remove(tmp);
            }
            return tmp;
          }
        }
      }
      return default(T);
    }


    public T Peek()
    {
      if (IsEmpty())
      {
        throw new Exception("Queue is Emptyg");
      }
      else
      {
        foreach (Queue<T> q in Data.Values)
        {
          if (q.Count > 0)
            return q.Peek();
        }
      }
      
      return default(T); 
    }

    public void Enqueue(T item)
    {
      if (!Data.ContainsKey(item))
      {
        Data.Add(item, new Queue<T>());
      }
      Data[item].Enqueue(item);
      TotalCount++;
    }

  }
}
