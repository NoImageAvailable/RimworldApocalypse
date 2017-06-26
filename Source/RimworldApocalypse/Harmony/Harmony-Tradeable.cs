using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using RimWorld.Planet;
using Verse;
using UnityEngine;
using Harmony;
using RimworldApocalypse.Trade;

namespace RimworldApocalypse.Harmony
{
    [HarmonyPatch(typeof(Tradeable), "PriceTypeFor")]
    public class Harmony_Tradeable_PriceTypeFor
    {
        public static bool Prefix(Tradeable __instance, ref PriceType __result, TradeAction action)
        {
            var thingDef = __instance.ThingDef;
            if (thingDef == ThingDefOf.Silver)
            {
                __result = PriceType.Undefined;
                return false;
            }
            var trader = TradeSession.trader;
            foreach (StockGenerator gen in trader.TraderKind.stockGenerators)
            {
                PriceType result;
                if (gen.TryGetPriceType(thingDef, action, out result))
                {
                    var commodityGen = gen as StockGenerator_Commodity;
                    var settlement = trader as Settlement;
                    if (commodityGen != null && settlement != null)
                    {
                        __result = commodityGen.GetPriceForTile(settlement.Tile);
                    }
                    else
                    {
                        gen.TryGetPriceType(thingDef, action, out __result);
                    }
                    break;
                }
            }
            return false;
        }
    }
}
