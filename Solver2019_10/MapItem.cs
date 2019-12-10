using System;
using System.Collections.Generic;
using System.Linq;

namespace Solver2019_10
{
  public class MapItem
  {
    public eMapItemType Type { get; set; }
    public Point Point { get; set; }

    private double CurrentAngle { get; set; } = 0;

    public List<MapItem> VisibleAsteroids { get; } = new List<MapItem>();

    public MapItem(Point point, eMapItemType type)
    {
      Type = type;
      Point = point;
    }

    public override string ToString()
    {
      return Point.ToString();
    }

    private AsteroidPosition AsteroidWithDistance(MapItem asteroid)
    {
      AsteroidPosition ap = new AsteroidPosition();
      ap.Distance = (this.Point - asteroid.Point).ManhatanDistance();
      ap.Angle = Vector.Angle(new Vector(0, -1), asteroid.Point - this.Point);
      ap.MapItem = asteroid;
      return ap;
    }

    private List<AsteroidPosition> AsteroidsWithDistance(List<MapItem> asteroids)
    {
      List<AsteroidPosition> ret = new List<AsteroidPosition>();

      foreach(var asteroid in asteroids)
      {
        ret.Add(AsteroidWithDistance(asteroid));
      }

      return ret;
    }

    public MapItem KillAndMurder(List<MapItem> asteroids, int killCount)
    {
      asteroids.Remove(this); // prece nezabiju sebe

      AsteroidPosition mi = null;
      for (int i = 0; i < killCount -1; i++)
      {

        //Print(asteroids, mi);
        mi = WhoIsNext(asteroids);
        Kill(asteroids, mi);
      }
      return WhoIsNext(asteroids).MapItem;
    }

    private AsteroidPosition WhoIsNext(List<MapItem> asteroids)
    {
      List<AsteroidPosition> awd = AsteroidsWithDistance(asteroids);

      List<AsteroidPosition> items = awd.Where(x => x.Angle >= CurrentAngle).ToList();
      if(items.Count() == 0)
      {
        return awd.OrderBy(x => x.Angle).ThenBy(x => x.Distance).First();
      } else
      {
        var tmp = items.GroupBy(x => x.Angle).OrderBy(x=>x.Key);
        return tmp.First().OrderBy(x => x.Distance).First();
      }

    }

    private void Kill(List<MapItem> asteroids, AsteroidPosition ap)
    {
      asteroids.Remove(ap.MapItem);
      CurrentAngle = ap.Angle + 0.000000001;
    }


    private class AsteroidPosition
    {
      public double Distance { get; set; }
      public double Angle { get; set; }
      public MapItem MapItem { get; set; }
    }

    private void Print(List<MapItem> items, AsteroidPosition lastKilled) 
    {
      for(int i = 0; i < 24; i++)
      {
        for(int j = 0; j < 24; j++)
        {
          if (lastKilled!= null && lastKilled.MapItem.Point.X == j && lastKilled.MapItem.Point.Y == i)
          {
            System.Diagnostics.Debug.Write("X");
            continue;
          }
          if(this.Point.X == j && this.Point.Y == i)
          {
            System.Diagnostics.Debug.Write("O");
            continue;
          }
          var item = items.Find(x => x.Point.X == j && x.Point.Y == i);
          if(item == null)
          {
            System.Diagnostics.Debug.Write(".");
          } else
          {
            System.Diagnostics.Debug.Write("8");
          }
        }
        System.Diagnostics.Debug.WriteLine("");
      }
      System.Diagnostics.Debug.WriteLine("");
    }

  }
}