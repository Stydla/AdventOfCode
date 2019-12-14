using System.Collections.Generic;

namespace Solver2019_14
{
  public class MaterialComponent
  {
    public long Amount { get; set; }
    public Material Material { get; set; }

    public MaterialComponent(Material material, long amount)
    {
      Material = material;
      Amount = amount;
    }

    public override string ToString()
    {
      return $"{Amount} {Material}";
    }
  }
}