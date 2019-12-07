using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solver2019_06
{
  public class InputItem
  {
    public string ObjCom { get; set; }
    public string ObjOrbit { get; set; }

    public InputItem(string input)
    {
      string [] inputs = input.Split(')');
      ObjCom = inputs[0];
      ObjOrbit = inputs[1];

    }
  }
}
