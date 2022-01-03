using EvilEye.Events;
using EvilEye.SDK;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using VRC;

namespace EvilEye.Module.Settings
{
    class PlayerLogger : BaseModule, OnPlayerJoinEvent
    {
        
        public PlayerLogger() : base("Avatar Logger", "Logs Avatars In World", Main.Instance.settingsLogger, null, true, true)
        {
        }

        public override void OnEnable()
        {
            Main.Instance.onPlayerJoinEvents.Add(this);
        }

        public override void OnDisable()
        {
            Main.Instance.onPlayerJoinEvents.Remove(this);
        }

        public void OnPlayerJoin(Player player)
        {
            string playerInfo = $"PlayerName:{player.GetAPIUser().displayName}\n" +
                $"PlayerID:{player.GetAPIUser().id}\n" +
                $"SteamID:{player.GetSteamID()}\n" +
                $"HashTable:{player.GetPhotonPlayer().prop_Hashtable_0.ToString()}\n" +
                $"AvatarID:{player.GetAPIAvatar().id}\n" +
                $"AvatarUrl:{player.GetAPIAvatar().assetUrl}\n" +
                $"AvatarIhumbnailURL:{player.GetAPIAvatar().thumbnailImageUrl}\n";
            File.AppendAllText("EvilEye/Logger/Players.txt", playerInfo);
            LoggerUtill.Log(playerInfo, ConsoleColor.Cyan);
        }
    }
}
