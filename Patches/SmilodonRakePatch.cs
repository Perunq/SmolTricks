using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Blueprints.Configurators;
using BlueprintCore.Blueprints.Configurators.UnitLogic.ActivatableAbilities;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using BlueprintCore.Utils.Types;
using HarmonyLib;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Facts;
using Kingmaker.Blueprints.Items.Weapons;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.ActivatableAbilities;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Kingmaker.Blueprints.BlueprintUnit;

namespace SmolCraft.Patches
{
    [HarmonyPatch(typeof(BlueprintUnit), nameof(BlueprintUnit.Body))]
    class SmilodonRakePatch
    {
        public static BlueprintFeature RakeFeature;
        public static BlueprintBuff RakeBuff;
        public static BlueprintBuff RakeExtraAttacksBuff;
        public static BlueprintFeature RakeExtraAttacksFeature;        
        public static BlueprintBuff RakeExtraAttacksFeatureBuff;

        public static BlueprintActivatableAbility RakeActivatableAbility;
        private static readonly string FeatName = "SmilodonRakeFeature";
        private static readonly string FeatGuid = "12cb49b4-79a9-4c6f-b5b1-64ce675e2001";

        private static readonly string DisplayName = "SmilodonRakeFeature.Name";
        private static readonly string Description = "SmilodonRakeFeature.Description";

        private static readonly string BuffName = "SmilodonRakeBuff";
        private static readonly string BuffGuid = "12cb49b4-79a9-4c6f-b5b1-64ce675e2002";

        private static readonly string BuffDisplayName = "SmilodonRakeFeature.Name";
        private static readonly string BuffDescription = "SmilodonRakeFeature.Description";

        private static readonly string ExtraAttacksBuffName = "SmilodonRakeAttacksBuff";
        private static readonly string ExtraAttacksBuffGuid = "12cb49b4-79a9-4c6f-b5b1-64ce675e2004";

        private static readonly string ExtraAttacksFeatureName = "SmilodonRakeAttacksFeature";
        private static readonly string ExtraAttacksFeatureGuid = "12cb49b4-79a9-4c6f-b5b1-64ce675e2005";

        private static readonly string ExtraAttacksFeatureBuffName = "SmilodonRakeAttacksFeatureBuff";
        private static readonly string ExtraAttacksFeatureBuffGuid = "12cb49b4-79a9-4c6f-b5b1-64ce675e2006";


        private static readonly string ExtraAttacksBuffDisplayName = "SmilodonRakeFeature.Name";
        private static readonly string ExtraAttacksBuffDescription = "SmilodonRakeFeature.Description";

        private static readonly string AbilityName = "SmilodonRakeAbility";
        private static readonly string AbilityGuid = "12cb49b4-79a9-4c6f-b5b1-64ce675e2003";

        private static readonly string AbilityDisplayName = "SmilodonRakeFeature.Name";
        private static readonly string AbilityDescription = "SmilodonRakeFeature.Description";

