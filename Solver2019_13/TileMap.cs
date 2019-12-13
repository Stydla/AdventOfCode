using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntCodeCpu;

namespace Solver2019_13
{
  public class TileMap
  {

    public List<Tile> Tiles { get; set; } = new List<Tile>();
    public int Score { get; set; }

    public Tile Paddle
    {
      get
      {
        return Tiles.Find(x => x.Type == eTileType.HorizontalPaddle);
      }
    }

    public Tile Ball
    {
      get
      {
        return Tiles.Find(x => x.Type == eTileType.Ball);
      }
    }


    public TileMap(List<int> input)
    {
      for(int i = 0; i < input.Count; i+=3)
      {
        if(input[i] == -1 && input[i+1] == 0)
        {
          Score = input[i + 2];
        } else
        {
          Point p = new Point(input[i], input[i + 1]);
          Tile tmp = new Tile(p, input[i + 2]);
          Tiles.Add(tmp);
        }
      }
    }

    public TileMap(Output output)
    {
      while (output.HasNext())
      {
        int x = -1, y = -1, val = -1;
        x = (int)output.GetNext();
        y = (int)output.GetNext();
        val = (int)output.GetNext();
        if (x == -1 && y == 0)
        {
          Score = val;
        }
        else
        {
          Point p = new Point(x, y);
          Tile tmp = new Tile(p, val);
          Tiles.Add(tmp);
        }
      }
    }

    public void Print()
    {
      if (Tiles.Count == 0) return;
      int sizeX = Tiles.Max(x => x.Location.X) + 1;
      int sizeY = Tiles.Max(x => x.Location.Y) + 1;
      for (int i = 0; i < sizeY; i++)
      {
        for(int j = 0; j < sizeX; j++)
        {
          var tile = Tiles.Find(x => x.Location.X == j && x.Location.Y == i);
          if(tile != null)
          {
            System.Diagnostics.Debug.Write(GetTileString(tile.Type));
          }else
          {
            throw new Exception($"Missing Tile {tile.Location}");
          }

        }
        System.Diagnostics.Debug.WriteLine("");
      }
      System.Diagnostics.Debug.WriteLine("");
    }

    internal void Update(Output output)
    {
      while (output.HasNext())
      {
        int x = -1, y = -1, val = -1;
        x = (int)output.GetNext();
        y = (int)output.GetNext();
        val = (int)output.GetNext();
        if (x == -1 && y == 0)
        {
          Score = val;
        }
        else
        {
          Tile tmp = Tiles.Find(tile => tile.Location.X == x && tile.Location.Y == y);
          if(tmp == null)
          {
           throw new Exception($"Tile not found {x},{y}");
          }
          tmp.Type = (eTileType)val;
        }
      }
    }

    private string GetTileString(eTileType type)
    {
      switch (type)
      {
        case eTileType.Empty:
          return " ";
        case eTileType.Wall:
          return "#";
        case eTileType.Block:
          return "X";
        case eTileType.HorizontalPaddle:
          return "_";
        case eTileType.Ball:
          return "O";
        default:
          return "F";
      }
    }
  }
}
