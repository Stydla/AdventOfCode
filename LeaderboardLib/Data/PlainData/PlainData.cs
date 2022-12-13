using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LeaderboardLib.Data
{
  public class PlainData
  {

    public List<List<Character>> Grid { get; set; }

    public PlainData(Event ev) 
    {
      /*
1) 176 *************************  Ondřej Brožek
      */


      int colSpace = 1;
      int colIndexWidth = ev.Members.Count.ToString().Length + 1;
      int colScoreWidth = ev.Members.Max(x=>x.StarCount).ToString().Length;
      int colStarsWidth = 25;
      int colNameWidth = ev.Members.Max(x=>x.Name.Length);

      int maxX = colIndexWidth + colScoreWidth + colStarsWidth + colNameWidth + 3 * colSpace;
      int maxY = ev.Members.Count + 2;

      InitGrid(maxX, maxY);
      WriteColumnHeader(colIndexWidth + colScoreWidth + 2*colSpace);

      for(int i = 0; i < ev.Members.Count; i++)
      {
        Member m = ev.Members[i];
        WriteMember(m, i, colSpace, colIndexWidth, colScoreWidth, colStarsWidth, colNameWidth);
      }

      Console.WriteLine(Print());
    }

    private void WriteMember(Member m, int index, int colSpace, int colIndexWidth, int colScoreWidth, int colStarsWidth, int colNameWidth)
    {
      string val = String.Format($"{{0, {colIndexWidth - 1}}})", index + 1);
      for(int i = 0; i < val.Length; i++)
      {
        Character ch = Grid[index + 2][i];
        ch.Value = val[i];
        ch.Type = ECharacterType.RowHeader;
      }

      val = String.Format($"{{0, {colScoreWidth}}}", m.LocalScore);
      for (int i = 0; i < val.Length; i++)
      {
        Character ch = Grid[index + 2][colIndexWidth + colSpace + i];
        ch.Value = val[i];
        ch.Type = ECharacterType.RowHeader;
      }

      for (int i = 0; i < m.Name.Length; i++)
      {
        Character ch = Grid[index + 2][colIndexWidth + colSpace + colScoreWidth + colSpace + colStarsWidth + colSpace + i];
        ch.Value = m.Name[i];
        ch.Type = ECharacterType.MemberName;
      }

      for(int i = 0; i < 25; i++)
      {
        int colIndex = i + colIndexWidth + colSpace + colScoreWidth + colSpace;
        Character ch = Grid[index + 2][colIndex];
        ch.Value = '*';
        ch.Type = ECharacterType.Star_0;
      }

      foreach(Day d in m.Days)
      {
        int colIndex = d.DayNumber - 1 + colIndexWidth + colSpace + colScoreWidth + colSpace;
        Character ch = Grid[index + 2][colIndex];
        switch (d.Stars.Count)
        {
          case 0:
            ch.Type = ECharacterType.Star_0;
            break; 
          case 1:
            ch.Type = ECharacterType.Star_1;
            break;
          case 2:
            ch.Type = ECharacterType.Star_2;
            break;
        }
      }

    }

    private void WriteColumnHeader(int offset)
    {
      for (int i = 1; i <= 25; i++)
      {
        int index = i + offset - 1;
        int val = i % 10;
        if (i / 10 > 0)
        {
          Grid[0][index].Value = (char)('0' + i / 10);
          Grid[0][index].Type = ECharacterType.ColumnHeader;
        }
        Grid[1][index].Value = (char)('0' + val);
        Grid[1][index].Type = ECharacterType.ColumnHeader;
      }
    }

    private void InitGrid(int maxX, int maxY)
    {
      Grid = new List<List<Character>>();
      for(int i = 0; i < maxY; i++)
      {
        List<Character> list = new List<Character>();
        for(int j = 0; j < maxX; j++)
        {
          list.Add(new Character());
        }
        Grid.Add(list);
      }
    }

    private string Print()
    {
      StringBuilder sb = new StringBuilder();
      for (int i = 0; i < Grid.Count; i++)
      {
        for (int j = 0; j < Grid[i].Count; j++)
        {
          sb.Append(Grid[i][j].ValueBasedOnType);
        }
        sb.AppendLine();
      } 
      return sb.ToString();
    }
  }
}
