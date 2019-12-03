﻿using Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
  public class Data
  {

    [ImportMany]
    public IEnumerable<IAdventSolver> Solvers;

    public List<DataItemView> Results { get; set; } = new List<DataItemView>();


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
    }
  }
}
