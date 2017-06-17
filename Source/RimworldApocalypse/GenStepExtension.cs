using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using Verse;
using UnityEngine;

namespace RimworldApocalypse
{
    public class GenStepExtension : DefModExtension
    {
        public float FlatMineablesPer10kCells = 4;
        public float SmallHillsMineablesPer10kCells = 8;
        public float LargeHillsMineablesPer10kCells = 11;
        public float MountainousMineablesPer10kCells = 15;
    }
}
