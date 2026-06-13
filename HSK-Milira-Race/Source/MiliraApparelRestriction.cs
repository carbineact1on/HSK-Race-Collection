using System;
using HarmonyLib;
using RimWorld;
using Verse;

namespace HSKMiliraCompat
{
    // Lets Milira pawns wear ANY backpack / utility-carry apparel (CE Backpack,
    // Webbing / tac-vest, vanilla Belt-layer packs) without each item having to be
    // hand-added to the HAR whiteApparelList by defName.
    //
    // WHY: Milira_Race sets onlyUseRaceRestrictedApparel=true, so it can only wear
    // its own race-restricted apparel + whatever is explicitly whitelisted. That
    // means every modded backpack is refused unless listed by defName. Maintaining
    // that list per-mod is impractical (user request, 2026-05). HAR has no tag- or
    // layer-based whitelist, so we do it in code.
    //
    // SCOPE (deliberately narrow): only apparel whose layers are ENTIRELY within the
    // utility/carry set {Backpack, Webbing, Belt} is auto-allowed. Anything touching
    // a body layer (OnSkin / Middle / Shell / Overhead / head layers) stays
    // restricted, so clothing and armor that would clip with the Milira wings/body
    // are NOT affected - this only opens up packs, webbing, and belts.
    //
    // Patches AlienRace.RaceRestrictionSettings.CanWear(ThingDef apparel, ThingDef race)
    // via reflection so there is no hard dependency on Humanoid Alien Races; if HAR
    // (or that method) is absent, the patch quietly no-ops.
    public static class MiliraApparelRestriction
    {
        private const string MiliraRaceDefName = "Milira_Race";

        // Carry/utility layers that do not clip with the Milira body or wings.
        private static readonly string[] AllowedLayers = { "Backpack", "Webbing", "Belt" };

        // Sister-race apparel Milira may also wear (renders on the shared bodyType).
        private static readonly string[] CrossWearPrefixes = { "Wolfein_" };

        private static bool loggedError;

        public static void Apply(Harmony harmony)
        {
            var t = AccessTools.TypeByName("AlienRace.RaceRestrictionSettings");
            if (t == null)
            {
                Log.Warning("[HSK Milira Compat] AlienRace.RaceRestrictionSettings not found - "
                    + "universal-backpack patch skipped (Humanoid Alien Races not loaded?).");
                return;
            }

            var target = AccessTools.Method(t, "CanWear", new[] { typeof(ThingDef), typeof(ThingDef) });
            if (target == null)
            {
                Log.Warning("[HSK Milira Compat] RaceRestrictionSettings.CanWear(ThingDef,ThingDef) "
                    + "not found - universal-backpack patch skipped.");
                return;
            }

            harmony.Patch(target,
                postfix: new HarmonyMethod(typeof(MiliraApparelRestriction), nameof(CanWearPostfix)));
            Log.Message("[HSK Milira Compat] Milira universal-backpack apparel patch applied.");
        }

        // Robust to argument order: figure out which arg is the Milira race def and
        // which is the apparel def, rather than assuming HAR's parameter order.
        public static void CanWearPostfix(ref bool __result, ThingDef __0, ThingDef __1)
        {
            try
            {
                if (__result) return; // already allowed - nothing to override

                ThingDef apparel;
                if (__0 != null && __0.defName == MiliraRaceDefName) apparel = __1;
                else if (__1 != null && __1.defName == MiliraRaceDefName) apparel = __0;
                else return; // not a Milira wearability check

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
                    if (!allowed) return; // a non-utility layer present -> keep restricted
                }

                __result = true; // every layer is utility/carry -> let Milira wear it
            }
            catch (Exception e)
            {
                if (!loggedError)
                {
                    loggedError = true;
                    Log.Warning("[HSK Milira Compat] CanWear postfix threw (will not retry-log): " + e);
                }
            }
        }
    }
}
