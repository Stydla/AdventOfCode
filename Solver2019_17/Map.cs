using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solver2019_17
{
  public class Map
  {
    public List<List<Field>> Fields { get; set; } = new List<List<Field>>();
    public List<Field> FieldList { get; set; } = new List<Field>();

    public int SizeX { get; set; }
    public int SizeY { get; set; }

    public Point InitialRobotLocation { get; }

    public List<Field> Intersects { get; set; }

    public bool IsIntersect(Point p)
    {
      if(Intersects == null)
      {
        Intersects = GetIntersectPoints();
      }
      return Intersects.Any(x => x.Location.X == p.X && x.Location.Y == p.Y);
    }

    public Map(List<int> tmp)
    {
      Fields.Add(new List<Field>());
      int x = 0;
      int y = 0;
      Point p;
      Field f;
      foreach (int i in tmp)
      {
        switch(i)
        {
          
          case '.':
            p = new Point(x, y);
            f = new Field(p, eFieldType.Empty);
            Fields[y].Add(f);
            x++;
            break;
          case 'v':
          case '^':
          case '>':
          case '<':
            p = new Point(x, y);
            f = new Field(p, eFieldType.Scaffold);
            InitialRobotLocation = p;
            x++;
            Fields[y].Add(f);
            break;
          case '#':
            p = new Point(x, y);
            f = new Field(p, eFieldType.Scaffold);
            x++;
            Fields[y].Add(f);
            break;
          case 10:
            y++;
            x = 0;
            Fields.Add(new List<Field>());
            break;
          default:
            throw new Exception("Invalid input");
        }
      }

      var emptyFields = Fields.Where(z => z.Count == 0);
      Fields = Fields.Except(emptyFields).ToList();
      
      SizeY = Fields.Count;
      SizeX = Fields.Count > 0 ? Fields[0].Count : 0;

      for (int i = 0; i < SizeY; i++)
      {
        for (int j = 0; j < SizeX; j++)
        {
          FieldList.Add(Fields[i][j]);
        }
      }
    }

    internal Field GetField(Point p)
    {
      return FieldList.Where(x => x.Location.X == p.X && x.Location.Y == p.Y).FirstOrDefault();
    }

    public string Print(Robot r)
    {
      StringBuilder sb = new StringBuilder();
      for(int i = 0; i < SizeY; i++)
      {
        for(int j = 0; j < SizeX; j++)
        {
          if(r.Location.X == j && r.Location.Y == i)
          {
            sb.Append(Direction.GetCharDirection(r.Direction));
          } else
          {
            sb.Append(Fields[i][j].GetCharValue());
          }
        }
        sb.AppendLine();
      }
      return sb.ToString();
    }

    private static eFieldType[,] INTERSECT_PATTERN = new eFieldType[,] 
    { 
      { eFieldType.Empty,   eFieldType.Scaffold, eFieldType.Empty },
      { eFieldType.Scaffold,eFieldType.Scaffold, eFieldType.Scaffold },
      { eFieldType.Empty,   eFieldType.Scaffold, eFieldType.Empty },
    };

    public List<Field> GetIntersectPoints()
    {
      List<Field> ret = new List<Field>();

      for(int i = 0; i < SizeY - 2; i++)
      {
        for(int j = 0; j < SizeX - 2; j++)
        {
          bool add = true;
          for(int a = 0; a < 3; a++)
          {
            for(int b = 0; b < 3; b++)
            {
              if (Fields[i + a][j + b].Type != INTERSECT_PATTERN[a, b])
              {
                add = false;
              }
            }
          }
          if (add) 
          {
            ret.Add(Fields[i + 1][j + 1]);
          }
        }
      }

      return ret;
    }



    



  }
}
