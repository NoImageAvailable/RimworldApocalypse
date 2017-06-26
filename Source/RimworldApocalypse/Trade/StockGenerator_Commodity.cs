using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using Verse;
using UnityEngine;

namespace RimworldApocalypse.Trade
{
    public abstract class StockGenerator_Commodity : StockGenerator
    {
        private IntRange commodityDefRange = IntRange.one;

        protected abstract IEnumerable<ThingDef> AllCommodityThingDefs();

        public abstract PriceType GetPriceForTile(int tile);

        public override bool HandlesThingDef(ThingDef thingDef)
        {
            return AllCommodityThingDefs().Contains(thingDef);
        }

        public override IEnumerable<Thing> GenerateThings(int forTile)
        {
            List<ThingDef> generatedDefs = new List<ThingDef>();
            int numThingDefsToUse = commodityDefRange.RandomInRange;
            for (int i = 0; i < numThingDefsToUse; i++)
            {
                ThingDef chosenThingDef;
                if (!(from t in AllCommodityThingDefs()
                      where t.tradeability == Tradeability.Stockable && t.techLevel <= maxTechLevelGenerate && !generatedDefs.Contains(t)
                      select t).TryRandomElement(out chosenThingDef))
                {
                    break;
                }
                foreach (Thing th in StockGeneratorUtility.TryMakeForStock(chosenThingDef, base.RandomCountOf(chosenThingDef)))
                {
                    yield return th;
                }
                generatedDefs.Add(chosenThingDef);
            }
        }
    }
}
