using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solver2019_14
{
  public class MaterialStock
  {
    public Dictionary<string, StockItem> StockItems { get; set; } = new Dictionary<string, StockItem>();

    public MaterialStock() { }
    public MaterialStock(MaterialStock stock)
    {
      foreach (var kv in stock.StockItems)
      {
        StockItems.Add(kv.Key, new StockItem(kv.Value.Material.Name) { Count = kv.Value.Count });
      }
    }

    public StockItem GetStockItem(string name)
    {
      if (StockItems.ContainsKey(name))
      {
        return StockItems[name];
      }
      else
      {
        StockItem tmp = new StockItem(name);
        StockItems.Add(name, tmp);
        return tmp;
      }
    }

    public bool IsOnStock(MaterialComponent mc, long multiplier)
    {
      return StockItems[mc.Material.Name].Count >= mc.Amount * multiplier;
    }

    internal void Deposit(MaterialComponent component, long multiplier)
    {
      GetStockItem(component.Material.Name).Count += (component.Amount * multiplier);
    }

    internal void Withdraw(MaterialComponent component, long multiplier)
    { 
      var si = GetStockItem(component.Material.Name);
      if(si.Count < component.Amount * multiplier)
      {
        throw new OutOfMaterialException($"stock: {si}, request: {component}");
      }
      GetStockItem(component.Material.Name).Count -= (component.Amount * multiplier);
    }

    internal void Modify(MaterialComponent component)
    {
      var si = GetStockItem(component.Material.Name);
      if (si.Count + component.Amount < 0)
      {
        throw new OutOfMaterialException($"stock: {si}, request: {component}");
      }
      GetStockItem(component.Material.Name).Count += component.Amount;
    }
  }


}
