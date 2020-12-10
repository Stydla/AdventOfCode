using Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Web;
using System.Net;
using System.IO;
using System.Net.Http;
using System.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AdventOfCode
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {

    public MainWindow()
    {


      //LoadJSON();

      Data d = new Data();

      foreach (IAdventSolver adventSolver in d.Solvers.OrderBy(x => x.TaskNumber))
      {
        d.Results.Add(new DataItemView() { TaskName = adventSolver.SolverName });
      }

      DataContext = d;

      InitializeComponent();

    }

    private void LoadJSON()
    {
      try
      {
        string html = string.Empty;
        string url = @"https://adventofcode.com/2020/leaderboard/private/view/378346.json";


        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        request.AutomaticDecompression = DecompressionMethods.GZip;

        CookieContainer cookies = new CookieContainer();
        Cookie ck = new Cookie("session", "53616c7465645f5f631592ad09f3c650b4f60248f23957150c2bf3fb0246cf14f647a1f96c2295e6885c24b2d156d41c");
        ck.Domain = ".adventofcode.com";
        cookies.Add(ck);
        request.CookieContainer = cookies;


        using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
        using (Stream stream = response.GetResponseStream())
        using (StreamReader reader = new StreamReader(stream))
        {
          html = reader.ReadToEnd();
        }
       //object obj = JsonConvert.DeserializeObject(html);
        //DataTable dt = Tabulate(html);
        //DataTable dt = (DataTable)JsonConvert.DeserializeObject(html, (typeof(DataTable)));

        string json= "{\"id\":\"10\",\"name\":\"User\",\"add\":false,\"edit\":true,\"authorize\":true,\"view\":true}";
        //DataTable dt = (DataTable)JsonConvert.DeserializeObject(json, (typeof(DataTable)));
        //var oMycustomclassname = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(json);
        var oMycustomclassname = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(html);

        Console.WriteLine(html);
      }
      catch(Exception ex)
      {

      }
    
    }

    public static DataTable Tabulate(string json)
    {
      var jsonLinq = JObject.Parse(json);

      // Find the first array using Linq
      var srcArray = jsonLinq.Descendants().Where(d => d is JArray).First();
      var trgArray = new JArray();
      foreach (JObject row in srcArray.Children<JObject>())
      {
        var cleanRow = new JObject();
        foreach (JProperty column in row.Properties())
        {
          // Only include JValue types
          if (column.Value is JValue)
          {
            cleanRow.Add(column.Name, column.Value);
          }
        }

        trgArray.Add(cleanRow);
      }

      return JsonConvert.DeserializeObject<DataTable>(trgArray.ToString());
    }



    private void SolveAll()
    {
      Data d = (Data)DataContext;
      Task.Factory.StartNew(() =>
      {
        TimeMeasure tm = new TimeMeasure(Dispatcher, d.Solvers.Count() * 4);

        Parallel.ForEach(d.Solvers, adventSolver =>
        {
          SolveTask1(adventSolver, d, tm);
          SolveTask2(adventSolver, d, tm);
          StartTests1(adventSolver, d, tm);
          StartTests2(adventSolver, d, tm);
        });
      });
    }

    private void SolveTask1(IAdventSolver adventSolver, Data d, TimeMeasure tm)
    {
      Task.Factory.StartNew(() =>
      {
        var result = d.Results.Where(x => x.TaskName == adventSolver.SolverName).FirstOrDefault();
        if (result != null)
        {
          try
          {
            StartSolver(adventSolver.SolveTask1, adventSolver.InputData, result.Result1, tm);
          } catch(Exception ex)
          {
            result.Result1.Value = ex.ToString();
          }
        }
      });
    }

    private void SolveTask2(IAdventSolver adventSolver, Data d, TimeMeasure tm)
    {
      Task.Factory.StartNew(() =>
      {
        var result = d.Results.Where(x => x.TaskName == adventSolver.SolverName).FirstOrDefault();
        if (result != null)
        {
          try
          {
            StartSolver(adventSolver.SolveTask2, adventSolver.InputData, result.Result2, tm);
          }
          catch (Exception ex)
          {
            result.Result2.Value = ex.ToString();
          }
        }
      });
    }

    private void StartTests1(IAdventSolver adventSolver, Data d, TimeMeasure tm)
    {
      Task.Factory.StartNew(() =>
      {
        var result = d.Results.Where(x => x.TaskName == adventSolver.SolverName).FirstOrDefault();
        try
        {
          if (result != null)
          {
            tm.Register(result.ResultTest1);
            result.ResultTest1.Value = string.Join("\n", adventSolver.RunTests1());
            tm.Unregister(result.ResultTest1);
            
          }
        }
        catch (NotImplementedException)
        {
          result.ResultTest1.Value = "Task not Implemented";
          tm.Unregister(result.ResultTest1);
        }
        catch (Exception ex)
        {
          result.ResultTest1.Value = ex.ToString();
          tm.Unregister(result.ResultTest1);
        }
        tm.TaskFinished();
      });
    }

    private void StartTests2(IAdventSolver adventSolver, Data d, TimeMeasure tm)
    {
      Task.Factory.StartNew(() =>
      {
        var result = d.Results.Where(x => x.TaskName == adventSolver.SolverName).FirstOrDefault();
        try
        {
          if (result != null)
          {
            tm.Register(result.ResultTest2);
            result.ResultTest2.Value = string.Join("\n", adventSolver.RunTests2());
            tm.Unregister(result.ResultTest2);
          }
        }
        catch (NotImplementedException)
        {
          result.ResultTest2.Value = "Task not Implemented";
          tm.Unregister(result.ResultTest2);
        }
        catch (Exception ex)
        {
          result.ResultTest2.Value = ex.ToString();
          tm.Unregister(result.ResultTest2);
        }
        tm.TaskFinished();
      });
    }

    private void StartSolver(Func<string, string> task, string inputData, ResultView rv, TimeMeasure tm)
    {
      try
      {
        tm.Register(rv);
        rv.Value = "Running";
        rv.Value = task(inputData);
        tm.Unregister(rv);

        if (string.IsNullOrEmpty(rv.Value))
        {
          rv.Value = "No result";
        }
      }
      catch (NotImplementedException)
      {
        rv.Value = "Task not Implemented";
        tm.Unregister(rv);
      }
      catch (Exception ex)
      {
        rv.Value = ex.ToString();
        tm.Unregister(rv);
      }
      tm.TaskFinished();
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
      SolveAll();
    }

    private void Button_Click_1(object sender, RoutedEventArgs e)
    {
      DataItemView div = (sender as Button).DataContext as DataItemView;
      Data d = DataContext as Data;
      IAdventSolver solver = d.Solvers.Where(x => x.SolverName == div.TaskName).FirstOrDefault();
      TimeMeasure tm = new TimeMeasure(Dispatcher, 1);
      SolveTask1(solver, d, tm);

    }

    private void Button_Click_2(object sender, RoutedEventArgs e)
    {
      DataItemView div = (sender as Button).DataContext as DataItemView;
      Data d = DataContext as Data;
      IAdventSolver solver = d.Solvers.Where(x => x.SolverName == div.TaskName).FirstOrDefault();
      TimeMeasure tm = new TimeMeasure(Dispatcher, 1);
      SolveTask2(solver, d, tm);
    }

    private void Button_Click_Test(object sender, RoutedEventArgs e)
    {
      DataItemView div = (sender as Button).DataContext as DataItemView;
      Data d = DataContext as Data;
      IAdventSolver solver = d.Solvers.Where(x => x.SolverName == div.TaskName).FirstOrDefault();
      TimeMeasure tm = new TimeMeasure(Dispatcher, 2);

      StartTests1(solver, d, tm);
      StartTests2(solver, d, tm);
    }
  }
}