        private static readonly string Icon = "assets/icons/quillen.jpg";
        public static BlueprintItemWeaponReference bite;
        public static void Configure()
        {

            RakeExtraAttacksFeatureBuff = BuffConfigurator.New(ExtraAttacksFeatureBuffName, ExtraAttacksFeatureBuffGuid)
                      .AddComponent<ManeuverTrigger>(Man =>
                          {
                              Man.ManeuverType = Kingmaker.RuleSystem.Rules.CombatManeuver.Grapple;
                              Man.OnlySuccess = true;
                              Man.Action = ActionsBuilder.New()
                                  .MeleeAttack(extraAttack: true)
                                  .MeleeAttack(extraAttack: true)
                                  .Build();
                          }
                          )
                      .SetStacking(StackingType.Replace)
                      .SetTickEachSecond(false)
                      .SetFrequency(Kingmaker.UnitLogic.Mechanics.DurationRate.Rounds)
                      .Configure(delayed: true);

            RakeExtraAttacksFeature = FeatureConfigurator.New(ExtraAttacksFeatureName, ExtraAttacksFeatureGuid, FeatureGroup.Feat)
                .AddFacts(new List<Blueprint<BlueprintUnitFactReference>>{
                    RakeExtraAttacksFeatureBuff.ToReference<BlueprintUnitFactReference>()
                    //,RakeExtraAttacksBuff.ToReference<BlueprintUnitFactReference>()
                })
                .Configure(delayed: true);





            RakeExtraAttacksBuff = BuffConfigurator.New(ExtraAttacksBuffName, ExtraAttacksBuffGuid)


                .AddRemoveBuffIfCasterIsMissing()
                .AddShifterGrabInitiatorBuff(attackRollBonus: -2, dexterityBonus: -4)
                .AddCMBBonusForManeuver(descriptor: ModifierDescriptor.Circumstance, value: ContextValues.Constant(5))
                .AddFacts(new List<Blueprint<BlueprintUnitFactReference>>{
                    RakeExtraAttacksFeature.ToReference<BlueprintUnitFactReference>()
                    //,RakeExtraAttacksBuff.ToReference<BlueprintUnitFactReference>()
                })
                .SetTickEachSecond(false)
                .SetFrequency(Kingmaker.UnitLogic.Mechanics.DurationRate.Rounds)

                .Configure(delayed: true);

            RakeBuff = BuffConfigurator.New(BuffName, BuffGuid)
                .AddInitiatorAttackWithWeaponTrigger(
                onlyHit: true,
                checkWeaponCategory: true,
                category: Kingmaker.Enums.WeaponCategory.Bite,
                action: ActionsBuilder.New().
                CombatManeuverCustom(type: Kingmaker.RuleSystem.Rules.CombatManeuver.Grapple
                , failure: null
                , success: ActionsBuilder.New()

                    .Grapple
                        (
                            casterBuff: RakeExtraAttacksBuff.ToReference<BlueprintBuffReference>(),
                            targetBuff: BuffRefs.TigerGrappledTargetBuff.Cast<BlueprintBuffReference>().Reference
                        ))
                        .Build()

                )

                //.OnConfigure(
                //bp =>
                //{
                //    bp.
                //}
                //)

                .Configure(delayed: true);




            RakeActivatableAbility = ActivatableAbilityConfigurator.New(AbilityName, AbilityGuid)
                .OnConfigure(bp =>
                {
                    bp.m_Buff = RakeBuff.ToReference<BlueprintBuffReference>();
                    bp.IsOnByDefault = true;
                    bp.m_ActivateOnUnitAction = Kingmaker.UnitLogic.ActivatableAbilities.AbilityActivateOnUnitActionType.Attack;
                    bp.DeactivateImmediately = true;
                    bp.ActivationType = Kingmaker.UnitLogic.ActivatableAbilities.AbilityActivationType.Immediately;
                    bp.m_ActivateWithUnitCommand = 0;

                }
                ).Configure(delayed: true);


            RakeFeature = FeatureConfigurator.New(FeatName, FeatGuid, FeatureGroup.Feat)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetIcon(Icon)
                .AddFacts(new List<Blueprint<BlueprintUnitFactReference>>{
                    RakeActivatableAbility.ToReference<BlueprintUnitFactReference>()
                    //,RakeExtraAttacksBuff.ToReference<BlueprintUnitFactReference>()
                })
                .Configure(delayed: true);

            UnitConfigurator.For(UnitRefs.AnimalCompanionUnitSmilodon)
              .OnConfigure(
                bp =>
                {
                    bite = ((BlueprintItemWeapon)bp.Body.m_PrimaryHand.GetBlueprint()).ToReference<BlueprintItemWeaponReference>();
                    if (bp.Body.m_AdditionalLimbs.Length > 1)
                    {
                        bp.Body.m_AdditionalLimbs = bp.Body.m_AdditionalLimbs.Take(1).ToArray();
                        //bp.Body.m_AdditionalLimbs                        .Take(1).ToArray();

                    }
                    //bp.Body.DisableHands = true;
                    //bp.Body.PrimaryHand = null;
                    //bp.Body.SecondaryHand = null;


                    //bp.Body.m_PrimaryHand = bp.Body.m_SecondaryHand;
                    //bp.Body.m_SecondaryHand = null;

                    //bp.Body.m_PrimaryHandAlternative1 = null;
                    //bp.Body.m_PrimaryHandAlternative2 = null;
                    //bp.Body.m_PrimaryHandAlternative3 = null;
                    //bp.Body.m_SecondaryHandAlternative1 = null;
                    //bp.Body.m_SecondaryHandAlternative2 = null;
                    //bp.Body.m_SecondaryHandAlternative3 = null;
                    //SmilodonAppend = bp.m_AddFacts;
                    //SmilodonAppend.Append<BlueprintUnitFactReference>(FeatureRefs.ShifterGrabTiger.Cast<BlueprintUnitFactReference>().Reference);
                    //bp.m_AddFacts = SmilodonAppend;


                })

              .AddFacts(new List<Blueprint<BlueprintUnitFactReference>>()
                { 
                    //FeatureRefs.ShifterGrabTiger.Cast<BlueprintUnitFactReference>().Reference,
                    //,FeatureRefs.ShifterTigerRend.Cast<BlueprintUnitFactReference>().Reference
                    RakeFeature.ToReference<BlueprintUnitFactReference>()

                }
              )
              //.AddBuffOnCombatStart(feature: BuffRefs.ShifterTigerRend.Cast<BlueprintBuffReference>().Reference)              
              .Configure(delayed: true);



            UnitConfigurator.For(UnitRefs.AnimalCompanionUnitSmilodon)
              .OnConfigure()
            .AddAdditionalLimb(bite)

            .Configure(delayed: true);



        }

        //private static void Postfix(BlueprintUnit __instance)
        //{
        //    if (!__instance.name.Contains("Smilodon")) {
        //        return;
        //    }
        //    int num_limbs = __instance.Body.AdditionalLimbs.Length;
        //    if (num_limbs < 3)
        //    {
        //        return;
        //    }
        //    __instance.Body.m_AdditionalLimbs = __instance.Body.m_AdditionalLimbs.Take(num_limbs - 2).ToArray();
        //}
    }


}



