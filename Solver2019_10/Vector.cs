using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solver2019_10
{
  public class Vector
  {
    public int X { get; set; }
    public int Y { get; set; }
    public Vector(int x, int y)
    {
      X = x;
      Y = y;
    }

    public static Vector operator +(Vector v1, Vector v2)
    {
      return new Vector(v1.X + v2.X, v1.Y + v2.Y);
    }
    public static Vector operator -(Vector v1, Vector v2)
    {
      return new Vector(v1.X + v2.X, v1.Y + v2.Y);
    }

    public static double Angle(Vector v1, Vector v2)
    {
      double dotProd = v1.X * v2.X + v1.Y * v2.Y;
      double magnitude1 = Math.Sqrt(v1.X * v1.X + v1.Y * v1.Y);
      double magnitude2 = Math.Sqrt(v2.X * v2.X + v2.Y * v2.Y);
      double angle = Math.Acos(dotProd / (magnitude1 * magnitude2)) * (v2.X < v1.X ? -1 : 1);
      
      return  angle >= 0 ? angle : Math.PI *2 + angle;
    }

    public int ManhatanDistance()
    {
      return Math.Abs(X) + Math.Abs(Y);
    }

    internal Vector Reduce()
    {
      int xTmp = X, yTmp = Y;
      int max = Math.Max(Math.Abs(X), Math.Abs(Y));
      for (int i = 2; i <= max;)
      {
        if(xTmp % i == 0 && yTmp % i == 0)
        {
          xTmp /= i;
          yTmp /= i;
        } else
        {
          i++;
        }
      }
      return new Vector(xTmp, yTmp);
    }

    public override string ToString()
    {
      return $"{X},{Y}";
    }
  }
}
