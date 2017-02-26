//using CommunityCoreLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Verse;

namespace EnhancedDevelopment.Plants24H
{
    /*
    public class DetourInjector : SpecialInjector
    {

        public override bool Inject()
        {

            // ---------------------------------------------- Detour Plant.Resting ----------------------------------------------

            Log.Message("RimWorld_Plant_Resting.");
            PropertyInfo RimWorld_Plant_Resting = typeof(RimWorld.Plant).GetProperty("Resting", BindingFlags.NonPublic | BindingFlags.Instance);
            this.LogNULL(RimWorld_Plant_Resting, "RimWorld_Plant_Resting");

            Log.Message("RimWorld_Plant_Resting_Getter.");
            MethodInfo RimWorld_Plant_Resting_Getter = RimWorld_Plant_Resting.GetGetMethod(true);
            this.LogNULL(RimWorld_Plant_Resting_Getter, "RimWorld_Plant_Resting_Getter");


           Log.Message("ED_Plant_Resting.");
            PropertyInfo ED_Plant_Resting = typeof(EnhancedDevelopment.Plants24H.Detours._Plant).GetProperty("_Resting", BindingFlags.NonPublic | BindingFlags.Instance);
            this.LogNULL(ED_Plant_Resting, "ED_Plant_Resting");

            Log.Message("ED_Plant_Resting_Getter.");
            MethodInfo ED_Plant_Resting_Getter = ED_Plant_Resting.GetGetMethod(true);
            this.LogNULL(ED_Plant_Resting_Getter, "ED_Plant_Resting_Getter");

            Log.Message("TryDetourFromTo.");
            if (!CommunityCoreLibrary.Detours.TryDetourFromTo(RimWorld_Plant_Resting_Getter, ED_Plant_Resting_Getter))
            {
                return false;
            }

            Log.Message("Plants24H.DetourInjector.Inject() Compleated.");

            return true;
        }

        private void LogNULL(object objectToTest, String name, bool logSucess = false)
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
*/
}
