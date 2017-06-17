using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using Verse;
using UnityEngine;

namespace RimworldApocalypse
{
    public class MapCompCountMineables : MapComponent
    {
        public MapCompCountMineables(Map map) : base(map)
        {
        }

        public override void MapGenerated()
        {
            var mineables = map.listerThings.AllThings.Where(t => t.def.mineable);
            var types = mineables.Select(m => m.def);
            foreach (ThingDef cur in types.Distinct())
            {
                float count = mineables.Count(t => t.def == cur);
                Log.Message("RA :: map contains " + cur.LabelCap + " x " + count.ToString() + " (" + GenText.ToStringPercent(count / map.AllCells.Count()) + ") ");
            }
        }
    }
}
