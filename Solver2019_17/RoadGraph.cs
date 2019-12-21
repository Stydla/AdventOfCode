using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solver2019_17
{
  public class RoadGraph
  {
    public List<Crossroad> Crossroads = new List<Crossroad>();
    public List<Road> Roads = new List<Road>();

    public Road FindRoad(Point p)
    {
      var road = Roads.Where(x => x.Points.Any(y => y.X == p.X && y.Y == p.Y));
      return road.First();
    }

    public void Print()
    {
      int sizeX = Roads.Max(x => x.Points.Max(y => y.X)) + 1;
      int sizeY = Roads.Max(x => x.Points.Max(y => y.X)) + 1;

      for(int i = 0; i < sizeY; i++)
      {
        for(int j = 0; j < sizeX; j++)
        {
          var cr = Crossroads.Where(x => x.Location.X == j && x.Location.Y == i).FirstOrDefault();
          if(cr != null)
          {
            System.Diagnostics.Debug.Write($"{"c" + cr.ID,4}");
          } else
          {
            var road = Roads.Where(x => x.Points.Any(y => y.X == j && y.Y == i)).FirstOrDefault();
            if (road != null)
            {
              System.Diagnostics.Debug.Write($"{"r" +road.ID,4}");
            } else
            {
              System.Diagnostics.Debug.Write($"{"",4}");
            }
          }
          
        }
        System.Diagnostics.Debug.WriteLine("");
      }

    }

    public Crossroad GetCrossroad(Point pTmp)
    {
      return Crossroads.Where(x => x.Location.X == pTmp.X && x.Location.Y == pTmp.Y).FirstOrDefault();
    }
  }
}
