using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solver2019_14
{
  public class Reaction
  {

    public Material PrimaryOutput { get; set; }
    public List<MaterialComponent> Outputs { get; set; } = new List<MaterialComponent>();
    public List<MaterialComponent> Inputs { get; set; } = new List<MaterialComponent>();

    public override string ToString()
    {
      return $"{string.Join(",", Inputs)} => {string.Join(",", Outputs)}";
    }

    public Reaction(Material primaryOutput)
    {
      PrimaryOutput = primaryOutput;
    }

    public void Execute(Nanofactory nf, long multiplier)
    {

      while(!Inputs.All(x=>nf.MaterialStock.IsOnStock(x, multiplier)))
      {
        foreach (var mc in Inputs)
        {
          while (!nf.MaterialStock.IsOnStock(mc, multiplier))
          {
            var stockMaterial = nf.MaterialStock.GetStockItem(mc.Material.Name);
            long needCount = mc.Amount * multiplier - stockMaterial.Count;
            long multiplierTmp;
            if (needCount % mc.Amount == 0)
            {
              multiplierTmp = needCount / mc.Amount;
            }
            else
            {
              multiplierTmp = needCount / mc.Amount + 1;
            }
            nf.CreateMultiple(mc.Material, (int)multiplierTmp);
          }
        }
      }
     

      foreach(var mc in Inputs)
      {
        nf.MaterialStock.Withdraw(mc, multiplier);
      }
      foreach(var mc in Outputs)
      {
        nf.MaterialStock.Deposit(mc, multiplier);
      }
      

    }

    public bool ExecuteReverse(Nanofactory nf, long multiplier)
    {
      if(Outputs.All(x=>nf.MaterialStock.IsOnStock(x,multiplier)))
      //if(nf.MaterialStock.IsOnStock(Output, multiplier))
      {
        foreach(var mc in Outputs)
        {
          nf.MaterialStock.Withdraw(mc, multiplier);
        }
        foreach (var mc in Inputs)
        {
          nf.MaterialStock.Deposit(mc, multiplier);
        }
        return true;
      }
      return false;
    }

    public Reaction CreateOptimalizedReaction(Nanofactory nf, int multiplier)
    {
      MaterialStock before = new MaterialStock(nf.MaterialStock);
      nf.CreateMultiple(this.PrimaryOutput, multiplier);
      nf.Reverse(new List<string>() { PrimaryOutput.Name });
      MaterialStock after = new MaterialStock(nf.MaterialStock);
      nf.Reverse(new List<string>() {});


      Reaction reaction = new Reaction(PrimaryOutput);
      foreach(var ms1 in before.StockItems)
      {
        var ms2 = after.StockItems.Where(x => x.Key == ms1.Key).First();
        long count = ms2.Value.Count - ms1.Value.Count;
        
        if(count < 0)
        {
          MaterialComponent mcTmp = new MaterialComponent(ms1.Value.Material, -count);
          reaction.Inputs.Add(mcTmp);
        }
        if(count > 0)
        {
          MaterialComponent mcTmp = new MaterialComponent(ms1.Value.Material, count);
          reaction.Outputs.Add(mcTmp);
        }
      }
      return reaction;
    }
  }
}
