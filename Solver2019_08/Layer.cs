using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solver2019_08
{
  public class Layer
  {
    public List<List<int>> Pixels { get; }
    public int Width { get; }
    public int Height { get; }
    public int Index { get; }

    public int NumberCount(int number)
    {
      int cnt = 0;
      foreach(var row in Pixels)
      {
        cnt += row.Where(x => x == number).Count();
      }
      return cnt;
    }

    public Layer(int width, int height)
    {
      Width = width;
      Height = height;
      Pixels = new List<List<int>>();
      for(int i = 0; i < height; i++)
      {
        Pixels.Add(new List<int>(new int[width]));
      }
      Index = -1;
    }

    public Layer(string data, int width, int height, int index)
    {
      Width = width;
      Height = height;
      Index = index;
      Pixels = new List<List<int>>();

      for (int i = 0; i < height; i++)
      {
        Pixels.Add(new List<int>());
        for (int j = 0; j < width; j++)
        {
          Pixels[i].Add(data[i * width + j] - '0');
        }
      }
    }

    public string GetString()
    {
      StringBuilder sb = new StringBuilder();
      for (int i = 0; i < Height; i++)
      {
        if(i != 0) sb.Append("\n");
        for (int j = 0; j < Width; j++)
        {

          sb.Append(Pixels[i][j]);
        }
      }
      return sb.ToString();
    }

  }
}
