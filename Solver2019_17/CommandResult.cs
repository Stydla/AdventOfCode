using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Solver2019_17.Direction;

namespace Solver2019_17
{
  public class CommandResult
  {
    public List<int> Command { get; set; }
    public Point NextPoint { get; set; }
    public eDirection NextDirection { get; set; }
  }
}
