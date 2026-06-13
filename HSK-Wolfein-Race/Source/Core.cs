using System;
using HarmonyLib;
using Verse;

namespace HSKWolfeinCompat
{
    [StaticConstructorOnStartup]
    public static class Core
    {
        public const string HarmonyId = "CarbineAction.HSK.Wolfein.Compat";

        static Core()
        {
            try
            {
                var harmony = new Harmony(HarmonyId);
                WolfeinApparelRestriction.Apply(harmony);
            }
            catch (Exception e)
            {
                Log.Error("[HSK Wolfein Compat] Failed to apply patches: " + e);
            }
        }
    }
}
