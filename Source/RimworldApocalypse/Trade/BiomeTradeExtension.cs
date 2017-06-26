using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using Verse;
using UnityEngine;

namespace RimworldApocalypse.Trade
{
    public class BiomeTradeExtension : DefModExtension
    {
        public PriceType woodPriceType = PriceType.Normal;
        public PriceType chemfuelPriceType = PriceType.Normal;
        public PriceType producePriceType = PriceType.Normal;
    }
}
