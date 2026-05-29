using System;
using HarmonyLib;
using Verse;

namespace HSKMiliraCompat
{
    /// <summary>
    /// Entry point for HSK-Milira-Race compatibility patches.
    /// Currently: universal backpack/utility-apparel support for the Milira race
    /// (see <see cref="MiliraApparelRestriction"/>).
    /// </summary>
    [StaticConstructorOnStartup]
    public static class Core
    {
        public const string HarmonyId = "CarbineAction.HSK.Milira.Compat";

        static Core()
        {
            try
            {
                var harmony = new Harmony(HarmonyId);
                MiliraApparelRestriction.Apply(harmony);
            }
            catch (Exception e)
            {
                Log.Error("[HSK Milira Compat] Failed to apply patches: " + e);
            }
        }
    }
}
