using IntCodeCpu;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solver2019_13
{
  public class Program : BaseAdventSolver
  {
    public Program() : base(2019, 13)
    {
    }

    public override string SolverName => "Day 13: Care Package";

    public override string SolveTask1(string InputData)
    {
      Processor p = new Processor(InputData);
      p.Run();

      List<int> output = new List<int>();
      while(p.Output.HasNext())
      {
        output.Add((int)p.Output.GetNext());
      }

      TileMap tileMap = new TileMap(output);
      //tileMap.Print();
      return tileMap.Tiles.Where(x=>x.Type == eTileType.Block).Count().ToString();


    }

    public override string SolveTask2(string InputData)
    {
      Processor p = new Processor(InputData);
      p.Run();

      List<int> output = new List<int>();
      while (p.Output.HasNext())
      {
        output.Add((int)p.Output.GetNext());
      }

      TileMap tileMap = new TileMap(output);


      Processor gameProcessor = new Processor(InputData);
      gameProcessor.ChangeProgramData(0, 2);


      while(true)
      {
        gameProcessor.Run();
        if(gameProcessor.Halted)
        {
          tileMap.Update(gameProcessor.Output);
          //tileMap.Print();

          int diff = tileMap.Paddle.Location.X - tileMap.Ball.Location.X;
          int dir;

          if(diff < 0)
          {
            dir = 1;
          } else if (diff > 0)
          {
            dir = -1;
          } else
          {
            dir = 0;
          }

          gameProcessor.Input.Add(dir);

        } else
        {
          tileMap.Update(gameProcessor.Output);
          break;
        }
      }

      return tileMap.Score.ToString();
    }
  }
}
