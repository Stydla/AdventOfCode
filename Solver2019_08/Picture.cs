using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solver2019_08
{
  public class Picture
  {

    public List<Layer> Layers { get; }
    public int Width {get;}
    public int Height { get; }

    public Picture(string data, int width, int height)
    {
      Width = width;
      Height = height;
      Layers = new List<Layer>();

      using (StringReader sr = new StringReader(data))
      {
        int layerSize = Width * Height;
        char[] buffer = new char[layerSize];
        for (int i = 0; i < layerSize; i++)
        {
          sr.Read(buffer, 0, layerSize);
          Layers.Add(new Layer(new string(buffer), Width, Height, i));
        }
      }
    }

    public int Task1()
    {
      Layer l =  Helpers.MinBy(Layers, x => x.NumberCount(0));
      return l.NumberCount(1) * l.NumberCount(2);
    }

    public string Task2()
    {
      Layer l = CreateRenderLayer();
      var message = l.GetString();
      message = message.Replace("0", "░").Replace("1", "█");
      return message;
    }

    private Layer CreateRenderLayer()
    {
      Layer l = new Layer(Width, Height);
      for(int i = 0; i < Height; i++)
      {
        for(int j = 0; j < Width; j++)
        {
          l.Pixels[i][j] = GetPixelColor(j, i);
        }
      }
      return l;
    }

    private int GetPixelColor(int x, int y)
    {
      for(int i = 0; i < Layers.Count; i++)
      {
        int layerPixel = Layers[i].Pixels[y][x];
        if (layerPixel != 2)
        {
          return layerPixel;
        } 
      }
      return 2;
    }

  }
}

