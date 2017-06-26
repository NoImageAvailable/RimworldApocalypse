using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using Verse;
using UnityEngine;
using Harmony;

namespace RimworldApocalypse.Harmony
{
    [HarmonyPatch(typeof(PriceTypeUtlity), "PriceMultiplier")]
    public static class Harmony_PriceTypeUtility_PriceMultiplier
    {
        public static bool Prefix(ref float __result, PriceType pType)
        {
            switch (pType)
            {
                case PriceType.VeryCheap:
                    __result = 0.4f;
                    break;
                case PriceType.Cheap:
                    __result = 0.7f;
                    break;
                case PriceType.Normal:
                    __result = 1f;
                    break;
                case PriceType.Expensive:
                    __result = 1.4f;
                    break;
                case PriceType.Exorbitant:
                    __result = 2.5f;
                    break;
                default:
                    __result = -1f;
                    break;
            }
            return false;
        }
    }
}
