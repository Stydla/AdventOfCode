using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solver2019_18
{
  public class Map
  {

    public List<List<Field>> Fields { get; } = new List<List<Field>>();
    public List<Door> Doors { get; } = new List<Door>();
    public List<Key> Keys { get; } = new List<Key>();
    public Entrance Entrance { get; }

    public int SizeX { get; }
    public int SizeY { get; }

    public Map(string input)
    {
      using (StringReader sr = new StringReader(input))
      {
        string line;
        int lineIndex = 0;
        while ((line = sr.ReadLine()) != null) {
          List<Field> lTmp = new List<Field>();
          for (int i = 0; i < line.Length; i++)
          {
            Point p = new Point(i, lineIndex);
            char c = line[i];

            Field fTmp = new Field(c, p);
            lTmp.Add(fTmp);

            if (c == '@')
            {
              Entrance entrance = new Entrance(c, p);              
              Entrance = entrance;
              fTmp.Item = entrance;
            }

            if (c >= 'A' && c <= 'Z')
            {
              Door door = new Door(c, p);
              Doors.Add(door);
              fTmp.Item = door;
            }

            if (c >= 'a' && c <= 'z')
            {
              Key key = new Key(c, p);
              Keys.Add(key);
              fTmp.Item = key;
            }
          }
          Fields.Add(lTmp);
          lineIndex++;
        }
      }
      SizeY = Fields.Count;
      if (SizeY > 0)
      {
        SizeX = Fields[0].Count;
      } else
      {
        SizeX = 0;
      }

    }
    public string Print()
    {
      StringBuilder sb = new StringBuilder();
      for(int i = 0; i < SizeY; i++)
      {
        for(int j = 0; j < SizeX; j++)
        {
          Field f = Fields[i][j];
          sb.Append(f.Print());
        }
        sb.AppendLine();
      }
      return sb.ToString();
    }
  }
}
