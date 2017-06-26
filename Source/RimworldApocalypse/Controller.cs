using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using RimWorld;
using Verse;
using UnityEngine;
using Harmony;

namespace RimworldApocalypse
{
    public class Controller : Mod
    {
        public Controller(ModContentPack content) : base(content)
        {
            HarmonyInstance.Create("RimworldApocalypse.Harmony").PatchAll(Assembly.GetExecutingAssembly());
            Log.Message("Rimworld Apocalypse :: Initialized");
        }
    }
}
