using System;
using HarmonyLib;
using RimWorld;
using Verse;

namespace HSKWolfeinCompat
{
    // Postfixes AlienRace.RaceRestrictionSettings.CanWear for Wolfein_Race
    // (onlyUseRaceRestrictedApparel=true). Two allowances, reflection-based so
    // there is no hard HAR dependency:
    //   1. Any apparel whose layers are ENTIRELY within {Backpack, Webbing, Belt}
    //      (backpacks / tac-vests / utility belts).
    //   2. Apparel from the sister race Milira (defName prefix Milira_ / Milian_),
    //      since the two races share a standard-bodyType apparel render.
    public static class WolfeinApparelRestriction
    {
        private const string WolfeinRaceDefName = "Wolfein_Race";

        private static readonly string[] AllowedLayers = { "Backpack", "Webbing", "Belt" };
        private static readonly string[] CrossWearPrefixes = { "Milira_", "Milian_" };

        private static bool loggedError;

        public static void Apply(Harmony harmony)
        {
            var t = AccessTools.TypeByName("AlienRace.RaceRestrictionSettings");
            if (t == null)
            {
                Log.Warning("[HSK Wolfein Compat] AlienRace.RaceRestrictionSettings not found - "
                    + "apparel patch skipped (Humanoid Alien Races not loaded?).");
                return;
            }

            var target = AccessTools.Method(t, "CanWear", new[] { typeof(ThingDef), typeof(ThingDef) });
            if (target == null)
            {
                Log.Warning("[HSK Wolfein Compat] RaceRestrictionSettings.CanWear(ThingDef,ThingDef) "
                    + "not found - apparel patch skipped.");
                return;
            }

            harmony.Patch(target,
                postfix: new HarmonyMethod(typeof(WolfeinApparelRestriction), nameof(CanWearPostfix)));
            Log.Message("[HSK Wolfein Compat] Wolfein apparel patch applied (backpacks + Milira clothing).");
        }

        public static void CanWearPostfix(ref bool __result, ThingDef __0, ThingDef __1)
        {
            try
            {
                if (__result) return;

                ThingDef apparel;
                if (__0 != null && __0.defName == WolfeinRaceDefName) apparel = __1;
                else if (__1 != null && __1.defName == WolfeinRaceDefName) apparel = __0;
                else return;

                if (apparel == null) return;

                for (int i = 0; i < CrossWearPrefixes.Length; i++)
                {
                    if (apparel.defName.StartsWith(CrossWearPrefixes[i], StringComparison.Ordinal))
                    {
                        __result = true;
                        return;
                    }
                }

                var ap = apparel.apparel;
                if (ap?.layers == null || ap.layers.Count == 0) return;

                foreach (var layer in ap.layers)
                {
                    if (layer == null) return;
                    bool allowed = false;
                    for (int i = 0; i < AllowedLayers.Length; i++)
                    {
                        if (layer.defName == AllowedLayers[i]) { allowed = true; break; }
                    }
                    if (!allowed) return;
                }

                __result = true;
            }
            catch (Exception e)
            {
                if (!loggedError)
                {
                    loggedError = true;
                    Log.Warning("[HSK Wolfein Compat] CanWear postfix threw (will not retry-log): " + e);
                }
            }
        }
    }
}
