using AmplitudeSDKWrapper;
using EvilEye.Events;
using EvilEye.Module;
using EvilEye.Module.Settings;
using EvilEye.SDK;
using ExitGames.Client.Photon;
using HarmonyLib;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VRC;
using VRC.Core;
using Transmtn;
using UnityEngine;
using UnityEngine.Networking;
using VRC.SDKBase;
using MelonLoader;
using VRC.Networking; 
namespace EvilEye
{
    class Patches 
    { 
        private static readonly HarmonyLib.Harmony Instance = new HarmonyLib.Harmony("EvilEye");
        private static string newHWID = "";
        public static List<int> blacklistedPlayers = new List<int>();

        public static void Init()
        {
            
            try
            {
                Instance.Patch(typeof(SystemInfo).GetProperty("deviceUniqueIdentifier").GetGetMethod(), new HarmonyMethod(AccessTools.Method(typeof(Patches), nameof(FakeHWID))));
                Instance.Patch(typeof(AmplitudeWrapper).GetMethod("PostEvents"), new HarmonyMethod(AccessTools.Method(typeof(Patches), nameof(VoidPatch))));
                Instance.Patch(typeof(AmplitudeWrapper).GetMethods().First((MethodInfo x) => x.Name == "LogEvent" && x.GetParameters().Length == 4), new HarmonyMethod(AccessTools.Method(typeof(Patches), nameof(VoidPatch))));
                LoggerUtill.Log("[Patch] Patche Analystics", ConsoleColor.Green);
            }
            catch(Exception ex)
            {
                LoggerUtill.Log($"[Patch] [Error] Analystics\n{ex}", ConsoleColor.Red);
            }

            try
            {
                Instance.Patch(typeof(LoadBalancingClient).GetMethod("OnEvent"), new HarmonyMethod(AccessTools.Method(typeof(Patches), nameof(OnEventPatch))));
                Instance.Patch(typeof(VRC_EventDispatcherRFC).GetMethod("Method_Public_Void_Player_VrcEvent_VrcBroadcastType_Int32_Single_0"), new HarmonyMethod(AccessTools.Method(typeof(Patches), nameof(OnRPC))));
                Instance.Patch(AccessTools.Method(typeof(LoadBalancingClient), "Method_Public_Virtual_New_Boolean_Byte_Object_RaiseEventOptions_SendOptions_0", null, null), new HarmonyMethod(AccessTools.Method(typeof(Patches), nameof(OpRaiseEvent))));
                Instance.Patch(typeof(UdonSync).GetMethod("UdonSyncRunProgramAsRPC"), new HarmonyMethod(AccessTools.Method(typeof(Patches), nameof(OnUdon))));
                LoggerUtill.Log("[Patch] Networking", ConsoleColor.Green);
            }
            catch (Exception ex)
            {
                LoggerUtill.Log($"[Patch] [Error] Networking\n{ex}", ConsoleColor.Red);
            }

            try
            {
                Instance.Patch(typeof(VRC.Core.AssetManagement).GetMethod("Method_Public_Static_Object_Object_Boolean_Boolean_Boolean_0"), new HarmonyMethod(AccessTools.Method(typeof(Patches), nameof(OnAvatarAssetBundleLoad))));

                LoggerUtill.Log("[Patch] Avatar", ConsoleColor.Green);
            }
            catch (Exception ex)
            {
                LoggerUtill.Log($"[Patch] [Error] Avatar\n{ex}", ConsoleColor.Red);
            }

            try
            {
                Instance.Patch(typeof(VRC.UI.Elements.QuickMenu).GetMethod("Awake"), new HarmonyMethod(AccessTools.Method(typeof(Main), nameof(Main.OnUIInit))));

                LoggerUtill.Log("[Patch] UI", ConsoleColor.Green);
            }
            catch (Exception ex)
            {
                LoggerUtill.Log($"[Patch] [Error] UI\n{ex}", ConsoleColor.Red);
            }

            try
            {
                Instance.Patch(typeof(Debug).GetMethods().First(x => x.Name == "Log" && x.GetParameters().Length == 1), new HarmonyMethod(AccessTools.Method(typeof(Patches), nameof(Debug))));
                Instance.Patch(typeof(Debug).GetMethods().First(x => x.Name == "LogError" && x.GetParameters().Length == 1), new HarmonyMethod(AccessTools.Method(typeof(Patches), nameof(DebugError))));
                Instance.Patch(typeof(Debug).GetMethods().First(x => x.Name == "LogWarning" && x.GetParameters().Length == 1), new HarmonyMethod(AccessTools.Method(typeof(Patches), nameof(DebugWarning))));

                LoggerUtill.Log("[Patch] Logger", ConsoleColor.Green);
            }
            catch (Exception ex)
            {
                LoggerUtill.Log($"[Patch] [Error] Logger\n{ex}", ConsoleColor.Red);
            }

            while (NetworkManager.field_Internal_Static_NetworkManager_0 == null)
            {
                Thread.Sleep(25);
            }

            VRCEventDelegate<VRC.Player> field_Internal_VRCEventDelegate_1_Player_ = NetworkManager.field_Internal_Static_NetworkManager_0.field_Internal_VRCEventDelegate_1_Player_0;
            VRCEventDelegate<VRC.Player> field_Internal_VRCEventDelegate_1_Player_2 = NetworkManager.field_Internal_Static_NetworkManager_0.field_Internal_VRCEventDelegate_1_Player_1;
            field_Internal_VRCEventDelegate_1_Player_.field_Private_HashSet_1_UnityAction_1_T_0.Add(new Action<VRC.Player>(Patches.OnPlayerJoin));
            field_Internal_VRCEventDelegate_1_Player_2.field_Private_HashSet_1_UnityAction_1_T_0.Add(new Action<VRC.Player>(Patches.OnPlayerLeave));

            LoggerUtill.Log("[Patch] All Patching Procedures Are Complete, Now Starting Client", ConsoleColor.Green);
        } 

