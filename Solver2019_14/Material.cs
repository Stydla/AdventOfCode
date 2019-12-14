using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solver2019_14
{
  public class Material
  {
    public string Name { get; set; }

    public Material(string name)
    {
      Name = name;    
    }

    public override string ToString()
    {
      return Name;
    }
  }
}
