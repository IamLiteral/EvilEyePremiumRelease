using EvilEye.Events;
using EvilEye.SDK;
using EvilEye.SDK.ButtonAPI;
using EvilEye.SDK.PhotonSDK;
using ExitGames.Client.Photon;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VRC;

namespace EvilEye.Module.Settings
{
    class EventLogger : BaseModule, OnEventEvent
    {
        public EventLogger() : base("EventLogger", "Logs Photon Events", Main.Instance.settingsLogger, null, true, true)
        {
        }

        public override void OnEnable()
        {
            Main.Instance.onEventEvents.Add(this);
        }

        public override void OnDisable()
        {
            Main.Instance.onEventEvents.Remove(this);
        }

        public bool OnEvent(EventData eventData)
        {
            if (eventData.Code == 7 || eventData.Code == 1 || eventData.Code == 8)
                return true;
            string Payload = "";
            Player player = PlayerWrapper.GetPlayerByActorID(eventData.Sender);
            string playerName = player != null ? player.GetAPIUser().displayName : eventData.Sender + "";
            if (eventData.Parameters != null)
                Payload = JsonConvert.SerializeObject(Serialization.FromIL2CPPToManaged<object>(eventData.Parameters), Formatting.Indented);
            LoggerUtill.Log($"[OpLog] {playerName} sended {eventData.Code} {Payload}", ConsoleColor.Cyan, true);
            
            return true;
        }
    }
}