        private static void OnPlayerJoin(VRC.Player player)
        {
            if (player == null)
            {
                return;
            }
            if (player == PlayerWrapper.LocalPlayer())
                WorldWrapper.Init();
            for (int i = 0; i < Main.Instance.onPlayerJoinEventArray.Length; i++)
                Main.Instance.onPlayerJoinEventArray[i].OnPlayerJoin(player);
            PlayerWrapper.PlayersActorID.Add(player.GetActorNumber(), player);
        }

        private static void OnPlayerLeave(VRC.Player player)
        {
            if (player == null)
            {
                return;
            }
            for (int i = 0; i < Main.Instance.onPlayerLeaveEventArray.Length; i++)
                Main.Instance.onPlayerLeaveEventArray[i].PlayerLeave(player);
            PlayerWrapper.PlayersActorID.Remove(player.GetActorNumber());
        }

        private static bool DebugError(ref Il2CppSystem.Object __0)
        {
            if (UnityLogger.Instance == null)
                return true;

            if (UnityLogger.Instance.toggled)
                LoggerUtill.Log("[UnityError] " + Il2CppSystem.Convert.ToString(__0));
            return true;
        }

        private static bool OnAvatarAssetBundleLoad(ref UnityEngine.Object __0)
        {
            GameObject gameObject = __0.TryCast<GameObject>();
            if (gameObject == null)
            {
                return true;
            }
            if (!gameObject.name.ToLower().Contains("avatar"))
            {
                return true;
            }
            string avatarId = gameObject.GetComponent<PipelineManager>().blueprintId;
            for (int i = 0; i < Main.Instance.OnAssetBundleLoadEventArray.Length; i++)
                if (!Main.Instance.OnAssetBundleLoadEventArray[i].OnAvatarAssetBundleLoad(gameObject, avatarId))
                    return false;
            
            return true;
        }

        private static bool DebugWarning(ref Il2CppSystem.Object __0)
        {
            if (UnityLogger.Instance == null)
                return true;

            if (UnityLogger.Instance.toggled)
                LoggerUtill.Log("[UnityWarning] " + Il2CppSystem.Convert.ToString(__0));
            return true;
        }

        private static bool Debug(ref Il2CppSystem.Object __0)
        {
            if (UnityLogger.Instance == null)
                return true;

            if (UnityLogger.Instance.toggled)
                LoggerUtill.Log("[Unity] " + Il2CppSystem.Convert.ToString(__0));
            return true;
        }

        private static bool OnUdon(string __0, VRC.Player __1)
        {
            for (int i = 0; i < Main.Instance.onUdonEventArray.Length; i++)
                if (!Main.Instance.onUdonEventArray[i].OnUdon(__0, __1))
                    return false;
            return true;
        }

        private static bool FakeHWID(ref string __result)
        {
            if (Patches.newHWID == "")
            {
                Patches.newHWID = KeyedHashAlgorithm.Create().ComputeHash(Encoding.UTF8.GetBytes(string.Format("{0}A-{1}{2}-{3}{4}-{5}{6}-3C-1F", new object[]
                {
                    new System.Random().Next(0, 9),
                    new System.Random().Next(0, 9),
                    new System.Random().Next(0, 9),
                    new System.Random().Next(0, 9),
                    new System.Random().Next(0, 9),
                    new System.Random().Next(0, 9),
                    new System.Random().Next(0, 9)
                }))).Select(delegate (byte x)
                {
                    byte b = x;
                    return b.ToString("x2");
                }).Aggregate((string x, string y) => x + y);
                LoggerUtill.Log("[HWID] new " + Patches.newHWID);
            }
            __result = Patches.newHWID;
            return false;
        } 

        private static bool OnEventPatch(EventData __0)
        {
            if (__0 == null)
                return false;
            if (blacklistedPlayers.Contains(__0.Sender))
                return false;
            
            for (int i = 0; i < Main.Instance.onEventEventArray.Length; i++)
                if (!Main.Instance.onEventEventArray[i].OnEvent(__0))
                    return false;
            return true;
        }

        private static bool OpRaiseEvent(byte __0, ref Il2CppSystem.Object __1, ref RaiseEventOptions __2)
        {
            for (int i = 0; i < Main.Instance.onSendOPEventArray.Length; i++)
                if (!Main.Instance.onSendOPEventArray[i].OnSendOP(__0, ref __1, ref __2))
                    return false;

            return true;
        }

        private static bool OnRPC(VRC.Player __0, VRC_EventHandler.VrcEvent __1, VRC_EventHandler.VrcBroadcastType __2, int __3, float __4)
        {
            for (int i = 0; i < Main.Instance.onRPCEventArray.Length; i++)
                if (!Main.Instance.onRPCEventArray[i].OnRPC(__0, __1, __2, __3, __4))
                    return false;

            return true;
        }

        private static bool VoidPatch()
        {
            return false;
        }

        private static bool VoidPatchTrue(bool __result)
        {
            __result = true;
            return false;
        }

        private static bool VoidPatchFalse(bool __result)
        {
            __result = false;
            return false;
        }
    }
}
