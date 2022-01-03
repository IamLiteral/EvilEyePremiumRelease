using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvilEye.Events;
using EvilEye.SDK;
using VRC;
using VRC.SDKBase;

namespace EvilEye.Module.Safety
{
    internal class AntiInvalidRpc : BaseModule, OnRPCEvent
    {
        public AntiInvalidRpc() : base("AntiInvalidRPC", "Anti for all RPC Exploits", Main.Instance.safetyPhotonGroup, null, true, true)
        {
        }

        public override void OnEnable()
        {
            Main.Instance.onRPCEvents.Add(this);
        }

        public override void OnDisable()
        {
            Main.Instance.onRPCEvents.Remove(this);
        }

        public bool OnRPC(Player sender, VRC_EventHandler.VrcEvent vrcEvent, VRC_EventHandler.VrcBroadcastType vrcBroadcastType, int instagatorId, float fastforward)
        {
            if(sender == null)
            {
                LoggerUtill.Log("Blocked RPC fron INVISABLE!", ConsoleColor.Red, true);
                return false;
            }

            if (vrcEvent.Name.Length > 50 || vrcEvent.ParameterString.Length > 50 || vrcEvent.Name.Any(c => c > 255) || vrcEvent.ParameterString.Any(c => c > 255))
            {
                LoggerUtill.Log(sender.prop_APIUser_0.displayName + " sended invalid " + vrcEvent.EventType,ConsoleColor.Red, true);
                return false;
            }
            if (vrcEvent.EventType == VRC_EventHandler.VrcEventType.SendRPC)
            {
                var objects = Networking.DecodeParameters(vrcEvent.ParameterBytes);

                if (objects.Length < 0)
                {
                    LoggerUtill.Log(sender.prop_APIUser_0.displayName + " sended invalid " + vrcEvent.EventType, ConsoleColor.Red, true);
                    return false;
                }
                if (vrcEvent.ParameterString == "SpawnEmojiRPC" || vrcEvent.ParameterString == "PlayEmoteRPC")
                {
                    if (objects.Length > 1)
                    {
                        LoggerUtill.Log(sender.prop_APIUser_0.displayName + " sended invalid " + vrcEvent.EventType, ConsoleColor.Red, true);
                        return false;
                    }
                    if (objects[0].Unbox<int>() < 0 || objects[0].Unbox<int>() > 30)
                    {
                        LoggerUtill.Log(sender.prop_APIUser_0.displayName + " sended invalid " + vrcEvent.EventType, ConsoleColor.Red, true);
                        return false;
                    }
                }else if (vrcEvent.ParameterObject.name.Contains("Uspeak"))
                {
                    for (int i = 0; i < objects.Length; i++)
                    {
                        if (objects[i].Unbox<int>() > 1000)
                        {
                            LoggerUtill.Log(sender.prop_APIUser_0.displayName + " sended invalid " + vrcEvent.EventType, ConsoleColor.Red, true);
                            return false;
                        }
                    }
                }
            }
            return true;
        }
    }
}
