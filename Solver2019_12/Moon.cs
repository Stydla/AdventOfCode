using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solver2019_12
{
  public class Moon
  {

    public Point Location { get; set; }
    public Vector Velocity { get; set; }
    public Vector Gravity { get; set; }

    public Moon(Point p)
    {
      Location = new Point(p);
      Velocity = new Vector(0, 0, 0);
      Gravity = new Vector(0, 0, 0);
    }

    public void Move()
    {
      Location = Location + Velocity;
    }

    internal void CalculateGravity(List<Moon> moons)
    {
      Gravity = new Vector(0, 0, 0);
      foreach(Moon m in moons)
      {
        if (m == this) continue;

        Vector v =  m.Location - Location;
        Vector oneVector = v / v.Abs();
        Gravity = Gravity + oneVector;

      }
    }

    internal void AplyGravity()
    {
      Velocity = Velocity + Gravity;
    }

    public int Energy 
    {
      get
      {
        return Velocity.Energy * Location.Energy;
      }
    }

    public override string ToString()
    {
      return $"loc=({Location}), vel=({Velocity}), grav=({Gravity})";
    }
  }
}
