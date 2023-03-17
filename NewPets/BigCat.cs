using BlueprintCore.Blueprints.Configurators;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using Kingmaker.AI.Blueprints;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Enums;
using Kingmaker.ResourceLinks;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.Visual.HitSystem;
using Kingmaker.Visual.Sound;
using System.Collections.Generic;

namespace SmolCraft.NewPets
{
    class BigCat
    {
        public static BlueprintFeature LionUpdateFeature;
        public static BlueprintUnit SmolCraftLionUnit;







        public static void Configure()
        {


            var Neutrals = ResourcesLibrary.TryGetBlueprint<BlueprintFaction>("d8de50cc80eb4dc409a983991e0b77ad");
            var AzataDragonUnit = ResourcesLibrary.TryGetBlueprint<BlueprintUnit>("32a037e97c3d5c54b85da8f639616c57");
            var CharacterBrain = ResourcesLibrary.TryGetBlueprint<BlueprintBrain>("cf986dd7ba9d4ec46ad8a3a0406d02ae");


            var MediumLionFeature = FeatureConfigurator.New("MediumLionFeature", "12cb49b4-79a9-4c6f-b5b1-64ce675e3003")
                .AddChangeUnitSize(type: Kingmaker.Designers.Mechanics.Buffs.ChangeUnitSize.ChangeType.Delta, size: Kingmaker.Enums.Size.Fine, sizeDelta: 0)
                .AddFacts(new List<Blueprint<BlueprintUnitFactReference>> { UnitFactRefs.NaturalArmor4.Cast<BlueprintUnitFactReference>().Reference })
                .SetHideInUI(true)
                .SetHideInCharacterSheetAndLevelUp(true)
                .Configure();

            var LionUpgradeFeature = FeatureConfigurator.New("LionUpgradeFeature", "12cb49b4-79a9-4c6f-b5b1-64ce675e3001")
                .AddChangeUnitSize(type: Kingmaker.Designers.Mechanics.Buffs.ChangeUnitSize.ChangeType.Delta, size: Kingmaker.Enums.Size.Fine, sizeDelta: 1)
                .AddStatBonus(Kingmaker.Enums.ModifierDescriptor.None, stat: Kingmaker.EntitySystem.Stats.StatType.Strength, value: 8)
                .AddStatBonus(Kingmaker.Enums.ModifierDescriptor.None, stat: Kingmaker.EntitySystem.Stats.StatType.Dexterity, value: -2)
                .AddStatBonus(Kingmaker.Enums.ModifierDescriptor.None, stat: Kingmaker.EntitySystem.Stats.StatType.Constitution, value: 4)
                .AddFacts(new List<Blueprint<BlueprintUnitFactReference>>
                {
                    UnitFactRefs.NaturalArmor2.Cast<BlueprintUnitFactReference>().Reference
                })
                .SetHideInUI(true)
                .SetHideInCharacterSheetAndLevelUp(true)
                .Configure();

            LionUpdateFeature = FeatureConfigurator.New("LionUpdateFeature", "12cb49b4-79a9-4c6f-b5b1-64ce675e3002")
                .AddChangeUnitSize(type: Kingmaker.Designers.Mechanics.Buffs.ChangeUnitSize.ChangeType.Delta, size: Kingmaker.Enums.Size.Fine, sizeDelta: 0)
                .AddFeatureOnClassLevel(level: 7, beforeThisLevel: false, feature: LionUpgradeFeature, clazz: CharacterClassRefs.AnimalCompanionClass.Reference.GetBlueprint())
                .AddFeatureOnClassLevel(level: 7, beforeThisLevel: true, feature: MediumLionFeature, clazz: CharacterClassRefs.AnimalCompanionClass.Reference.GetBlueprint())
                .SetHideInUI(true)
                .SetHideInCharacterSheetAndLevelUp(true)
                .Configure();


            var LionBody = new BlueprintUnit.UnitBody()
            {
                DisableHands = true,
                m_EmptyHandWeapon = ItemWeaponRefs.WeaponEmptyHand.Cast<BlueprintItemWeaponReference>().Reference,
                m_PrimaryHand = ItemWeaponRefs.Claw1d4.Cast<BlueprintItemEquipmentHandReference>().Reference,
                m_SecondaryHand = ItemWeaponRefs.Claw1d4.Cast<BlueprintItemEquipmentHandReference>().Reference,
                m_PrimaryHandAlternative1 = ItemWeaponRefs.Claw1d4.Cast<BlueprintItemEquipmentHandReference>().Reference,
                m_SecondaryHandAlternative1 = ItemWeaponRefs.Claw1d4.Cast<BlueprintItemEquipmentHandReference>().Reference,
                m_PrimaryHandAlternative2 = ItemWeaponRefs.Claw1d4.Cast<BlueprintItemEquipmentHandReference>().Reference,
                m_SecondaryHandAlternative2 = ItemWeaponRefs.Claw1d4.Cast<BlueprintItemEquipmentHandReference>().Reference,
                m_PrimaryHandAlternative3 = ItemWeaponRefs.Claw1d4.Cast<BlueprintItemEquipmentHandReference>().Reference,
                m_SecondaryHandAlternative3 = ItemWeaponRefs.Claw1d4.Cast<BlueprintItemEquipmentHandReference>().Reference,
                m_AdditionalLimbs = new BlueprintItemWeaponReference[]
                {
                    ItemWeaponRefs.Bite1d6.Cast<BlueprintItemWeaponReference>().Reference
                },
            };

            //var UnitViewLinks = "14b0730dd2c0a684f89f2982bf1035cd";
            //var UnitEntityView = new UnitViewLink { AssetId = UnitViewLinks};
            //var prefab = UnityEngine.Object.Instantiate<UnityEngine.Object>(UnitEntityView.LoadObject());
            //var uevprefab = (UnitEntityView)prefab;
            //var comp = uevprefab.GetComponentsInChildren<SkinnedMeshRenderer>();
            //comp[0].material.mainTexture=

            SmolCraftLionUnit = UnitConfigurator.New("LionUnit", "12cb49b4-79a9-4c6f-b5b1-64ce675e3000")
            .CopyFrom(UnitRefs.AnimalCompanionUnitSmilodon, typeof(AllowDyingCondition), typeof(AddResurrectOnRest), typeof(LockEquipmentSlot), typeof(AddClassLevels))
            .SetDisplayName("Lion.Name")
            .SetDescription("Lion.Description")
            .SetGender(Kingmaker.Blueprints.Gender.Male)
            .SetSize(Kingmaker.Enums.Size.Medium)
            .SetColor(UnityEngine.Color.white)
            .SetAlignment(Kingmaker.Enums.Alignment.TrueNeutral)
            .SetFaction(Neutrals)
            .SetFactionOverrides(AzataDragonUnit.FactionOverrides)
            .SetBrain(CharacterBrain)
            .SetVisual(new UnitVisualParams()
            {
                BloodType = BloodType.Common,
                FootprintType = FootprintType.AnimalPaw,
                FootprintScale = 1,
                ArmorFx = new PrefabLink(),
                BloodPuddleFx = new PrefabLink(),
                DismemberFx = new PrefabLink(),
                RipLimbsApartFx = new PrefabLink(),
                IsNotUseDismember = false,
                m_Barks = BlueprintTool.GetRef<BlueprintUnitAsksListReference>("f3397a5e218472c4ab7b5ef00ed07654"),

                ReachFXThresholdBonus = 0,
                DefaultArmorSoundType = ArmorSoundType.Flesh,
                FootstepSoundSizeType = FootstepSoundSizeType.BootMedium,
                FootSoundType = FootSoundType.SoftPaw,
                FootSoundSize = Size.Medium,
                BodySoundType = BodySoundType.Flesh,
                BodySoundSize = Size.Medium,
                FoleySoundPrefix = null, //?
                NoFinishingBlow = false,
                ImportanceOverride = 0,
                SilentCaster = true
            })
            .SetPortrait(BlueprintTool.GetRef<BlueprintPortraitReference>("b3712a85095646141b2d43129d19983e")
            )
            .SetPrefab(


            "14b0730dd2c0a684f89f2982bf1035cd")
            .SetBody(LionBody)
            .SetStrength(13)
            .SetDexterity(17)
            .SetConstitution(13)
            .SetIntelligence(2)
            .SetWisdom(15)
            .SetCharisma(10)
            .SetSkills(new BlueprintUnit.UnitSkills
            {
                Acrobatics = 0,
                Physique = 0,
                Diplomacy = 0,
                Thievery = 0,
                LoreNature = 0,
                Perception = 0,
                Stealth = 0,
                UseMagicDevice = 0,
                LoreReligion = 0,
                KnowledgeWorld = 0,
                KnowledgeArcana = 0,
            })
            .SetMaxHP(0)
            .SetSize(Kingmaker.Enums.Size.Medium)
            .SetAddFacts(FeatureRefs.TripDefenseFourLegs.Cast<BlueprintUnitFactReference>().Reference, LionUpdateFeature.ToReference<BlueprintUnitFactReference>())
            .Configure();

            var CompanionLionFeature = FeatureConfigurator.New("CompanionLionFeature", "12cb49b4-79a9-4c6f-b5b1-64ce675e3004", FeatureGroup.AnimalCompanion)
            .SetDisplayName("Lion.Name")
            .SetDescription("Lion.Description")
            .AddPet(type: PetType.AnimalCompanion, progressionType: PetProgressionType.AnimalCompanion, pet: SmolCraftLionUnit, levelRank: FeatureRefs.AnimalCompanionRank.Cast<BlueprintFeatureReference>())
            .AddPrerequisitePet(noCompanion: true, type: PetType.AnimalCompanion, hideInUI: false)
            .SetAllowNonContextActions(false)
            .SetRanks(1)
            .SetReapplyOnLevelUp(true)
            .SetIsClassFeature(true)
            .Configure();
        }
    }
}
