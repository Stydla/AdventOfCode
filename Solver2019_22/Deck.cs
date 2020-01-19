using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solver2019_22
{
  public class Deck
  {

    public List<long> Cards { get; private set; }
    public long Size { get; }
    public bool IsVirtual { get; }

    public Deck(long size, bool isVirtual = false)
    {
      if(!isVirtual)
      {
        Cards = new List<long>((int)size);
        for (int i = 0; i < size; i++)
        {
          Cards.Add(i);
        }
      }
      Size = size;
      IsVirtual = IsVirtual;
    }

    public void DealNewStack()
    {
      if (IsVirtual) return;

      long cnt = Size / 2;
      for(int i = 0; i < cnt; i++)
      {
        long tmp = Cards[(int)i];
        Cards[i] = Cards[(int)(Size - 1 - i)];
        Cards[(int)(Size - 1 - i)] = tmp;
      }
    }

    public void DealIncrement(int n)
    {
      if (IsVirtual) return;

      List<long> tmp = new List<long>(Cards);
      for(int i = 0; i < Size; i++)
      {
        long index = (i * n) % Size;
        tmp[(int)index] = Cards[i];
      }
      Cards = tmp;
    }

    public void DealCut(long n)
    {
      if (IsVirtual) return;

      long count;
      if(n > 0)
      {
        count = n;
      } else
      {
        count = Size + n;
      }
      List<long> tmp = new List<long>();
      tmp.AddRange(Cards.Skip((int)count));
      tmp.AddRange(Cards.Take((int)count));
      Cards = tmp;
    }

    internal long BacktrackIncrement(int value, long currentPosition)
    {
      long s, t;
      long res = gcdExtended(value, Size, out s, out t);

      long origin = checked(mulmod(currentPosition, Math.Abs(s), Size));
      //long origin2 = checked(currentPosition * s) % Size;
      if(s < 0)
      {
        origin = (Size - origin) % Size;
      }
      //if (origin2 < 0)
      //{
      //  origin2 += Size;
      //}
      //if(origin != origin2)
      //{

      //}
      return origin;
    }

    static long mulmod(long a, long b, long mod)
    {
      long res = 0; // Initialize result  
      a = a % mod;
      while (b > 0)
      {
        // If b is odd, add 'a' to result  
        if (b % 2 == 1)
        {
          res = (res + a) % mod;
        }

        // Multiply 'a' with 2  
        a = (a * 2) % mod;

        // Divide b by 2  
        b /= 2;
      }

      // Return result  
      return res % mod;
    }

    internal long BacktrackDealNewStack(long currentPosition)
    {
      return Size - 1 - currentPosition;
    }

    internal long BacktrackDealCut(int n, long currentPosition)
    {
      return (currentPosition + n + Size) % Size;
    }





    private long gcdExtended(long a, long b, out long x, out long y)
    {
      // Base Case  
      if (a == 0)
      {
        x = 0;
        y = 1;
        return b;
      }

      long x1, y1; // To store results of recursive call  
      long gcd = gcdExtended(b % a, a, out x1, out y1);

      // Update x and y using results of  
      // recursive call  
      x = y1 - checked((b / a) * x1);
      y = x1;

      return gcd;
    }
  }
}
