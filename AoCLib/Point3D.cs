using AoCLib.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoCLib
{
  public class Point3D
  {

    public int X { get; set; }
    public int Y { get; set; }
    public int Z { get; set; }

    public Point3D(int x, int y, int z)
    {
      X = x;
      Y = y;
      Z = z;
    }


    public Point3D(Point3D orig)
    {
      this.X = orig.X;
      this.Y = orig.Y;
      this.Z = orig.Z;
    }

    public override string ToString()
    {
      return $"{X},{Y},{Z}";
    }

    public static Point3D Parse(string input)
    {
      string[] coords = input.Split(',');
      return new Point3D(int.Parse(coords[0]), int.Parse(coords[1]), int.Parse(coords[2]));
    }

    public override bool Equals(object obj)
    {
      if (obj is Point3D p)
      {
        return p.X == X && p.Y == Y && p.Z == Z;
      }
      return false;
    }

    public override int GetHashCode()
    {
      return X ^ Y << 10 ^ Z << 20;
    }




    public Point3D F(int cnt)
    {
      return new Point3D(X, Y - cnt, Z);
    }

    public Point3D L(int cnt)
    {
      return new Point3D(X - cnt, Y, Z);
    }

    public Point3D B(int cnt)
    {
      return new Point3D(X, Y + cnt, Z);
    }

    public Point3D R(int cnt)
    {
      return new Point3D(X + cnt, Y, Z);
    }

    public Point3D U(int cnt)
    {
      return new Point3D(X, Y, Z + cnt);
    }

    public Point3D D(int cnt)
    {
      return new Point3D(X, Y , Z - cnt);
    }




    public Point3D Move(EDirection3D_6 dir)
    {
      switch (dir)
      {
        case EDirection3D_6.FRONT:
          return F(1);
        case EDirection3D_6.LEFT:
          return L(1);
        case EDirection3D_6.BACK:
          return B(1);
        case EDirection3D_6.RIGHT:
          return R(1);
        case EDirection3D_6.UP:
          return U(1);
        case EDirection3D_6.DOWN:
          return D(1);
      }
      throw new Exception($"Missin direction {dir}");
    }

    public List<Point3D> GetNeightbours6()
    {
      return new List<Point3D>
      {
        F(1), L(1), B(1), R(1), U(1), D(1)
      };
    }

    public List<Point3D> GetNeightbours26()
    {
      List<Point3D> ret = new List<Point3D>();  
      for(int i = 0; i < 3; i++)
      {
        for(int j = 0; j < 3; j++)
        {
          for(int k = 0; k < 3; k++)
          {
            if (i != 1 && j != 1 && k!= 1)
            {
              ret.Add(Move(k, j, i));
            }
          }
        }
      }
      return ret;
    }

    public long ManhattanDistance(Point3D other)
    {
      return Math.Abs(X - other.X) + Math.Abs(Y - other.Y) + Math.Abs(Z - other.Z);
    }

    public Point3D Move(EDirection3D_6 dir, int cnt)
    {
      switch (dir)
      {
        case EDirection3D_6.FRONT:
          return F(cnt);
        case EDirection3D_6.LEFT:
          return L(cnt);
        case EDirection3D_6.BACK:
          return B(cnt);
        case EDirection3D_6.RIGHT:
          return R(cnt);
        case EDirection3D_6.UP:
          return U(cnt);
        case EDirection3D_6.DOWN:
          return D(cnt);
      }
      throw new Exception($"Missin direction {dir}");
    }

    public Point3D Move(int x, int y, int z)
    {
      return new Point3D(X + x, Y + y, Z + z);
    }

		public long Distance(Point3D location)
		{
			return (long)Math.Sqrt(Math.Pow(X - location.X, 2) + Math.Pow(Y - location.Y, 2) + Math.Pow(Z - location.Z, 2));
		}
	}
}
