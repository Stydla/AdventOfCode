using IntCodeCpu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Solver2019_15.Direction;

namespace Solver2019_15
{
  public class Robot
  {
    private Processor Processor { get; }
    public Point Location { get; set; } = new Point(0, 0);
    public Map Map { get; set; } = new Map();

    public Robot(string inputProgram)
    {
      Processor = new Processor(inputProgram);
      Map.Fields.Add(new Field(new Point(0, 0), eFieldType.Empty));
    }

    public int GetOxygenPathCount()
    {
      var oxygen = Map.Fields.Where(x => x.Type == eFieldType.Oxygen).FirstOrDefault();
      if (oxygen == null) throw new Exception("Oxygen not found");

      var startField = Map.GetField(new Point(0, 0));
      MapPath path = Map.FindPath(startField);
      var list = path.GetDirectionList(oxygen);
      return list.Count;
    }

    public MapPath MapPathFromOxygen()
    {
      var oxygen = Map.Fields.Where(x => x.Type == eFieldType.Oxygen).FirstOrDefault();
      if (oxygen == null) throw new Exception("Oxygen not found");

      MapPath path = Map.FindPath(oxygen);
      return path;
    }


    public void SearchMap()
    {
      List<Field> unsearchedFields;
      while ((unsearchedFields = Map.GetUnsearchedFields()).Count > 0)
      {
        foreach(var field in unsearchedFields)
        {
          Field robotField = Map.GetField(Location);
          MapPath path = Map.FindPath(robotField);
          List<eDirection> dirsToField = path.GetDirectionList(field);
          Go(dirsToField);

          List<eDirection> dirs = field.GetMissingNeighboursDirections();
          foreach(var dir in dirs)
          {
            SearchCurrentField(dir); 
          }
        }
      };
    }

    private void SearchCurrentField(eDirection dir)
    {
      Field f = Map.GetField(Location);
      Processor.Input.Add((int)dir);
      Processor.Run();
      int result = (int)Processor.Output.GetNext();
      Point newPoint = Location.Move(dir);
      Field newField;
      switch (result)
      {
        case 0:
          // Hit wall
          newField = new Field(newPoint, eFieldType.Wall);
          Map.Fields.Add(newField);
          break;
        case 1:
          // Moved
          newField = new Field(newPoint, eFieldType.Empty);
          Map.Fields.Add(newField);
          GoBack(dir);
          break;
        case 2:
          // Moved and found oxygen
          newField = new Field(newPoint, eFieldType.Oxygen);
          Map.Fields.Add(newField);
          GoBack(dir);
          break;
        default:
          throw new Exception("$Invalid program output: {result}");
      }
      f.Neighbours.Add(dir, newField);
      if(newField.Type != eFieldType.Wall)
      {
        f.EmptyNeighbours.Add(dir, newField);
        newField.AddNeighbours(Map);
      }
    }




    private void GoBack(eDirection dir)
    {
      Processor.Input.Add((int)Direction.GetOposite(dir));
      Processor.Run();
      Processor.Output.GetNext();
    }

    private void Go(List<eDirection> dirs)
    { 
      foreach(var dir in dirs)
      {
        Location = Location.Move(dir);
        Processor.Input.Add((int)dir);
        Processor.Run();
        Processor.Output.GetNext();
      }
    }

    public void Print()
    {
      var arr = Map.Print(Location);

      StringBuilder sb = new StringBuilder();
      foreach(var l in arr)
      {
        foreach(var c in l)
        {
          sb.Append(c);
        }
        sb.AppendLine();
      }
      System.Diagnostics.Debug.WriteLine(sb.ToString());
    }

  }
}
