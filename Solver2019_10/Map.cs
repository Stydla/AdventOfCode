using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solver2019_10
{
  public class Map
  {

    public List<List<MapItem>> MapItems = new List<List<MapItem>>();
    public int SizeX { get; }
    public int SizeY { get; }


    public List<MapItem> Asteroids 
    { 
      get
      {
        var tmp = new List<MapItem>();
        foreach(var l in MapItems)
        {
          tmp.AddRange(l.Where(x => x.Type == eMapItemType.Asteroid));
        }
        return tmp;
      } 
    }
    private string InputString { get; set; }
    public Map(string inputData)
    {
      using(StringReader ss = new StringReader(inputData)) 
      {
        string line;
        int x = 0, y = 0;
        while ((line = ss.ReadLine()) != null)
        {
          x = 0;
          var listTmp = new List<MapItem>();
          foreach (var c in line)
          {
            switch(c)
            {
              case '.':
                listTmp.Add(new MapItem(new Point(x, y), eMapItemType.Empty));
                break;
              case '#':
                listTmp.Add(new MapItem(new Point(x, y), eMapItemType.Asteroid));
                break;
              default:
                throw new Exception($"Invalid input character {c}");
            }
            x++;
          }
          MapItems.Add(listTmp);
          y++;
        }
        SizeX = MapItems.FirstOrDefault()?.Count ?? 0;
        SizeY = MapItems.Count;
      }
    }

    private bool IsInsideMap(Point p)
    {
      return (p.X >= 0 && p.X < SizeX &&
        p.Y >= 0 && p.Y < SizeY) ;
    }

    internal void CalculateVisibleAsteroids()
    {
      foreach(var asteroid1 in Asteroids)
      {
        foreach(var asteroid2 in Asteroids)
        {
          if (asteroid1 != asteroid2)
          {
            asteroid1.VisibleAsteroids.Add(asteroid2);
          }
        }
      }

      foreach (var asteroid1 in Asteroids)
      {
        foreach (var asteroid2 in Asteroids)
        {
          if (asteroid1 != asteroid2)
          {

            Vector v = asteroid2.Point - asteroid1.Point;
            v = v.Reduce();
            Point tmp = asteroid2.Point + v;

            while(IsInsideMap(tmp))
            {
              asteroid1.VisibleAsteroids.Remove(MapItems[tmp.Y][tmp.X]);
              tmp += v;
            }
          }
        }
      }
    }

    public MapItem TheBestAsteroid()
    {
      int max = Asteroids.Max(x => x.VisibleAsteroids.Count);

      return Asteroids.Where(x=>x.VisibleAsteroids.Count == max).First();
    }

    public Map(Map m) : this(m.InputString)
    {
    }

  }
}
