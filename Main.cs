using BlueprintCore.Blueprints.Configurators.Root;
using BlueprintCore.Utils;
using HarmonyLib;
using Kingmaker.Blueprints.JsonSystem;
using System;
using UnityEngine.SceneManagement;
using UnityModManagerNet;

namespace SmolCraft
{
  public static class Main
  {
    public static bool Enabled;
        internal static bool loaded;
        private static readonly LogWrapper Logger = LogWrapper.Get("SmolCraft");
        public static UnityModManager.ModEntry ModEntry;

    public static bool Load(UnityModManager.ModEntry modEntry)
    {
      try
      {
        modEntry.OnToggle = OnToggle;
        var harmony = new Harmony(modEntry.Info.Id);
               // SceneManager.sceneLoaded += Patches.BigCatTexturePatch.CreateLookup;
                harmony.PatchAll();
        Logger.Info("Finished patching.");
        
      }
      catch (Exception e)
      {
        Logger.Error("Failed to patch", e);
      }
      return true;
    }

    public static bool OnToggle(UnityModManager.ModEntry modEntry, bool value)
    {
            ModEntry = modEntry;
      Enabled = value;

      return true;
    }

    [HarmonyPatch(typeof(BlueprintsCache))]
    static class BlueprintsCaches_Patch
    {
      private static bool Initialized = false;

      [HarmonyPriority(Priority.Last)]
      [HarmonyPatch(nameof(BlueprintsCache.Init)), HarmonyPostfix]
      static void Init()
        {
            try
            {
                if (Initialized)
                    {
                    Logger.Info("Already configured blueprints.");
                    return;
                    }
                Initialized = true;

                Logger.Info("Configuring blueprints.");

                    MythicAbilities.MythicAbilityMundaneBeating.Configure();
                    //Patches.SmilodonRakePatch.Configure();
                   // NewAbilities.GrappleAbility.Configure();
                    NewPets.BigCat.Configure();
                    




                }
            catch (Exception e)
            {
                Logger.Error("Failed to configure blueprints.", e);
            }
      }
    }

    [HarmonyPatch(typeof(StartGameLoader))]
    static class StartGameLoader_Patch
    {
      private static bool Initialized = false;

      [HarmonyPatch(nameof(StartGameLoader.LoadPackTOC)), HarmonyPostfix]
      static void LoadPackTOC()
      {
        try
        {
          if (Initialized)
          {
                        
                        Logger.Info("Already configured delayed blueprints.");
                        
                        return;
          }
          Initialized = true;

          RootConfigurator.ConfigureDelayedBlueprints();
        }
        catch (Exception e)
        {
          Logger.Error("Failed to configure delayed blueprints.", e);
        }
      }
    }
  }
}

