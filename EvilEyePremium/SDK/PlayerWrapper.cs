using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VRC;
using VRC.Core;
using VRC.DataModel;
using VRC.SDKBase;
using VRC.UI;
using VRC.UI.Elements.Menus;

namespace EvilEye.SDK
{
    static class PlayerWrapper
    {
        //converted all the bs to one lines to clean the class
        public static Dictionary<int, VRC.Player> PlayersActorID = new Dictionary<int, VRC.Player>();
        public static Player[] GetAllPlayers() => PlayerManager.prop_PlayerManager_0.prop_ArrayOf_Player_0;
        public static Player GetByUsrID(string usrID) => GetAllPlayers().First(x => x.prop_APIUser_0.id == usrID);
        public static void Teleport(this Player player) => LocalVRCPlayer().transform.position = player.prop_VRCPlayer_0.transform.position;
        public static Player LocalPlayer() => Player.prop_Player_0;
        public static VRCPlayer LocalVRCPlayer() => VRCPlayer.field_Internal_Static_VRCPlayer_0;
        public static APIUser GetAPIUser(this VRC.Player player) => player.prop_APIUser_0;
        public static float GetFrames(this Player player) => (player._playerNet.prop_Byte_0 != 0) ? Mathf.Floor(1000f / (float)player._playerNet.prop_Byte_0) : -1f;
        public static short GetPing(this Player player) => player._playerNet.field_Private_Int16_0;
        public static bool IsBot(this Player player) => player.GetPing() <= 0 && player.GetFrames() <= 0 || player.transform.position == Vector3.zero;
        public static IUser GetSelectedUser(this SelectedUserMenuQM selectMenu) => selectMenu.field_Private_IUser_0;
        public static Player GetPlayer(this VRCPlayer player) => player.prop_Player_0;
        public static VRCPlayer GetVRCPlayer(this Player player) => player._vrcplayer;
        public static Color GetTrustColor(this VRC.Player player) => VRCPlayer.Method_Public_Static_Color_APIUser_0(player.GetAPIUser());
        public static APIUser GetAPIUser(this VRCPlayer Instance) => Instance.GetPlayer().GetAPIUser();
        public static VRCPlayerApi GetVRCPlayerApi(this Player Instance) => Instance?.prop_VRCPlayerApi_0;
        public static bool GetIsMaster(this Player Instance) => Instance.GetVRCPlayerApi().isMaster;
        public static int GetActorNumber(this Player player) => player.GetVRCPlayerApi() != null ? player.GetVRCPlayerApi().playerId : -1;
        public static void SetHide(this VRCPlayer Instance, bool State) => Instance.GetPlayer().SetHide(State);
        public static void SetHide(this Player Instance, bool State) => Instance.transform.Find("ForwardDirection").gameObject.active = !State;
        public static USpeaker GetUspeaker(this Player player) => player.prop_USpeaker_0;
        public static ulong GetSteamID(this Player player) => (player.GetVRCPlayer().field_Private_UInt64_0 > 10000000000000000UL) ? player.GetVRCPlayer().field_Private_UInt64_0 : ulong.Parse(player.GetPhotonPlayer().prop_Hashtable_0["steamUserID"].ToString());
        public static Photon.Realtime.Player GetPhotonPlayer(this Player player) => player.prop_Player_1;
        public static bool ClientDetect(this Player player) => player.GetFrames() > 90 || player.GetFrames() < 1 || player.GetPing() > 665 || player.GetPing() < 0;
        public static ApiAvatar GetAPIAvatar(this VRCPlayer vrcPlayer) => vrcPlayer.prop_ApiAvatar_0;
        public static ApiAvatar GetAPIAvatar(this Player player) => player.GetVRCPlayer().GetAPIAvatar();
        public static void Tele2MousePos()
        {
            Ray posF = new Ray(Camera.main.transform.position, Camera.main.transform.forward); //pos, directon 
            RaycastHit[] PosData = Physics.RaycastAll(posF);
            if (PosData.Length > 0) { RaycastHit pos = PosData[0]; VRCPlayer.field_Internal_Static_VRCPlayer_0.transform.position = pos.point; }
        }

        public static string GetFramesColord(this Player player)
        {
            float fps = player.GetFrames();
            if (fps > 80)
                return "<color=green>" + fps + "</color>";
            else if (fps > 30)
                return "<color=yellow>" + fps + "</color>";
            else
                return "<color=red>" + fps + "</color>";
        }

        public static string GetPingColord(this Player player)
        {
            short ping = player.GetPing();
            if (ping > 150)
                return "<color=red>" + ping + "</color>";
            else if (ping > 75)
                return "<color=yellow>" + ping + "</color>";
            else
                return "<color=green>" + ping + "</color>";
        }

        public static string GetPlatform(this Player player)
        {
            if (player.GetAPIUser().IsOnMobile)
            {
                return "<color=green>Q</color>";
            }
            else if (player.GetVRCPlayerApi().IsUserInVR())
            {
                return "<color=#CE00D5>V</color>";
            }
            else
            {
                return "<color=grey>PC</color>";
            }
        }

        public static void DelegateSafeInvoke(this Delegate @delegate, params object[] args)
        {
            Delegate[] invocationList = @delegate.GetInvocationList();
            for (int i = 0; i < invocationList.Length; i++)
            {
                try
                {
                    invocationList[i].DynamicInvoke(args);
                }
                catch (Exception ex)
                {
                    LoggerUtill.Log("Error while executing delegate:\n" + ex.ToString(), ConsoleColor.Red);
                }
            }
        }

        public static void ChangeAvatar(string AvatarID)
        {
            PageAvatar component = GameObject.Find("Screens").transform.Find("Avatar").GetComponent<PageAvatar>();
            component.field_Public_SimpleAvatarPedestal_0.field_Internal_ApiAvatar_0 = new ApiAvatar
            {
                id = AvatarID
            };
            component.ChangeToSelectedAvatar();
        }

        public static Player GetPlayerByActorID(int actorId)
        {
            VRC.Player player = null;
            PlayersActorID.TryGetValue(actorId, out player);
            return player;
        }
    }
}
