using Harmony;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Verse;

namespace EnhancedDevelopment.Plants24H.Plants24H
{
    [StaticConstructorOnStartup]
    class Main
    {
        static Main()
        {

            //var harmony = HarmonyInstance.Create("com.company.project.product");
            //var original = typeof(TheClass).GetMethod("TheMethod");
            //var prefix = typeof(MyPatchClass1).GetMethod("SomeMethod");
            //var postfix = typeof(MyPatchClass2).GetMethod("SomeMethod");
            //harmony.Patch(original, new HarmonyMethod(prefix), new HarmonyMethod(postfix));

            var harmony = HarmonyInstance.Create("Jaxxa.EnhancedDevelopment.Plants24H");
            //harmony.PatchAll(Assembly.GetExecutingAssembly());

            //var original = typeof(Plant).GetMethod("TheMethod");


            Log.Message("RimWorld_Plant_Resting.");
            PropertyInfo RimWorld_Plant_Resting = typeof(RimWorld.Plant).GetProperty("Resting", BindingFlags.NonPublic | BindingFlags.Instance);
            Main.LogNULL(RimWorld_Plant_Resting, "RimWorld_Plant_Resting", true);

            Log.Message("RimWorld_Plant_Resting_Getter.");
            MethodInfo RimWorld_Plant_Resting_Getter = RimWorld_Plant_Resting.GetGetMethod(true);
            Main.LogNULL(RimWorld_Plant_Resting_Getter, "RimWorld_Plant_Resting_Getter", true);

            //var postfix = typeof(MyPatchClass2).GetMethod("SomeMethod");

            var prefix = typeof(PlantRestingPatcher).GetMethod("PrefixMessage");
            Main.LogNULL(prefix, "Prefix", true);

            harmony.Patch(RimWorld_Plant_Resting_Getter, new HarmonyMethod(prefix), new HarmonyMethod(prefix), new HarmonyMethod(prefix));
            Log.Message("Patched");
        }


        private static void LogNULL(object objectToTest, String name, bool logSucess = false)
        {
            if (objectToTest == null)
            {
                Log.Error(name + " Is NULL.");
            }
            else
            {
                if (logSucess)
                {
                    Log.Message(name + " Is Not NULL.");
                }
            }
        }
    }


    //[HarmonyPatch(typeof(Plant))]
    //[HarmonyPatch("Add")]
    //[HarmonyPatch("Resting_Getter")]
    static class PlantRestingPatcher
    {

        // prefix
        // - wants instance, result and count
        // - wants to change count
        // - returns a boolean that controls if original is executed (true) or not (false)
        static void PrefixMessage()
        {
            Log.Message("Prefix Running");
        }
        
    }
}
