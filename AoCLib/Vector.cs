using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoCLib
{
  public class Vector
  {

    protected List<long> Values = new List<long>();
    public int Size
    {
      get
      {
        return Values.Count;
      }
    }

    protected Vector(int size) 
    {
      for(int i = 0; i < size; i++)
      {
        Values.Add(0);
      }
    }

    public override bool Equals(object obj)
    {
      if(obj == null) return false;
      if(obj is Vector v)
      {
        if (v.Values.Count != Values.Count) return false;
        for(int i = 0; i < Values.Count; i++) 
        {
          if (v.Values[i] != Values[i]) return false;
        }
        return true;
      }
      return false;
    }

    public override int GetHashCode()
    {
      int res = 0;
      int block = int.MaxValue / Values.Count;
      foreach(int value in Values)
      {
        res += ((int)value);
        res *= block;
      }
      return res;
    }

    public override string ToString()
    {
      return $"<{string.Join(",", Values)}>";
    }

    public long ManhattanDistance(Vector vector)
    {
      return ManhattanDistance(this, vector);
    }

    public static long ManhattanDistance(Vector vector1, Vector vector2)
    {
      CheckSize(vector1, vector2);
      
      long dist = 0;
      for (int i = 0; i < vector1.Values.Count; i++)
      {
        dist += Math.Abs(vector1.Values[i] - vector2.Values[i]);
      }
      return dist;
    }

    protected static void CheckSize(Vector v1, Vector v2)
    {
      if (v1.Size != v2.Size) throw new Exception($"Different size of vectors: {v1} {v2}");
    }

    public static Vector operator +(Vector v1, Vector v2)
    { 
      CheckSize(v1 , v2);

      Vector res = new Vector(v1.Size);

      for(int i = 0; i < v1.Values.Count; i++)
      {
        res.Values[i] = v1.Values[i] + v2.Values[i];
      }
      return res;
    }

  }
}
