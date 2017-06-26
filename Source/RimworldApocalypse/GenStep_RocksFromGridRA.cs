using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using RimWorld.Planet;
using Verse;
using UnityEngine;

namespace RimworldApocalypse
{
    // Mostly copy-pasted from GenStep_RocksFromGrid - would be better as a Harmony infix
    public class GenStep_RocksFromGridRA : GenStep_RocksFromGrid
    {
        private class RoofThreshold
        {
            public RoofDef roofDef;

            public float minGridVal;
        }

        private bool IsNaturalRoofAt(IntVec3 c, Map map)
        {
            return c.Roofed(map) && c.GetRoof(map).isNatural;
        }

        public override void Generate(Map map)
        {
            if (map.TileInfo.WaterCovered)
            {
                return;
            }
            map.regionAndRoomUpdater.Enabled = false;
            float num = 0.7f;
            List<RoofThreshold> list = new List<RoofThreshold>();
            list.Add(new RoofThreshold
            {
                roofDef = RoofDefOf.RoofRockThick,
                minGridVal = num * 1.14f
            });
            list.Add(new RoofThreshold
            {
                roofDef = RoofDefOf.RoofRockThin,
                minGridVal = num * 1.04f
            });
            MapGenFloatGrid elevation = MapGenerator.Elevation;
            foreach (IntVec3 current in map.AllCells)
            {
                float num2 = elevation[current];
                if (num2 > num)
                {
                    ThingDef def = RockDefAt(current);
                    GenSpawn.Spawn(def, current, map);
                    for (int i = 0; i < list.Count; i++)
                    {
                        if (num2 > list[i].minGridVal)
                        {
                            map.roofGrid.SetRoof(current, list[i].roofDef);
                            break;
                        }
                    }
                }
            }
            BoolGrid visited = new BoolGrid(map);
            List<IntVec3> toRemove = new List<IntVec3>();
            foreach (IntVec3 current2 in map.AllCells)
            {
                if (!visited[current2])
                {
                    if (this.IsNaturalRoofAt(current2, map))
                    {
                        toRemove.Clear();
                        map.floodFiller.FloodFill(current2, (IntVec3 x) => this.IsNaturalRoofAt(x, map), delegate (IntVec3 x)
                        {
                            visited[x] = true;
                            toRemove.Add(x);
                        }, false);
                        if (toRemove.Count < 20)
                        {
                            for (int j = 0; j < toRemove.Count; j++)
                            {
                                map.roofGrid.SetRoof(toRemove[j], null);
                            }
                        }
                    }
                }
            }
            GenStep_ScatterLumpsMineable genStep_ScatterLumpsMineable = new GenStep_ScatterLumpsMineable();
            float num3 = 10f;

            // Use RA values here
            var defExtension = def.GetModExtension<GenStepExtension>() ?? new GenStepExtension();
            switch (Find.WorldGrid[map.Tile].hilliness)
            {
                case Hilliness.Flat:
                    num3 = defExtension.FlatMineablesPer10kCells;
                    break;
                case Hilliness.SmallHills:
                    num3 = defExtension.SmallHillsMineablesPer10kCells;
                    break;
                case Hilliness.LargeHills:
                    num3 = defExtension.LargeHillsMineablesPer10kCells;
                    break;
                case Hilliness.Mountainous:
                    num3 = defExtension.MountainousMineablesPer10kCells;
                    break;
                case Hilliness.Impassable:
                    num3 = 16f;
                    break;
            }
            genStep_ScatterLumpsMineable.countPer10kCellsRange = new FloatRange(num3, num3);
            genStep_ScatterLumpsMineable.Generate(map);
            map.regionAndRoomUpdater.Enabled = true;
        }
    }
}
