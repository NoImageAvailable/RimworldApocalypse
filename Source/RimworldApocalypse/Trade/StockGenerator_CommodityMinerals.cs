using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using Verse;
using UnityEngine;

namespace RimworldApocalypse.Trade
{
    public class StockGenerator_CommodityMinerals : StockGenerator_Commodity
    {
        protected override IEnumerable<ThingDef> AllCommodityThingDefs()
        {
            var defList = new List<ThingDef>() { ThingDefOf.Steel, ThingDefOf.Plasteel, ThingDefOf.Gold, RAThingDefOf.Uranium, RAThingDefOf.Jade };
            defList.AddRange(ThingCategoryDefOf.StoneBlocks.DescendantThingDefs);
            return defList;
        }

        public override PriceType GetPriceForTile(int tile)
        {
            switch (Find.WorldGrid[tile].hilliness)
            {
                case RimWorld.Planet.Hilliness.Flat:
                    return PriceType.Exorbitant;
                case RimWorld.Planet.Hilliness.SmallHills:
                    return PriceType.Expensive;
                case RimWorld.Planet.Hilliness.LargeHills:
                    return PriceType.Cheap;
                case RimWorld.Planet.Hilliness.Mountainous:
                    return PriceType.VeryCheap;
                default:
                    return PriceType.Normal;
            }
        }
    }
}
