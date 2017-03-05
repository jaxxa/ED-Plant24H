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
            Log.Message("Plants24H, Starting Patching.");

            //var harmony = HarmonyInstance.Create("com.company.project.product");
            //var original = typeof(TheClass).GetMethod("TheMethod");
            //var prefix = typeof(MyPatchClass1).GetMethod("SomeMethod");
            //var postfix = typeof(MyPatchClass2).GetMethod("SomeMethod");
            //harmony.Patch(original, new HarmonyMethod(prefix), new HarmonyMethod(postfix));

            var harmony = HarmonyInstance.Create("Jaxxa.EnhancedDevelopment.Plants24H");
            //harmony.PatchAll(Assembly.GetExecutingAssembly());
            
            //Get the Origional Resting Property
            PropertyInfo RimWorld_Plant_Resting = typeof(RimWorld.Plant).GetProperty("Resting", BindingFlags.NonPublic | BindingFlags.Instance);
            Main.LogNULL(RimWorld_Plant_Resting, "RimWorld_Plant_Resting", false);

            //Get the Resting Property Getter Method
            MethodInfo RimWorld_Plant_Resting_Getter = RimWorld_Plant_Resting.GetGetMethod(true);
            Main.LogNULL(RimWorld_Plant_Resting_Getter, "RimWorld_Plant_Resting_Getter", false);
           
            //Get the Prefix Patch
            var prefix = typeof(PlantRestingPatcher).GetMethod("Prefix", BindingFlags.Public | BindingFlags.Static);
            Main.LogNULL(prefix, "Prefix", false);

            //Apply the Prefix Patch
            harmony.Patch(RimWorld_Plant_Resting_Getter, new HarmonyMethod(prefix), null);

            Log.Message("Plants24H, Finished Patching.");
        }

        /// <summary>
        /// Debug Logging Helper
        /// </summary>
        /// <param name="objectToTest"></param>
        /// <param name="name"></param>
        /// <param name="logSucess"></param>
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
        public static Boolean Prefix(ref bool __result )
        {
            //Write to log to debug id the patch is running.
            //Log.Message("Prefix Running");

            //This is the result that will be used, note that it was passed as a ref.
            __result = false;

            //Retuen False so the origional method is not executed, overriting the false result.
            return false;
        }
        
    }
}
