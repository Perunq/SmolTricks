using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Blueprints.Configurators.UnitLogic.ActivatableAbilities;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.UnitLogic.ActivatableAbilities;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmolCraft.NewAbilities
{
    class GrappleAbility
    {


        public static BlueprintFeature BaseGrappleFeature;
        public static BlueprintBuff BaseGrappleBuff;

        public static BlueprintActivatableAbility BaseGrappleActivatableAbility;
        private static readonly string FeatName = "BaseGrappleFeature";
        private static readonly string FeatGuid = "12cb49b4-79a9-4c6f-b5b1-64ce675e2007";

        private static readonly string DisplayName = "BaseGrappleFeature.Name";
        private static readonly string Description = "BaseGrappleFeature.Description";

        private static readonly string BuffName = "BaseGrappleBuff";
        private static readonly string BuffGuid = "12cb49b4-79a9-4c6f-b5b1-64ce675e2008";


        private static readonly string AbilityName = "BaseGrappleAbility";
        private static readonly string AbilityGuid = "12cb49b4-79a9-4c6f-b5b1-64ce675e2009";

        private static readonly string AbilityDisplayName = "BaseGrappleAbility.Name";
        private static readonly string AbilityDescription = "BaseGrappleAbility.Description";

        private static readonly string Icon = "assets/icons/quillen.jpg";

        public static void Configure()
        {


            BaseGrappleBuff = BuffConfigurator.New(BuffName, BuffGuid)
                .AddInitiatorAttackWithWeaponTrigger(
                onlyHit: false,
                checkWeaponCategory: false,
                //category: Kingmaker.Enums.WeaponCategory.Bite,
                onlyOnFirstAttack: true,
                triggerBeforeAttack: false,
                action: ActionsBuilder.New().
                CombatManeuverCustom(type: Kingmaker.RuleSystem.Rules.CombatManeuver.Grapple
                , failure: null
                , success: ActionsBuilder.New()

                    .Grapple
                        (
                            casterBuff: BuffRefs.TigerGrappledInitiatorBuff.Cast<BlueprintBuffReference>().Reference,//RakeExtraAttacksBuff.ToReference<BlueprintBuffReference>(),
                            targetBuff: BuffRefs.TigerGrappledTargetBuff.Cast<BlueprintBuffReference>().Reference

                        ))
                        .Build()

                ).Configure(delayed: true);



            BaseGrappleActivatableAbility = ActivatableAbilityConfigurator.New(AbilityName, AbilityGuid)
                .OnConfigure(bp =>
                {
                    bp.m_Buff = BaseGrappleBuff.ToReference<BlueprintBuffReference>();
                    bp.IsOnByDefault = true;
                    bp.m_ActivateOnUnitAction = Kingmaker.UnitLogic.ActivatableAbilities.AbilityActivateOnUnitActionType.Attack;
                    bp.DeactivateImmediately = true;
                    bp.ActivationType = Kingmaker.UnitLogic.ActivatableAbilities.AbilityActivationType.Immediately;
                    bp.m_ActivateWithUnitCommand = 0;

                }
                ).Configure(delayed: true);


            BaseGrappleFeature = FeatureConfigurator.New(FeatName, FeatGuid, FeatureGroup.Feat)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetIcon(Icon)
                .AddFacts(new List<Blueprint<BlueprintUnitFactReference>>{
                    BaseGrappleActivatableAbility.ToReference<BlueprintUnitFactReference>()
                    //,RakeExtraAttacksBuff.ToReference<BlueprintUnitFactReference>()
                })
                .Configure(delayed: true);

        }
    }
}
