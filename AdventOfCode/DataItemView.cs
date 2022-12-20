using System.ComponentModel;
using System.Windows.Media;

namespace AdventOfCode
{
  public class DataItemView : INotifyPropertyChanged
  {

    public event PropertyChangedEventHandler PropertyChanged;

    private string _TaskName;
    public string TaskName
    {
      get
      {
        return _TaskName;
      }
      set
      {
        _TaskName = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TaskName)));
      }
    }



    private ResultView _Result1 = new ResultView();
    public ResultView Result1
    {
      get
      {
        return _Result1;
      }
      set
      {
        _Result1 = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Result1)));
      }
    }

    private ResultView _Result2 = new ResultView();
    public ResultView Result2
    {
      get
      {
        return _Result2;
      }
      set
      {
        _Result2 = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Result2)));
      }
    }

    private ResultView _ResultTest1 = new ResultView();
    public ResultView ResultTest1
    {
      get
      {
        return _ResultTest1;
      }
      set
      {
        _ResultTest1 = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ResultTest1)));
      }
    }

    private ResultView _ResultTest2 = new ResultView();
    public ResultView ResultTest2
    {
      get
      {
        return _ResultTest2;
      }
      set
      {
        _ResultTest2 = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ResultTest2)));
      }
    }
  }

  public class ResultView : INotifyPropertyChanged
  {
    public event PropertyChangedEventHandler PropertyChanged;

    private string _Value = "Not Solved Yet";
    public string Value
    {
      get
      {
        return _Value;
      }
      set
      {
        _Value = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Value)));
      }
    }

    private string _Time;

    public string Time
    {
      get
      {
        return _Time;
      }
      set
      {
        _Time = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Time)));
      }
    }

    private ERunningState _RunningState = ERunningState.NotStarted;
    public ERunningState RunningState
    {
      get
      {
        return _RunningState;
      }
      set
      {
        _RunningState = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(RunningState)));
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(RunningColor)));
      }
    }
   
    public Brush RunningColor
    {
      get
      {
        switch (RunningState)
        {
          case ERunningState.NotStarted:
            return Brushes.White;
          case ERunningState.Running:
            return Brushes.Yellow;
          case ERunningState.Finished:
            return Brushes.LightGreen;
          case ERunningState.Exception:
            return Brushes.OrangeRed;
          case ERunningState.NotImplemented:
            return Brushes.Cyan;
          case ERunningState.Incorrect:
            return Brushes.PaleVioletRed;
          case ERunningState.NoTestData:
            return Brushes.Transparent;
        }
        return Brushes.Gray;
      }
    }
  }

  public enum ERunningState
  {
    NotStarted,
    Running,
    Finished,
    Exception,
    NotImplemented,
    Incorrect,
    NoTestData
  }
}