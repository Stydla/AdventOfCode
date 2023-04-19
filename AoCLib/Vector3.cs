using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AoCLib
{
  public class Vector3 : Vector
  {


    public long X 
    {
      get
      {
        return Values[0];
      }
      set
      {
        Values[0] = value;
      }
    }
    public long Y
    {
      get
      {
        return Values[1];
      }
      set
      {
        Values[1] = value;
      }
    }
    public long Z
    {
      get
      {
        return Values[2];
      }
      set
      {
        Values[2] = value;
      }
    }
    public Vector3(long x, long y, long z) : base(3)
    {
      X = x;
      Y = y;
      Z = z;
    }

    public Vector3(string input, string separator) : base(3)
    {
      string [] vals = input.Split(new string[] { separator }, StringSplitOptions.None);
      X = long.Parse(vals[0]);
      Y = long.Parse(vals[1]);
      Z = long.Parse(vals[2]);
    }

    public static Vector3 Zero
    {
      get
      {
        return new Vector3(0, 0, 0);
      }
    }

    public static Vector3 operator +(Vector3 v1, Vector3 v2)
    {
      
      CheckSize(v1, v2);

      Vector3 res = Vector3.Zero;

      for (int i = 0; i < v1.Values.Count; i++)
      {
        res.Values[i] = v1.Values[i] + v2.Values[i];
      }
      return res;
    }

  }
}
