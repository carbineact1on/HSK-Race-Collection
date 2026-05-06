// HSK-VRE-Android Harmony patch.
//
// Why: VREAndroids.Window_AndroidCreation.OnGenesChanged() hardcodes the
// android creation cost via ThingDefOf lookups:
//
//     requiredItems = new List<ThingDefCount> {
//         new ThingDefCount(VREA_DefOf.VREA_PersonaSubcore, 1),
//         new ThingDefCount(ThingDefOf.Plasteel,         125),
//         new ThingDefCount(ThingDefOf.Uranium,           30),  <-- raw ore in HSK
//         new ThingDefCount(ThingDefOf.ComponentSpacer,    7),
//     };
//
// HSK uses raw Uranium as the ore stuff "Carnotite (U+V)" and DepletedUranium
// as the refined ingot. Spacer-tier crafting recipes should ask for the
// refined ingot, not raw ore. Pure XML patches can't change ThingDefOf
// references, so we postfix-rewrite the requiredItems list at runtime.

using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using Verse;
using VREAndroids;

namespace HSKVREAndroidPatch
{
    [StaticConstructorOnStartup]
    public static class HSKVREAndroidPatchInit
    {
        static HSKVREAndroidPatchInit()
        {
            new Harmony("CarbineAction.HSK.VRE.Android.Patch").PatchAll();
            Log.Message("[HSK-VRE-Android] Harmony patch loaded - swapping Uranium -> DepletedUranium in Window_AndroidCreation.");
        }
    }

    [HarmonyPatch(typeof(Window_AndroidCreation), nameof(Window_AndroidCreation.OnGenesChanged))]
    public static class Window_AndroidCreation_OnGenesChanged_Patch
    {
        static void Postfix(Window_AndroidCreation __instance)
        {
            // requiredItems is declared on Window_AndroidCreation (or its base
            // Window_CreateAndroidBase). Walk the inheritance chain to find it.
            var field = AccessTools.Field(typeof(Window_AndroidCreation), "requiredItems");
            if (field == null)
            {
                var t = typeof(Window_AndroidCreation).BaseType;
                while (t != null && field == null)
                {
                    field = AccessTools.Field(t, "requiredItems");
                    t = t.BaseType;
                }
            }
            if (field == null) return;

            if (!(field.GetValue(__instance) is List<ThingDefCount> list)) return;

            ThingDef uranium = ThingDefOf.Uranium;
            ThingDef depleted = DefDatabase<ThingDef>.GetNamedSilentFail("DepletedUranium");
            if (uranium == null || depleted == null) return; // HSK not loaded -> leave vanilla

            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].ThingDef == uranium)
                {
                    list[i] = new ThingDefCount(depleted, list[i].Count);
                }
            }
        }
    }
}
