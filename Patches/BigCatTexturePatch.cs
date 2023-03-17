//using HarmonyLib;
//using Kingmaker.Blueprints;
//using Kingmaker.EntitySystem.Entities;
//using Kingmaker.View;
//using Kingmaker.Visual.CharacterSystem;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using UnityEngine;
//using UnityEngine.SceneManagement;
//using static Kingmaker.UI.CanvasScalerWorkaround;
//using UnityEngine;
//using UnityModManagerNet;
//using static UnityModManagerNet.UnityModManager;
//using System;
//using System.Reflection;
//using System.IO;
//using Kingmaker.Visual.CharacterSystem;
//using Kingmaker.ResourceLinks;
//using System.Collections.Generic;
//using Kingmaker.Blueprints;
//using Kingmaker.Blueprints.Items.Weapons;
//using Kingmaker.Blueprints.Items.Equipment;
//using Newtonsoft.Json;
//using Newtonsoft.Json.Converters;
//using System.Linq;
//using Kingmaker.Blueprints.Classes;
//using UnityEngine.SceneManagement;
//using Kingmaker;
//using UnityEngine.UI;
//using BlueprintCore.Utils;

//namespace SmolCraft.Patches
//{
//    class BigCatTexturePatch
//    {
//        public static HashSet<String> uevLookupUnit = new HashSet<string>();
//        static Dictionary<string, Texture2D> textureLookup = new Dictionary<string, Texture2D>();

//        public static Texture2D LoadTexture(string filePath)
//        {
//            if (textureLookup.ContainsKey(filePath))
//            {
//                return textureLookup[filePath];
//            }
//            else
//            {
//                var fileData = File.ReadAllBytes(filePath);
//                var texture = new Texture2D(1024, 1024);
//                texture.LoadRawTextureData(fileData);
//                //texture.LoadImage(fileData);
//                textureLookup[filePath] = texture;
//                return texture;
//            }
//        }

//        public static class Access
//        {

//            public static HarmonyLib.AccessTools.FieldRef<Material, Texture2D> mainTexture;
//            public static void Init()
//            {
//                mainTexture = HarmonyLib.AccessTools.FieldRefAccess<Material, Texture2D>("mainTexture");
//            }
//        }

//        public static void CreateLookup(Scene scene, LoadSceneMode mode)
//        {
//                foreach (BlueprintUnit Unit in GetAllMyUnits())
//                {
//                    //BpCore.Log(Unit.Name);
//                    AddUnit(Unit);
//                }

//        }

//        private static void AddUnit(BlueprintUnit unit)
//        {
//            uevLookupUnit.Add(unit.Prefab.AssetId);
//        }

//        private static BlueprintUnit[] GetAllMyUnits()
//        {
//            return new BlueprintUnit[]
//            {
//                    ResourcesLibrary.TryGetBlueprint<BlueprintUnit>(NewPets.BigCat.SmolCraftLionUnit.AssetGuid.ToString())

//            };

//            // Game.Instance.BlueprintRoot.Progression.CharacterRaces;
//        }

//        static bool IsLion(string assetId)
//        {
//            return uevLookupUnit.Contains(assetId);

//        }


//        [HarmonyPatch(typeof(AssetBundle), "LoadFromFile", new Type[] { typeof(string) })]//MethodType.Getter)]
//        public static class BigCatTexturePatchConfiguration
//        {
         
//            static void Postfix(
//                //ref UnitEntityView __result,
//                ref AssetBundle __result, string path)
//            {
              
//                try
//                {
//                    //var assetId = Path.GetFileName(path).Replace("resource_", "");
//                    var assetId = Path.GetFileName(path).Replace("resource_", "");

                   
//                    if (IsLion(assetId))
//                    {
//                        var UEV = __result.LoadAllAssets<UnitEntityView>()[0];
//                        if (UEV.EntityData.Blueprint.name.Contains("SmolCraftLion"))
//                        {
//                            UEV.GetComponentInChildren<SkinnedMeshRenderer>().material.mainTexture = LoadTexture(Main.ModEntry.Path
//                                + @"\NewTextures\SmolCraftLion_d.png");

//                        };

//                    }
                     
                    

//                }
//                catch (Exception ex)
//                {
//                    ;
//                }
//            }
//        }
//    }
    
//}
