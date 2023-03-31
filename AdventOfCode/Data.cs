using Interfaces;
using LeaderboardLib;
using LeaderboardLib.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows;

namespace AdventOfCode
{
  public class Data
  {

    [ImportMany]
    public IEnumerable<IAdventSolver> Solvers;

    public List<DataItemView> Results { get; set; } = new List<DataItemView>();

    public Event Event { get; set; }

    public Data()
    {
      // An aggregate catalog that combines multiple catalogs.
      var catalog = new AggregateCatalog();
      string path = (new System.Uri(Assembly.GetExecutingAssembly().CodeBase)).AbsolutePath;
      string dir = Path.GetDirectoryName(path);
      path = Path.Combine(dir, "Solvers");
      if (!Directory.Exists(path))
      {
        Directory.CreateDirectory(path);
      }

      // Adds all the parts found in the same assembly as the Program class.
      catalog.Catalogs.Add(new DirectoryCatalog(path));

      // Create the CompositionContainer with the parts in the catalog.
      CompositionContainer container = new CompositionContainer(catalog);

      // Fill the imports of this object.
      try
      {
        container.ComposeParts(this);
      }
      catch (CompositionException compositionException)
      {
        Console.WriteLine(compositionException.ToString());
      }


      Match m = Regex.Match(path, ".*\\\\AdventOfCode_(\\d*)\\\\.*");
      string year = m.Groups[1].Value;
      string session = File.ReadAllText("session.txt");
      if(!string.IsNullOrEmpty(session))
      {
        try
        {
          Event = Leaderboard.LoadEvent(year, session);
        }
        catch
        {
          var dialog = new MyDialog();
          //dialog.Width = 500;
          //dialog.Height = 100;
          if (dialog.ShowDialog() == true)
          {
            string result = dialog.ResponseText;
            if(!result.StartsWith("session="))
            {
              result = "session=" + result;
            }
            File.WriteAllText("session.txt", result);
          }
          try
          {
            session = File.ReadAllText("session.txt");
            Event = Leaderboard.LoadEvent(year, session);
          }
          catch (Exception)
          {
          }

        }
        
      }
    }
  }

  
}
