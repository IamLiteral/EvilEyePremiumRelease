using EvilEye.Events;
using EvilEye.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace EvilEye.Module.Settings
{
    class JoinLogger : BaseModule, OnPlayerJoinEvent, OnPlayerLeaveEvent
    {
        public JoinLogger() : base("Join/Leave Log", "Logs Players Joining And Leaving", Main.Instance.settingsLogger, null, true, true)
        {
        }

        public override void OnEnable()
        {
            Main.Instance.onPlayerJoinEvents.Add(this);
            Main.Instance.onPlayerLeaveEvents.Add(this);
        }

        public override void OnDisable()
        {
            Main.Instance.onPlayerJoinEvents.Remove(this);
            Main.Instance.onPlayerLeaveEvents.Remove(this);
        }

        void OnPlayerJoinEvent.OnPlayerJoin(VRC.Player player)
        {
            LoggerUtill.Log($"Player Joined ~> Username: {player.prop_APIUser_0.displayName} | Photon ID: {player.prop_VRCPlayerApi_0.playerId} | UserID: {player.prop_APIUser_0.id}", ConsoleColor.Cyan);
            LoggerUtill.LogDebug($"[PLAYER] <color=#{ColorUtility.ToHtmlStringRGB(player.GetTrustColor())}>{player.prop_APIUser_0.displayName}</color> : <color=green><i>JOIN</i></color>");
        }

        public void PlayerLeave(VRC.Player player)
        {
            LoggerUtill.Log($"Player Left ~> Username: {player.prop_APIUser_0.displayName} | UserID: {player.prop_APIUser_0.id}");
            LoggerUtill.LogDebug($"[PLAYER] <color=#{ColorUtility.ToHtmlStringRGB(player.GetTrustColor())}>{player.prop_APIUser_0.displayName}</color> : <color=red><i>Left</i></color>");
        }
    }
}
