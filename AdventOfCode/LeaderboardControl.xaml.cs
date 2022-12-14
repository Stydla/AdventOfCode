using LeaderboardLib.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AdventOfCode
{
  /// <summary>
  /// Interaction logic for Leaderboard.xaml
  /// </summary>
  public partial class LeaderboardControl : UserControl
  {
    public LeaderboardControl()
     {
      InitializeComponent();
    }

    private void UserControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
      WriteData();
    }

    private void WriteData()
    {
      rtb.Document.Blocks.Clear();

      if(DataContext is Event ev)
      {
        FlowDocument doc = rtb.Document;

        

        PlainData pd = ev.GetPlainData();
        TextRange tr;

        for (int i = 0; i < pd.Grid.Count; i++)
        {
          for(int j = 0; j < pd.Grid[i].Count; j++)
          {
            Character character = pd.Grid[i][j];

            tr = new TextRange(rtb.Document.ContentEnd, rtb.Document.ContentEnd);
            tr.Text = character.Value.ToString();
            Brush brush = null;
            switch(character.Type)
            {
              case ECharacterType.Star_0:
                brush = (SolidColorBrush)new BrushConverter().ConvertFrom("#FF333333");
                break;
              case ECharacterType.Star_1:
                brush = (SolidColorBrush)new BrushConverter().ConvertFrom("#FF9999CC");
                break;
              case ECharacterType.Star_2:
                brush = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFFFFF66");
                break;
              case ECharacterType.ColumnHeader:
                brush = (SolidColorBrush)new BrushConverter().ConvertFrom("#FF056A0C");
                break;
              case ECharacterType.RowHeader:
              case ECharacterType.MemberName:
                brush = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFCCCCCC");
                break;
            }
            tr.ApplyPropertyValue(TextElement.ForegroundProperty, brush);
          }
          tr = new TextRange(rtb.Document.ContentEnd, rtb.Document.ContentEnd);
          tr.Text = Environment.NewLine;
          tr.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.Black);
        }
        
      }

      
    }
  }
}
