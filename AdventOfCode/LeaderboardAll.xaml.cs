using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AdventOfCode
{
  /// <summary>
  /// Interaction logic for LeaderboardAll.xaml
  /// </summary>
  public partial class LeaderboardAll : Window
  {
    public LeaderboardAll()
    {
      InitializeComponent();

      LeaderboardAllData data = new LeaderboardAllData();

      for(int i = 0; i < data.Events.Count; i++)
      {
        
        LeaderboardControl leaderboardControl = new LeaderboardControl();
        leaderboardControl.DataContext = data.Events[i];


        if (i < 5)
        {
          stackPanel1.Children.Add(leaderboardControl);
        }
        else if (i < 10)
        {
          stackPanel2.Children.Add(leaderboardControl);
        }
        else if (i < 15)
        {
          stackPanel3.Children.Add(leaderboardControl);
        }

      }


    }
  }
}
