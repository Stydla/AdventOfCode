using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaderboardLib.Data
{
  public class Character
  {
    public char Value { get; set; } = ' ';
    public ECharacterType Type { get; set; } = ECharacterType.None;
    public char ValueBasedOnType 
    { 
      get
      {
        switch (Type)
        {
          case ECharacterType.Star_0:
            return ' ';
          case ECharacterType.Star_1:
            return '1';
          case ECharacterType.Star_2:
            return '2';
          default:
            return Value;
        }
      }
    }
  }
}
