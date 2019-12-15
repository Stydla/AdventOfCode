using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solver2019_15
{
  public class Map
  {

    public List<Field> Fields { get; set; } = new List<Field>();

    public Field GetField(Point p)
    {
      return Fields.Where(x => x.Location.X == p.X && x.Location.Y == p.Y).FirstOrDefault();
    }

    public MapPath FindPath(Field f1)
    {
      MapPath mp = new MapPath(new PathItem(f1, null));

      List<Field> current = new List<Field>();
      List<Field> next = new List<Field>();
      current.Add(f1);

      while(current.Count > 0)
      {
        foreach(var field in current)
        {
          //if(!mp.Contains(field))
          //{
          //  next.Add(field);
          //}
          
          PathItem currentPI = mp.Find(field);
          var emptyNeighbours = field.EmptyNeighbours;
          foreach(var emptyNeighbour in emptyNeighbours)
          {
            PathItem pi = mp.Find(emptyNeighbour.Value);
            if(pi == null)
            {

              pi = new PathItem(emptyNeighbour.Value, currentPI);
              currentPI.Childs.Add(pi, emptyNeighbour.Key);
              next.Add(pi.Field);
            }
          }
        }
        current = next;
        next = new List<Field>();
      }
      return mp;
    }

    public List<Field> GetUnsearchedFields()
    {
      return Fields.Where(x => !x.IsSearched()).ToList();
    }

    

    public List<List<char>> Print(Point robotLocation)
    {
      int maxX = Fields.Max(x => x.Location.X);
      int maxY = Fields.Max(x => x.Location.Y);
      int minX = Fields.Min(x => x.Location.X);
      int minY = Fields.Min(x => x.Location.Y);

      int sizeX = maxX - minX + 1;
      int sizeY = maxY - minY + 1;

      List<List<char>> ret = new List<List<char>>();

      for(int i = 0; i < sizeY; i++)
      {
        ret.Add(new List<char>());
        for(int j = 0; j < sizeX; j++)
        {
          ret[i].Add('?');
        }
      }
      
      foreach(var field in Fields)
      {
        int indexX = field.Location.X - minX;
        int indexY = field.Location.Y - minY;
        ret[indexY][indexX] = field.Print();
      }

      {
        int indexX = robotLocation.X - minX;
        int indexY = robotLocation.Y - minY;
        ret[indexY][indexX] = 'R';
      }

      return ret;
    }

  }
}

