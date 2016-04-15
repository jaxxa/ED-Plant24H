using RimWorld;
using System.Text;
using UnityEngine;
using Verse;

namespace Enhanced_Development.Plants24H
{
    public class Plant : RimWorld.Plant
    {

        private int unlitTicks;

        private float GrowthPerTick
        {
            get
            {
                if (this.LifeStage != PlantLifeStage.Growing)
                    return 0.0f;
                return (float)(1.0 / (60000.0 * (double)this.def.plant.growDays)) * this.GrowthRate;
            }
        }

        private void CheckTemperatureMakeLeafless()
        {
            float num = 8f;
            if ((double)GridsUtility.GetTemperature(this.Position) >= (double)Gen.HashOffset((Thing)this) * 0.00999999977648258 % (double)num - (double)num - 2.0)
                return;
            this.MakeLeafless();
        }

        private bool HasEnoughLightToGrow
        {
            get
            {
                return (double)this.GrowthRateFactor_Light > 1.0 / 1000.0;
            }
        }

        public override void TickLong()
        {
            this.CheckTemperatureMakeLeafless();
            if (!GenPlant.GrowthSeasonNow(this.Position))
                return;
            if (!this.HasEnoughLightToGrow)
                this.unlitTicks += 2000;
            else
                this.unlitTicks = 0;
            bool flag = this.LifeStage == PlantLifeStage.Mature;
            this.growth += this.GrowthPerTick * 2000f;
            if (!flag && this.LifeStage == PlantLifeStage.Mature)
                this.NewlyMatured();
            if (this.def.plant.LimitedLifespan)
            {
                this.age += 2000;
                if (this.Rotting)
                {
                    int amount = Mathf.CeilToInt(10f);
                    this.TakeDamage(new DamageInfo(DamageDefOf.Rotting, amount, (Thing)null, new BodyPartDamageInfo?(), (ThingDef)null));
                }
            }
            if (this.Destroyed || !this.def.plant.shootsSeeds || ((double)this.growth < 0.600000023841858 || !Rand.MTBEventOccurs(this.def.plant.seedEmitMTBDays, 60000f, 2000f)) || (!GenPlant.SnowAllowsPlanting(this.Position) || GridsUtility.Roofed(this.Position)))
                return;
            GenPlantReproduction.TrySpawnSeed(this.Position, this.def, SeedTargFindMode.ReproduceSeed, (Thing)this);
        }

        private void NewlyMatured()
        {
            if (!this.CurrentlyCultivated())
                return;
            Find.MapDrawer.MapMeshDirty(this.Position, MapMeshFlag.Things);
        }

        private bool CurrentlyCultivated()
        {
            if (!this.def.plant.Sowable)
                return false;
            Zone zone = Find.ZoneManager.ZoneAt(this.Position);
            if (zone != null && zone is Zone_Growing)
                return true;
            Building edifice = GridsUtility.GetEdifice(this.Position);
            return edifice != null && edifice.def.building.SupportsPlants;
        }


        public override string GetInspectString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(base.GetInspectString());
            if (this.LifeStage == PlantLifeStage.Growing)
            {
                stringBuilder.AppendLine(Translator.Translate("PercentGrowth", (object)this.GrowthPercentString));
                stringBuilder.AppendLine(Translator.Translate("GrowthRate") + ": " + GenText.ToStringPercent(this.GrowthRate));
                //if (this.Resting)

                //Resting is currently modded out.
                if (false)
                {
                 //   stringBuilder.AppendLine(Translator.Translate("PlantResting"));
                }
                else
                {
                    if (!this.HasEnoughLightToGrow)
                        stringBuilder.AppendLine(Translator.Translate("PlantNeedsLightLevel") + ": " + GenText.ToStringPercent(this.def.plant.growMinGlow));
                    float factorTemperature = this.GrowthRateFactor_Temperature;
                    if ((double)factorTemperature < 0.990000009536743)
                    {
                        if ((double)factorTemperature < 0.00999999977648258)
                            stringBuilder.AppendLine(Translator.Translate("OutOfIdealTemperatureRangeNotGrowing"));
                        else
                            stringBuilder.AppendLine(Translator.Translate("OutOfIdealTemperatureRange", (object)Mathf.RoundToInt(factorTemperature * 100f).ToString()));
                    }
                }
            }
            else if (this.LifeStage == PlantLifeStage.Mature)
            {
                if (this.def.plant.Harvestable)
                    stringBuilder.AppendLine(Translator.Translate("ReadyToHarvest"));
                else
                    stringBuilder.AppendLine(Translator.Translate("Mature"));
            }
            return stringBuilder.ToString();
        }


        private string GrowthPercentString
        {
            get
            {
                return GenText.ToStringPercent(this.growth + 0.0001f);
            }
        }

        //public bool Rotting
        //{
        //    get
        //    {
        //        return this.def.plant.LimitedLifespan && this.age > this.def.plant.LifespanTicks || this.unlitTicks > 450000;
        //    }
        //}

        public override void ExposeData()
        {
            Scribe_Values.LookValue<int>(ref this.unlitTicks, "unlitTicks", 0, false);
            base.ExposeData();
        }
    }
}
