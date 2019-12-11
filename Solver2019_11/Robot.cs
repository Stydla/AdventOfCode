using IntCodeCpu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solver2019_11
{
  public class Robot
  {

    private Processor Proc;
    private char CurrentDirection;
    public Point Location { get; } = new Point(0, 0);
    public List<Field> Fields = new List<Field>();

    public Robot(string inputData)
    {
      Proc = new Processor(inputData);
      CurrentDirection = '^';
    }

    private List<char> DIRS = new List<char>() { '^', '>', 'v', '<' };

    public void Run(int initialInput)
    {

      Proc.Input.Add(initialInput);
      while (true)
      {
        Proc.Run();
        if (Proc.Halted)
        {
          long out1 = Proc.Output.GetNext();
          long out2 = Proc.Output.GetNext();

          Field fieldTmp = GetCurrentField();

          switch(out1)
          {
            case 0:
              fieldTmp.Color = eColor.Black;
              break;
            case 1:
              fieldTmp.Color = eColor.White;
              break;
            default:
              throw new Exception($"Invalid out1: {out1}");
          }

          int dirIndex = DIRS.IndexOf(CurrentDirection);
          switch (out2)
          {
            case 0:
              dirIndex--;
              break;
            case 1:
              dirIndex++;
              break;
            default:
              throw new Exception($"Invalid out2: {out2}");
          }
          dirIndex = (dirIndex + 4) % 4;

          CurrentDirection = DIRS[dirIndex];
          Move(1);

          Field f = GetCurrentField();
          int nextInput = f.Color == eColor.Black ? 0 : 1;

          Proc.Input.Add(nextInput);
          
        }
        else
        {
          break;
        }
      }
    }

    internal string GetResultPlane()
    {
      int minX = Fields.Min(x => x.Location.X);
      int maxX = Fields.Max(x => x.Location.X);
      int minY = Fields.Min(x => x.Location.Y);
      int maxY = Fields.Max(x => x.Location.Y);

      int sizeX = maxX - minX + 1;
      int sizeY = maxY - minY;

      StringBuilder sb = new StringBuilder();
      for(int i = sizeY; i >= 0 ; i--)
      {
        for(int j = 0; j < sizeX; j++)
        {

          eColor c = GetColor(new Point(j + minX, i + minY));

          switch(c)
          {
            case eColor.Black:
              sb.Append('░');
              break;
            case eColor.White:
              sb.Append('█');
              break;
          }
        }
        sb.AppendLine();
      }
      return sb.ToString();

    }

    private eColor GetColor(Point point)
    {
      if (Fields.Any(x => x.Location.X == point.X && x.Location.Y == point.Y))
      {
        var fields = Fields.Where(x => x.Location.X == point.X && x.Location.Y == point.Y);
        if (fields.Count() != 1)
        {
          throw new Exception("Field is there multiple times!");
        }
        else
        {
          return fields.First().Color;
        }
      } else
      {
        return eColor.Black;
      }
    }

    private Field GetCurrentField()
    {
      Field fieldTmp;
      if (Fields.Any(x => x.Location.X == Location.X && x.Location.Y == Location.Y))
      {
        var fields = Fields.Where(x => x.Location.X == Location.X && x.Location.Y == Location.Y);
        if (fields.Count() != 1)
        {
          throw new Exception("Field is there multiple times!");
        }
        else
        {
          fieldTmp = fields.First();
        }
      }
      else
      {
        fieldTmp = new Field();
        fieldTmp.Location = new Point(Location);
        fieldTmp.Color = eColor.Black;
        Fields.Add(fieldTmp);
      }
      return fieldTmp;
    }

    private void Move(int count)
    {
      switch(CurrentDirection)
      {
        case '^':
          Location.Y++;
          break;
        case '>':
          Location.X++;
          break;
        case 'v':
          Location.Y--;
          break;
        case '<':
          Location.X--;
          break;
        default:
          throw new Exception($"Invalid direction {CurrentDirection}");

      }
    }

    internal int GetBlackFields()
    {
      return Fields.Count();
    }
  }
}
