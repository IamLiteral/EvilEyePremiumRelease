using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using VRC.SDKBase;
using EvilEye.SDK;
using VRC.Networking;
using VRC.Udon;
using VRC.Core;

namespace EvilEye.SDK
{
    class WorldWrapper
    {
        public static VRC_Pickup[] vrc_Pickups;
        public static UdonBehaviour[] udonBehaviours;
        public static VRC_Trigger[] vrc_Triggers;
        public static string GetInstance() => CurrentWorldInstance().instanceId; 
        public static string GetID() => CurrentWorld().id;
        public static string GetLocation() => PlayerWrapper.LocalPlayer().GetAPIUser().location;
        public static ApiWorld CurrentWorld() => RoomManager.field_Internal_Static_ApiWorld_0;
        public static ApiWorldInstance CurrentWorldInstance() => RoomManager.field_Internal_Static_ApiWorldInstance_0;

        public static void Init()
        {
            vrc_Pickups = UnityEngine.Object.FindObjectsOfType<VRC_Pickup>();
            udonBehaviours = UnityEngine.Object.FindObjectsOfType<UdonBehaviour>();
            vrc_Triggers = UnityEngine.Object.FindObjectsOfType<VRC_Trigger>();
            PlayerWrapper.PlayersActorID = new Dictionary<int, VRC.Player>();
            for (int i = 0; i < Main.Instance.onWorldInitEventArray.Length; i++)
                Main.Instance.onWorldInitEventArray[i].OnWorldInit();
        }
    }
}
