using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using Verse;
using UnityEngine;

namespace RimworldApocalypse.Trade
{
    public class StockGenerator_CommodityWood : StockGenerator_Commodity
    {
        public override PriceType GetPriceForTile(int tile)
        {
            var tradeExtension = Find.WorldGrid[tile].biome.GetModExtension<BiomeTradeExtension>() ?? new BiomeTradeExtension();
            return tradeExtension.woodPriceType;
        }

        protected override IEnumerable<ThingDef> AllCommodityThingDefs()
        {
            yield return ThingDefOf.WoodLog;
        }
    }
}
