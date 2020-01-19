using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Solver2019_22
{
  public class LinCongFunc
  {
    public long A;
    public long B;
    public long Mod;

    public LinCongFunc(long a, long b, long mod)
    {
      A = a < 0 ? a + mod : a;
      B = b < 0 ? b + mod : b;
      Mod = mod;
    }

    public LinCongFunc Compose(LinCongFunc inner)
    {
      if(Mod != inner.Mod)
      {
        throw new Exception("Different mod");
      }
      long a = mulmod(A, inner.A, Mod);//(A * inner.A) % Mod;
      long b = (mulmod(B, inner.A, Mod) + inner.B) % Mod;// (B * inner.A + inner.B) % Mod;

      return new LinCongFunc(a, b, Mod);
    }

    public long Solve(long x)
    {
      //return (((A * x + B)  % Mod) + Mod) % Mod;
      return (((mulmod(A , x, Mod) + B) % Mod) + Mod) % Mod;
    }

    private long mulmod(long a, long b, long mod)
    {
      long res = 0; // Initialize result  
      a = a % mod;
      while (b != 0)
      {
        // If b is odd, add 'a' to result  
        if (b % 2 == 1 )
        {
          res = (res + a) % mod;
        } else if (b % 2 == -1)
        {
          res = (res - a) % mod;
        }

        // Multiply 'a' with 2  
        a = (a * 2) % mod;

        // Divide b by 2  
        b /= 2;
      }

      // Return result  
      return res % mod;
    }

    public LinCongFunc Inverse()
    {
      BigInteger a_inverse = BigInteger.ModPow(A, Mod - 2, Mod);

      return new LinCongFunc((long)a_inverse, -mulmod((long)a_inverse, B, Mod), Mod);
    }

  }
}
