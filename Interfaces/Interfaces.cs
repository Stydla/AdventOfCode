using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{

  [InheritedExport(typeof(IAdventSolver))]
  public interface IAdventSolver
  {
    string Filename { get; set; }
    string SolverName { get; }
    int TaskNumber { get; }
    int Year { get; }
    string SolveTask1();
    string SolveTask2();
  }

  public abstract class BaseAdventSolver : IAdventSolver
  {

    public virtual string Filename { get; set; }
    public abstract string SolverName { get; }

    public int TaskNumber { get; }

    public int Year { get; }

    abstract public string SolveTask1();

    abstract public string SolveTask2();

    public BaseAdventSolver(int year, int taskNumber)
    {
      TaskNumber = taskNumber;
      Year = year;
      string path = (new System.Uri(Assembly.GetExecutingAssembly().CodeBase)).AbsolutePath;
      string dir = Path.GetDirectoryName(path);
      path = Path.Combine(dir, "Inputs");
      if (!Directory.Exists(path))
      {
        Directory.CreateDirectory(path);
      }
      Filename = Path.Combine(path, $"input{taskNumber}.txt");
    }
  }
}
