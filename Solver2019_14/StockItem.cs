using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solver2019_14
{
  public class StockItem
  {
    public long Count { get; set; }
    public Material Material { get; set; }

    public StockItem(string name)
    {
      Material = new Material(name);
      Count = 0;
    }

    public override string ToString()
    {
      return $"{Material}: {Count}";
    }

  }
}
