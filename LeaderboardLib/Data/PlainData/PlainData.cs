using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

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
      int colStarsWidth = ev.MaxStars / 2;
      int colNameWidth = ev.Members.Max(x=>x.Name.Length);
      int colMaxScoreWidth = 5;
      int colMinScoreWidth = 5;

      int maxX = colIndexWidth + colScoreWidth + colStarsWidth + colNameWidth + 3 * colSpace + colMaxScoreWidth + 3 * colSpace + colMinScoreWidth;
      int maxY = ev.Members.Count + 3;

      InitGrid(maxX, maxY);
      int maxTextIndex = colIndexWidth + colSpace + colScoreWidth + colSpace + colStarsWidth + colSpace + colNameWidth;
      int minTextIndex = maxTextIndex + colMaxScoreWidth;
      WriteColumnHeader(colIndexWidth + colScoreWidth + 2*colSpace, maxTextIndex, minTextIndex, $"Event: {ev.EventName}", ev.MaxStars / 2);

      for(int i = 0; i < ev.Members.Count; i++)
      {
        Member m = ev.Members[i];
        WriteMember(m, i+1, colSpace, colIndexWidth, colScoreWidth, colStarsWidth, colNameWidth, colMaxScoreWidth, colMinScoreWidth, ev.MaxStars / 2);
      }

      Console.WriteLine(Print());
    }

    private void WriteMember(Member m, int index, int colSpace, int colIndexWidth, int colScoreWidth, int colStarsWidth, int colNameWidth, int colMaxScoreWidth, int colMinScoreWidth, int maxStars)
    {
      string val = String.Format($"{{0, {colIndexWidth - 1}}})", index);
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

      val = String.Format($"{{0, {colMaxScoreWidth}}}", m.MaxPossibleScore);
      for (int i = 0; i < val.Length; i++)
      {
        Character ch = Grid[index + 2][colIndexWidth + colSpace + colScoreWidth + colSpace + colStarsWidth + colSpace + i + colNameWidth];
        ch.Value = val[i];
        ch.Type = ECharacterType.RowHeader;
      }
      val = String.Format($"{{0, {colMinScoreWidth}}}", m.MinPossibleScore);
      for (int i = 0; i < val.Length; i++)
      {
        Character ch = Grid[index + 2][colIndexWidth + colSpace + colScoreWidth + colSpace + colStarsWidth + colSpace + i + colNameWidth + colMaxScoreWidth];
        ch.Value = val[i];
        ch.Type = ECharacterType.RowHeader;
      }

      for (int i = 0; i < maxStars; i++)
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

    private void WriteColumnHeader(int offset, int maxTextIndex, int minTextIndex, string eventName, int maxStars)
    {
      for(int i = 0; i < eventName.Length; i++)
      {
        Grid[0][i].Value = eventName[i];
        Grid[0][i].Type = ECharacterType.ColumnHeader;
      }

      for (int i = 1; i <= maxStars; i++)
      {
        int index = i + offset - 1;
        int val = i % 10;
        if (i / 10 > 0)
        {
          Grid[1][index].Value = (char)('0' + i / 10);
          Grid[1][index].Type = ECharacterType.ColumnHeader;
        }
        Grid[2][index].Value = (char)('0' + val);
        Grid[2][index].Type = ECharacterType.ColumnHeader;
      }

      string valStr = "  Max";
      for (int i = 0; i < valStr.Length; i++)
      {
        Character ch = Grid[1][maxTextIndex + i];
        ch.Value = valStr[i];
        ch.Type = ECharacterType.ColumnHeader;
      }

      valStr = "  Min";
      for (int i = 0; i < valStr.Length; i++)
      {
        Character ch = Grid[1][minTextIndex + i];
        ch.Value = valStr[i];
        ch.Type = ECharacterType.ColumnHeader;
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
