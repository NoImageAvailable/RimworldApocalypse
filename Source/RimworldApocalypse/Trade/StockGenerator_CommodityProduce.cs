using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using Verse;
using UnityEngine;

namespace RimworldApocalypse.Trade
{
    public class StockGenerator_CommodityProduce : StockGenerator_Commodity
    {
        public override PriceType GetPriceForTile(int tile)
        {
            PriceType hillinessPrice;
            var worldTile = Find.WorldGrid[tile];
            switch (worldTile.hilliness)
            {
                case RimWorld.Planet.Hilliness.Flat:
                    hillinessPrice = PriceType.VeryCheap;
                    break;
                case RimWorld.Planet.Hilliness.SmallHills:
                    hillinessPrice = PriceType.Cheap;
                    break;
                case RimWorld.Planet.Hilliness.LargeHills:
                    hillinessPrice = PriceType.Expensive;
                    break;
                case RimWorld.Planet.Hilliness.Mountainous:
                    hillinessPrice = PriceType.Exorbitant;
                    break;
                default:
                    hillinessPrice = PriceType.Normal;
                    break;
            }
            var tradeExtension = worldTile.biome.GetModExtension<BiomeTradeExtension>() ?? new BiomeTradeExtension();
            var biomePrice = tradeExtension.producePriceType;

            return (PriceType)Mathf.RoundToInt(((int)hillinessPrice * 2 + (int)biomePrice) / 3f);
        }

        protected override IEnumerable<ThingDef> AllCommodityThingDefs()
        {
            var list = RAThingCategoryDefOf.FoodRaw.DescendantThingDefs.Where(f => !RAThingCategoryDefOf.EggsFertilized.DescendantThingDefs.Contains(f) && f.techLevel <= maxTechLevelBuy).ToList();
            list.Add(ThingDefOf.Cloth);
            return list;
        }
    }
}
