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

namespace AdventOfCode
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {

    public MainWindow()
    {

      Data d = new Data();

      foreach (IAdventSolver adventSolver in d.Solvers)
      {
        d.Results.Add(new DataItemView() { TaskName = adventSolver.SolverName });
      }

      DataContext = d;

      InitializeComponent();

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
