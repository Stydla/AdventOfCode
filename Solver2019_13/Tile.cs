using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solver2019_13
{
  public class Tile
  {
    public Point Location { get; set;}
    public eTileType Type { get; set; }

    public Tile(Point location, int type)
    {
      Location = location;
      Type = (eTileType)type;
    }

  }

  public enum eTileType
  {
    Empty,
    Wall,
    Block,
    HorizontalPaddle,
    Ball,
  }
}
