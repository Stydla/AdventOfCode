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

    string InputData { get; }
    List<TestData> TestDataList1 { get; }
    List<TestData> TestDataList2 { get; }
    string SolverName { get; }
    int TaskNumber { get; }
    int Year { get; }
    string SolveTask1(string InputData);
    string SolveTask2(string InputData);
    List<string> RunTests1();
    List<string> RunTests2();
  }

  public abstract class BaseAdventSolver : IAdventSolver
  {

    private string Filename { get; set; }
    public abstract string SolverName { get; }

    public int TaskNumber { get; }

    public int Year { get; }

    public string InputData
    {
      get
      {
        if (!File.Exists(Filename))
        {
          File.WriteAllText(Filename, "");
        }
        return File.ReadAllText(Filename);
      }
    }

    public List<TestData> TestDataList1
    {
      get
      {
        List<TestData> ret = new List<TestData>();
        string path = (new System.Uri(Assembly.GetExecutingAssembly().CodeBase)).AbsolutePath;
        string dir = Path.GetDirectoryName(path);
        path = Path.Combine(dir, "Inputs", $"Test{TaskNumber}");
        if (!Directory.Exists(path))
        {
          Directory.CreateDirectory(path);
        }
        foreach(var file in Directory.GetFiles(path))
        {
          string tmpPath = Path.GetDirectoryName(file);
          string filename = Path.GetFileNameWithoutExtension(file);
          if (filename.StartsWith("out"))
          {

            int fileNumber = int.Parse(filename.Substring(3));
            string input = File.ReadAllText(Path.Combine(tmpPath, $"in{fileNumber}.txt"));
            string output = File.ReadAllText(Path.Combine(tmpPath, $"out{fileNumber}.txt"));
            ret.Add(new TestData(fileNumber, input, output));
          }
        }
        return ret;
      }
    }

    public List<TestData> TestDataList2
    {
      get
      {
        List<TestData> ret = new List<TestData>();
        string path = (new System.Uri(Assembly.GetExecutingAssembly().CodeBase)).AbsolutePath;
        string dir = Path.GetDirectoryName(path);
        path = Path.Combine(dir, "Inputs", $"Test{TaskNumber}");
        if (!Directory.Exists(path))
        {
          Directory.CreateDirectory(path);
        }
        foreach (var file in Directory.GetFiles(path))
        {
          string tmpPath = Path.GetDirectoryName(file);
          string filename = Path.GetFileNameWithoutExtension(file);
          if (filename.StartsWith("sec"))
          {

            int fileNumber = int.Parse(filename.Substring(3));
            string input = File.ReadAllText(Path.Combine(tmpPath, $"in{fileNumber}.txt"));
            string output = File.ReadAllText(Path.Combine(tmpPath, $"sec{fileNumber}.txt"));
            ret.Add(new TestData(fileNumber, input, output));
          }
        }
        return ret;
      }
    }

    abstract public string SolveTask1(string InputData);

    abstract public string SolveTask2(string InputData);

    public List<string> RunTests1()
    {
      List<string> tmp = new List<string>();
      if(TestDataList1.Count == 0)
      {
        tmp.Add("No Test Data");
        return tmp;
      }
      foreach (TestData td in TestDataList1)
      {
        string result = SolveTask1(td.Input);
        if (td.Output == result)
        {
          tmp.Add($"{td.TestNumber} - OK");
        }
        else
        {
          tmp.Add($"{td.TestNumber} - FAIL: {td.Output} != {result}");
        }
      }
      return tmp;
    }

    public List<string> RunTests2()
    {
      List<string> tmp = new List<string>();
      if (TestDataList2.Count == 0)
      {
        tmp.Add("No Test Data");
        return tmp;
      }
      foreach (TestData td in TestDataList2)
      {
        string result = SolveTask2(td.Input);
        if (td.Output == result)
        {
          tmp.Add($"{td.TestNumber} - OK");
        }
        else
        {
          tmp.Add($"{td.TestNumber}({td.Output}) - FAIL: {result}");
        }
      }
      return tmp;
    }
    
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

  public class TestData
  {
    public TestData(int testNumber, string input, string output)
    {
      Input = input;
      Output = output;
      TestNumber = testNumber;
    }

    public int TestNumber { get; }
    public string Input { get; }
    public string Output { get; }
  }
}
