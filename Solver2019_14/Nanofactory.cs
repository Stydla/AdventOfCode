using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solver2019_14
{
  public class Nanofactory
  {



    public Dictionary<Material, Dictionary<long, Reaction>> Reactions { get; set; } = new Dictionary<Material, Dictionary<long, Reaction>>();

    public MaterialStock MaterialStock { get; set; } = new MaterialStock();


    public Nanofactory(string input)
    {

      using (StringReader sr = new StringReader(input))
      {
        string line;
        while ((line = sr.ReadLine()) != null)
        {
          string[] inAndOut = line.Split(new string[] { " => " }, StringSplitOptions.None);
          string[] inputs = inAndOut[0].Split(new string[] { ", " }, StringSplitOptions.None);
          string output = inAndOut[1];

          string[] materialComponentStr = output.Split(' ');
          long amount = int.Parse(materialComponentStr[0]);
          string nameStr = materialComponentStr[1];
          StockItem stockItem = MaterialStock.GetStockItem(nameStr);
          MaterialComponent outputComponent = new MaterialComponent(stockItem.Material, amount);

          Reaction reaction = new Reaction(stockItem.Material);
          reaction.Outputs.Add(outputComponent);

          foreach (string matInput in inputs)
          {
            materialComponentStr = matInput.Split(' ');
            amount = int.Parse(materialComponentStr[0]);
            nameStr = materialComponentStr[1];

            stockItem = MaterialStock.GetStockItem(nameStr);
            MaterialComponent inputComponent = new MaterialComponent(stockItem.Material, amount);

            reaction.Inputs.Add(inputComponent);
          }
          if(!Reactions.ContainsKey(reaction.PrimaryOutput))
          {
            Reactions.Add(reaction.PrimaryOutput, new Dictionary<long, Reaction>());
          }
          Reactions[reaction.PrimaryOutput].Add(1, reaction);
          
        }
      }
    }

    public void OptimalizeReaction(string name, int multiplier)
    {
      if(!Reactions.Any(x=>x.Key.Name == name))
      {
        throw new Exception($"Reaction {name} not exists");
      }
      try
      {
        var si = MaterialStock.GetStockItem(name);
        Reaction original = Reactions[si.Material].First().Value;
        Reaction optimalizedReaction = original.CreateOptimalizedReaction(this, multiplier);
        Reactions[si.Material].Add(multiplier, optimalizedReaction);
      }catch(OutOfMaterialException)
      {

      }
      
    }

    internal bool Reverse(List<string> exceptList)
    {
      bool ret = false;
      bool changed = true;
      while(changed)
      {
        changed = false;
        foreach(var react in Reactions)
        {
          if (exceptList.Contains(react.Key.Name)) continue;

          StockItem si = MaterialStock.GetStockItem(react.Key.Name);
          long multiplier = si.Count / react.Value.First().Value.Outputs[0].Amount;
          if(multiplier > 0)
          {
            react.Value.First().Value.ExecuteReverse(this, multiplier);
            changed = true;
            ret = true;
          }
        }
      }
      return ret;
    }

    public void Create(string material, long count)
    {
      if (!Reactions.ContainsKey(MaterialStock.GetStockItem(material).Material))
      {
        throw new OutOfMaterialException($"Not Existing reaction for material:{material}");
      }
      else
      {
        while(count > 0)
        {
          var reactions = Reactions[MaterialStock.GetStockItem(material).Material];
          var subList = reactions.Where(x => x.Key <= count);
          long max = subList.Max(x => x.Key);
          var reaction = subList.Where(x => x.Key == max).First().Value;
          reaction.Execute(this, 1);
          count -= max;
        }
       
      }
    }

    //public void Create(Material material, long multiplier)
    //{
      
    //  if(!Reactions.ContainsKey(material))
    //  {
    //    throw new OutOfMaterialException($"Not Existing reaction for material: {material.Name}");
    //  } else
    //  {
    //    //var reaction = Reactions[material];
    //    //reaction.Execute(this, multiplier)
    //    var reactions = Reactions[material];
    //    var subList = reactions.Where(x => x.Key <= multiplier);
    //    long max = subList.Max(x => x.Key);
    //    var reaction = subList.Where(x => x.Key == max).First().Value;
    //    reaction.Execute(this, multiplier);
    //  }
    //}

    public void CreateMultiple(Material material, int multiplier)
    {
      if (!Reactions.ContainsKey(material))
      {
        throw new OutOfMaterialException($"Not Existing reaction for material: {material.Name}");
      }
      else
      {
        //var reaction = Reactions[material];
        //reaction.Execute(this, multiplier);
        var reactions = Reactions[material];
        var subList = reactions.Where(x => x.Key <= multiplier);
        long max = subList.Max(x => x.Key);
        var reaction = subList.Where(x => x.Key == max).First().Value;

        int newMultiplier = multiplier / (int)reaction.Outputs.Where(x => x.Material.Name == reaction.PrimaryOutput.Name).First().Amount;
        newMultiplier = newMultiplier == 0 ? 1 : newMultiplier;
        reaction.Execute(this, newMultiplier);
      }
    }

  }

  public class OutOfMaterialException : Exception
  {
    public OutOfMaterialException(string message) : base(message)
    {
    }
  }

}