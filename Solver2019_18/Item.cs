using System;

namespace Solver2019_18
{
  public abstract class Item
  {
    public char Name { get; }
    public Point Point { get; }

    public Item (char name, Point point)
    {
      Name = name;
      Point = point;
    }

    public string Print()
    {
      return $"{Name}";
    }
  }
}