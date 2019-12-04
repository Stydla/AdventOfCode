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

      foreach (IAdventSolver adventSolver in  d.Solvers)
      {
        d.Results.Add(new DataItemView() { TaskName = adventSolver.SolverName});
      }

      DataContext = d;

      InitializeComponent();

    }

    private void SolveAll()
    {
      Data d = (Data)DataContext;
      Task.Factory.StartNew(() =>
      {
        TimeMeasure tm = new TimeMeasure(Dispatcher, d.Solvers.Count() * 2);
        
        Parallel.ForEach(d.Solvers, adventSolver =>
        {
          SolveTask1(adventSolver, d, tm);
          SolveTask2(adventSolver, d, tm);
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
          StartSolver(adventSolver.SolveTask1, result.Result1, tm);
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
          StartSolver(adventSolver.SolveTask2, result.Result2, tm);
        }
      });
    }

    private void StartSolver(Func<string> task, ResultView rv, TimeMeasure tm)
    {
      try
      {
        tm.Register(rv);
        rv.Value = "Running";
        rv.Value = task();
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
  }
}
