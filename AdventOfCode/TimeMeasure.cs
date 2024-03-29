﻿using System.Collections.Generic;
using System.Diagnostics;
using System.Timers;
using System.Windows.Threading;

namespace AdventOfCode
{
  public class TimeMeasure
  {
    private Dictionary<ResultView, long> ResultViewList = new Dictionary<ResultView, long>();
    private Timer t = new Timer(30);
    private Stopwatch sw = new Stopwatch();
    private Dispatcher dispatcher;
    private int TaskCount { get; set; }


    public TimeMeasure(Dispatcher dispatcher, int taskCount)
    {
      this.dispatcher = dispatcher;
      dispatcher.Invoke(() =>
      {
        TaskCount = taskCount;
        if (TaskCount == 0) return;
        sw.Reset();
        sw.Start();
        t.Elapsed += T_Elapsed;
        t.Start();
      });
    }


    public void TaskFinished()
    {
      dispatcher.Invoke(() =>
      {
        TaskCount--;
        if (TaskCount == 0)
        {
          Stop();
        }
      });
    }

    private void T_Elapsed(object sender, ElapsedEventArgs e)
    {
      try
      {
        dispatcher.Invoke(() =>
        {
          foreach (var item in ResultViewList)
          {
            item.Key.Time = $"{(sw.ElapsedMilliseconds - item.Value) / 1000.0}s";
            //item.Key.Time = $"{sw.ElapsedTicks}s";
          }
        });
      } catch { }
    }

    public void Stop()
    {
      dispatcher.Invoke(() =>
      {
        t.Stop();
        sw.Stop();
      });
    }

    internal void Register(ResultView rv)
    {
      dispatcher.Invoke(() =>
      {
        ResultViewList.Add(rv, sw.ElapsedMilliseconds);
        rv.Time = $"0s";
      });
    }

    internal void Unregister(ResultView rv)
    {
      dispatcher.Invoke(() =>
      {
        ResultViewList.Remove(rv);
      });
    }
  }
}
