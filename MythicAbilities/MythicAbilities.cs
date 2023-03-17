using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Utils.Types;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.ElementsSystem;
using Kingmaker.Enums.Damage;
using Kingmaker.RuleSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.Components;
using System.Collections.Generic;
using static Kingmaker.UnitLogic.Mechanics.Components.AdditionalDiceOnAttack;
using static SmolCraft.NewMechanics.NewComponents;

namespace SmolCraft.MythicAbilities
{
  /// <summary>
  /// Creates a feat that does nothing but show up.
  /// </summary>
  public class MythicAbilityMundaneBeating
  {
    private static readonly string FeatName = "MundaneBeating";
    private static readonly string FeatGuid = "12cb49b4-79a9-4c6f-b5b1-64ce675e2000";

    private static readonly string DisplayName = "MundaneBeating.Name";
    private static readonly string Description = "MundaneBeating.Description";
    private static readonly string Icon = "assets/icons/quillen.jpg";
    
    public static void Configure()
    {

            FeatureConfigurator
                .New(FeatName, FeatGuid, FeatureGroup.MythicAbility)
                .AddComponent<MundaneBeatingComponent>()
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetIcon(Icon)
                .Configure(delayed: true);
    }
  }


    //public class MythicAbilityUniversalWeapon
    //{
    //    private static readonly string FeatName = "UniversalWeapon";
    //    private static readonly string FeatGuid = "12cb49b4-79a9-4c6f-b5b1-64ce675e20bb";

    //    private static readonly string DisplayName = "UniversalWeapon.Name";
    //    private static readonly string Description = "UniversalWeapon.Description";
    //    private static readonly string Icon = "assets/icons/quillen.jpg";

    //    public static void Configure()
    //    {
    //        FeatureConfigurator.New(FeatName, FeatGuid, FeatureGroup.MythicAbility)
    //          .SetDisplayName(DisplayName)
    //          .SetDescription(Description)
    //          .SetIcon(Icon)
    //          .Configure(delayed: true);
    //    }
    //}
}

