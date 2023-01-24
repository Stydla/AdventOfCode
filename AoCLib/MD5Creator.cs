using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AoCLib
{
  public class MD5Creator
  {
    public static string Create(string input)
    {
      using(MD5 MD5 = MD5.Create())
      {
        byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
        byte[] hashBytes = MD5.ComputeHash(inputBytes);

        StringBuilder sb = new System.Text.StringBuilder();
        for (int i = 0; i < hashBytes.Length; i++)
        {
          sb.Append(hashBytes[i].ToString("X2"));
        }
        return sb.ToString().ToLower();
      }
    }
  }
}
